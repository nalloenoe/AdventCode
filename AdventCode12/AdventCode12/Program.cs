using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventCode12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode\AdventCode12\input.txt");
            string[] mapLines = bigInput.Split(Environment.NewLine);
            List<string> map = mapLines.ToList();
            List<int> pathLengths = new List<int>(); //a list that keeps track of all the pathways

            for (int i = 0; i < map.Count; i++)
            {
                for(int j = 0; j< map[i].Length; j++)
                {
                    if (map[i][j] != 'S' && map[i][j] != 'a')
                    {
                        continue;
                    }

                    var start = new Tile();
                    start.Y = i;
                    start.X = j;

                    var goal = new Tile();
                    goal.Y = map.FindIndex(x => x.Contains("E"));
                    goal.X = map[goal.Y].IndexOf("E");

                    start.SetDistance(goal.X, goal.Y);

                    var activeTiles = new List<Tile>(); //tiles we're currently working on
                    activeTiles.Add(start);
                    var visitedTiles = new List<Tile>(); //different paths to goal


                    while (activeTiles.Any())
                    {
                        var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                        if (checkTile.X == goal.X && checkTile.Y == goal.Y)
                        {
                            //Console.WriteLine("Reached destination!");
                            //we found destination, and becuase the orderby method it is the lowest cost
                            var tile = checkTile;
                            //Console.WriteLine("Retracing Steps..");
                            int pathlength = 0;
                            while (true)
                            {

                                //Uncomment for part 1
                                //var newMapRow = map[tile.Y].ToCharArray();
                                //newMapRow[tile.X] = '*';
                                //map[tile.Y] = new string(newMapRow);
                                pathlength++;

                                tile = tile.Parent;
                                if (tile == null)
                                {
                                    //uncomment for part 1
                                    //Console.WriteLine("Map: ");
                                    //map.ForEach(x => Console.WriteLine(x));
                                    //Console.WriteLine("done");
                                    //Console.WriteLine("pathlength: {0}", pathlength-1);
                                    pathLengths.Add(pathlength-1); //for part 2
                                    break;
                                }
                            }
                        }

                        visitedTiles.Add(checkTile);
                        activeTiles.Remove(checkTile);

                        var walkableTiles = WalkableTiles(map, checkTile, goal);
                        foreach (var walkableTile in walkableTiles)
                        {
                            //dont check already visited tiles
                            if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                                continue;

                            //check if this active tile still has a better route
                            if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                            {
                                var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                                if (existingTile.CostDistance > checkTile.CostDistance)
                                {
                                    activeTiles.Remove(existingTile);
                                    activeTiles.Add(walkableTile);
                                }
                            }
                            else
                            {
                                activeTiles.Add(walkableTile); //we havent seen this tile before so add
                            }
                        }
                    }
                    Console.WriteLine("No path found!");
                }
            }

            pathLengths.Sort();
            Console.WriteLine("Shortest path: {0}", pathLengths[0]);
            //Train of thought
            //find S and E
            //A* algorithm? https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/ 
        }
        //get a list of walkable tiles around a tile
        private static List<Tile> WalkableTiles(List<string> map, Tile current, Tile target)
        {
            List<Tile> possibleTiles = new List<Tile>()
                {
                new Tile { X = current.X, Y = current.Y - 1, Parent = current, Cost = current.Cost + 1 },
                new Tile { X = current.X, Y = current.Y + 1, Parent = current, Cost = current.Cost + 1},
                new Tile { X = current.X - 1, Y = current.Y, Parent = current, Cost = current.Cost + 1 },
                new Tile { X = current.X + 1, Y = current.Y, Parent = current, Cost = current.Cost + 1 },
                };
            possibleTiles.ForEach(tile => tile.SetDistance(target.X, target.Y));

            int maxX = map.First().Length - 1;
            int maxY = map.Count - 1;

            List<Tile> possTilesF = new List<Tile>();
            
            foreach(Tile postile in possibleTiles)
            {
                if((postile.X >= 0 && postile.X <= maxX) && (postile.Y >= 0 && postile.Y <= maxY))
                {
                    if(map[postile.Y][postile.X] == 'E')
                    {
                        if(map[current.Y][current.X] == 'z')
                        {
                            possTilesF.Add(postile);
                            continue;
                        }
                    }
                    else if(map[current.Y][current.X] == 'S')
                    {
                        if (map[postile.Y][postile.X] == 'a')
                        {
                            possTilesF.Add(postile);
                            continue;
                        }
                    }
                    else if((char.ToUpper(map[postile.Y][postile.X]) - 64) - (char.ToUpper(map[current.Y][current.X]) - 64) <= 1)
                    {
                        possTilesF.Add(postile);
                        continue;
                    }

                }
                
            }

            return possTilesF;
                        //.Where(tile => tile.X >= 0 && tile.X <= maxX) //xbound
                        //.Where(tile => tile.Y >= 0 && tile.Y <= maxY) //ybound
                        //.Where(tile => (map[tile.Y][tile.X] == 'E' && map[current.Y][current.X] == 'z') || //when tile is E the current should be z
                        //                map[current.Y][current.X] == 'S' && map[tile.Y][tile.X] == 'a') //when the current tile is S, the next tile should be a
                        //.Where(tile => ((char.ToUpper(map[tile.Y][tile.X]) - 64) - (char.ToUpper(map[current.Y][current.X]) - 64) <= 1 ))                                        
                        //.ToList(); //line above: check if letter is smaller bigger or same. Or that tile is destination
            //ADD that you cant immediately jump to E?, first have to go through Z?
            //How to do ifstatement in .where (if tile =E only z, if current tile =S, only a
        }

        
    }
    class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; } //How much it costs to travel to this tile
        public int Distance { get; set; } //Estimated distance to goal
        public int CostDistance => Cost + Distance; //Important for choosing which way to go
        public Tile Parent { get; set; } //previous tile we came from

        //estimated distance towards goal (ignoring walls etc)
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }
    }
}
