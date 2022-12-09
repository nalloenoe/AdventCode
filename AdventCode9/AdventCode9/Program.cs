using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace AdventCode9
{
    class Program
    {
        List<Vector2> positionsT = new List<Vector2>();
        Vector2 currLocH = new Vector2(0, 0);
        Vector2 currLocT = new Vector2(0, 0);
        List<Vector2> positionsT2 = new List<Vector2>(); //for the second part
        Vector2[] currLoc =  { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) };
        //0 is H, 9 = T. Everything starts on (0,0)


        static void Main(string[] args)
        {
            Program program = new Program();

            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode\AdventCode9\input.txt");
            string[] instructions = bigInput.Split(Environment.NewLine);

            program.positionsT.Add(program.currLocT); //Add the starting position
            program.positionsT2.Add(program.currLoc[9]);
            program.ReadInstructions(instructions);


            Console.WriteLine("The visited places are {0}", program.positionsT.Distinct().Count());
            Console.WriteLine("The visited places for 9T are {0}", program.positionsT2.Distinct().Count());
        }

        //Train of thought
        //Make a function that reads the input
        //Make a function that moves the Head
        //Make a function that moves the Tail and store this location in the list
        //at the end see how many distinct locations there are in the list.

        public void ReadInstructions(string[] instructions)
        {
            string[] lineI;
            //loop through all the instruction lines
            for (int i = 0; i < instructions.Length; i++)
            {
                lineI = instructions[i].Split(' ');

                if (lineI[0] == "U")
                {
                    MoveUp(int.Parse(lineI[1]));
                    MoveUp2(int.Parse(lineI[1]));
                    continue;
                }
                else if (lineI[0] == "D")
                { 
                    MoveDown(int.Parse(lineI[1]));
                    MoveDown2(int.Parse(lineI[1]));
                    continue; 
                }
                else if (lineI[0] == "L")
                { 
                    MoveLeft(int.Parse(lineI[1]));
                    MoveLeft2(int.Parse(lineI[1]));
                    continue; 
                }
                else
                { 
                    MoveRight(int.Parse(lineI[1]));
                    MoveRight2(int.Parse(lineI[1]));
                    continue; 
                }
            }
        }

        //The functions for moving for part 1
        public void MoveUp(int movement)
        {
            for (int i = 0; i < movement; i++)
            {
                if (currLocT.Y == currLocH.Y || currLocT.Y == (currLocH.Y + 1))//T is on H, or H is leveled/below T
                {
                    currLocH.Y++; //move one up, Tail stays in place
                    continue;
                }
                if (currLocT.X == currLocH.X) //T is directly under H
                {
                    currLocH.Y++; //move one up
                    currLocT.Y++; //move one up

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
                if (currLocT.X == (currLocH.X + 1))    //H is left up from T
                {
                    currLocH.Y++; //move one up
                    currLocT += new Vector2(-1, 1); //move diagonaly up left

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
                else //H is right up from T
                {
                    currLocH.Y++; //move one up
                    currLocT += new Vector2(1, 1); //move diagonally up right

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
            }
        }
        public void MoveDown(int movement)
        {
            for(int i=0; i< movement; i++)
            {
                if (currLocT.Y == currLocH.Y || currLocT.Y == (currLocH.Y - 1))//T is on H, or H is leveled/above T
                {
                    currLocH.Y--; //move one down, Tail stays in place
                    continue;
                }
                if (currLocT.X == currLocH.X) //T is directly above H
                {
                    currLocH.Y--; //move one down
                    currLocT.Y--; //move one down

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
                if (currLocT.X == (currLocH.X + 1))    //H is left down from T
                {
                    currLocH.Y--; //move one down
                    currLocT += new Vector2(-1, -1); //move diagonaly down left

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
                else //H is right down from T
                {
                    currLocH.Y--; //move one down
                    currLocT += new Vector2(1, -1); //move diagonally down right

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
            }
        }
        public void MoveLeft(int movement)
        {
            for (int i = 0; i < movement; i++)
            {
                if (currLocT.X == currLocH.X || currLocT.X == (currLocH.X - 1))//T is on H, or H is leveled/right of T
                {
                    currLocH.X--; //move one to left, Tail stays in place
                    continue;
                }
                if (currLocT.Y == currLocH.Y) //T is directly right H
                {
                    currLocH.X--; //move one to left
                    currLocT.X--; //move one to left

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
                if (currLocT.Y == (currLocH.Y - 1))    //H is left up from T
                {
                    currLocH.X--; //move one to left
                    currLocT += new Vector2(-1, 1); //move diagonaly top left

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
                else //H is left down from T
                {
                    currLocH.X--; //move one to left
                    currLocT += new Vector2(-1, -1); //move diagonally down left

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
            }
        }
        public void MoveRight(int movement)
        {
            for (int i = 0; i < movement; i++)
            {
                if (currLocT.X == currLocH.X || currLocT.X == (currLocH.X + 1))//T is on H, or H is leveled/left of T
                {
                    currLocH.X++; //move one to right, Tail stays in place
                    continue;
                }
                if (currLocT.Y == currLocH.Y) //T is directly left H
                {
                    currLocH.X++; //move one to right
                    currLocT.X++; //move one to right

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
                if (currLocT.Y == (currLocH.Y - 1))    //H is right up from T
                {
                    currLocH.X++; //move one to right
                    currLocT += new Vector2(1, 1); //move diagonaly top right

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
                else //H is right down from T
                {
                    currLocH.X++; //move one to right
                    currLocT += new Vector2(1, -1); //move diagonally down right

                    positionsT.Add(currLocT); //add new location to list
                    continue;
                }
            }
        }

        //The functions for moving for part 2
        public void MoveUp2(int movement)
        {
            for (int i = 0; i < movement; i++)
            {
                //deal with the head
                if (currLoc[1].Y == currLoc[0].Y || currLoc[1].Y == (currLoc[0].Y + 1))//T is on H, or H is leveled/below T
                {
                        currLoc[0].Y++; //move one up, Tail stays in place
                }
                else if (currLoc[1].X == currLoc[0].X) //T is directly under H
                {
                    currLoc[0].Y++; //move one up
                    currLoc[1].Y++; //move one up
                }
                else if (currLoc[1].X == (currLoc[0].X + 1))    //H is left up from T
                {
                    currLoc[0].Y++; //move one up
                    currLoc[1] += new Vector2(-1, 1); //move diagonaly up left
                }
                else //H is right up from T
                {
                    currLoc[0].Y++; //move one up
                    currLoc[1] += new Vector2(1, 1); //move diagonally up right
                }
                //deal with the tail
                for (int j = 1; j<currLoc.Length-1; j++)
                {
                    if (OutOfBounds(currLoc[j + 1], currLoc[j]))
                    {
                        FixOOB(j);
                    }
                    else
                        continue;
                }                
            }
        }
        public void MoveDown2(int movement)
        {
            for (int i = 0; i < movement; i++)
            {
                if (currLoc[1].Y == currLoc[0].Y || currLoc[1].Y == (currLoc[0].Y - 1))//T is on H, or H is leveled/above T
                {
                        currLoc[0].Y--; //move one down, Tail stays in place
                }
                else if (currLoc[1].X == currLoc[0].X) //T is directly above H
                {
                    currLoc[0].Y--; //move one down
                    currLoc[1].Y--; //move one down
                }
                else if (currLoc[1].X == (currLoc[0].X + 1))    //H is left down from T
                {
                    currLoc[0].Y--; //move one down
                    currLoc[ 1] += new Vector2(-1, -1); //move diagonaly down left
                }
                else //H is right down from T
                {
                    currLoc[0].Y--; //move one down
                    currLoc[1] += new Vector2(1, -1); //move diagonally down right
                }
                for (int j = 0; j < currLoc.Length-1; j++)
                {
                    if (OutOfBounds(currLoc[j + 1], currLoc[j]))
                    {
                        FixOOB(j);
                    
                    }
                    else
                        continue;
                }
            }
        }
        public void MoveLeft2(int movement)
        {
            for (int i = 0; i < movement; i++)
            {
                if (currLoc[1].X == currLoc[0].X || currLoc[1].X == (currLoc[0].X - 1))//T is on H, or H is leveled/right of T
                {
                        currLoc[0].X--; //move one to left, Tail stays in place
                }
                else if (currLoc[1].Y == currLoc[0].Y) //T is directly right H
                {
                    currLoc[0].X--; //move one to left
                    currLoc[1].X--; //move one to left
                }
                else if (currLoc[1].Y == (currLoc[0].Y - 1))    //H is left up from T
                {
                    currLoc[0].X--; //move one to left
                    currLoc[1] += new Vector2(-1, 1); //move diagonaly top left
                }
                else //H is left down from T
                {
                    currLoc[0].X--; //move one to left
                    currLoc[1] += new Vector2(-1, -1); //move diagonally down left
                }
                for (int j = 1; j < currLoc.Length-1; j++)
                {
                    if (OutOfBounds(currLoc[j + 1], currLoc[j]))
                    {
                        FixOOB(j);
                    }
                    else
                        continue;
                }
            }
        }
        public void MoveRight2(int movement)
        {
            for (int i = 0; i < movement; i++)
            {
                if (currLoc[1].X == currLoc[0].X || currLoc[1].X == (currLoc[0].X + 1))//T is on H, or H is leveled/left of T
                {
                    currLoc[0].X++; //move one to right, Tail stays in place
                }
                else if (currLoc[1].Y == currLoc[0].Y) //T is directly left H
                {
                    currLoc[0].X++; //move one to right
                    currLoc[1].X++; //move one to right
                }
                else if (currLoc[1].Y == (currLoc[0].Y - 1))    //H is right up from T
                {
                    currLoc[0].X++; //move one to right
                    currLoc[1] += new Vector2(1, 1); //move diagonaly top right
                }
                else //H is right down from T
                {
                    currLoc[0].X++; //move one to right
                    currLoc[1] += new Vector2(1, -1); //move diagonally down right
                }
                for (int j = 1; j < currLoc.Length-1; j++)
                {
                    if (OutOfBounds(currLoc[j+1],currLoc[j]))
                    {
                        FixOOB(j);
                        /*if(currLoc[j+1].Y == currLoc[j].Y)
                        {
                            currLoc[j + 1].X++;
                            if (j + 1 == 9)
                            {
                                positionsT2.Add(currLoc[j + 1]); //add new location to list
                            }
                            continue;
                        }
                        else if(currLoc[j+1].Y == currLoc[j].Y-1)
                        {
                            currLoc[j + 1] += new Vector2(1, 1); //move diagonaly top right

                            if (j + 1 == 9)
                            {
                                positionsT2.Add(currLoc[j + 1]); //add new location to list
                            }
                            continue;
                        }
                        else
                        {
                            currLoc[j + 1] += new Vector2(1, -1); //move diagonally down right

                            if (j + 1 == 9)
                            {
                                positionsT2.Add(currLoc[j + 1]); //add new location to list
                            }
                            continue;
                        }*/
                    }
                    else
                        break;
                }
            }
        }

        //check if the tail needs to move or not
        public bool OutOfBounds(Vector2 T, Vector2 H)
        {
            Vector2 distance = T-H;
            if (Math.Abs(distance.X) > 1 || Math.Abs(distance.Y) > 1)
                return true;
            return false;
        }
        //fix the out of bounds position for a tail segment
        public void FixOOB(int j)
        {
            Vector2 distance = currLoc[j] - currLoc[j+1];
            if (distance.X == 0 || distance.Y == 0)//X or Y =0, same as in the same row or column
            {
                if (distance.X > 1)
                {
                    currLoc[j + 1].X++;
                }
                else if (distance.X < -1)
                {
                    currLoc[j + 1].X--;
                }
                if (distance.Y > 1)
                {
                    currLoc[j + 1].Y++;
                }
                else if (distance.Y < -1)
                {
                    currLoc[j + 1].Y--;
                }
            }
            //move diagonal
            else if (Math.Abs(distance.X) == 2)
            {
                if (distance.X > 1)
                {
                    if (distance.Y > 0)
                        currLoc[j + 1] += new Vector2(1, 1);
                    else
                        currLoc[j + 1] += new Vector2(1, -1);
                }
                if (distance.X < -1)
                {
                    if (distance.Y > 0)
                        currLoc[j + 1] += new Vector2(-1, 1);
                    else
                        currLoc[j + 1] += new Vector2(-1, -1);
                }
            }
            else if(Math.Abs(distance.Y)==2)
            {
                if (distance.Y > 1)
                {
                    if (distance.X > 0)
                        currLoc[j + 1] += new Vector2(1, 1);
                    else
                        currLoc[j + 1] += new Vector2(-1, 1);
                }
                if (distance.Y < -1)
                {
                    if (distance.X > 0)
                        currLoc[j + 1] += new Vector2(1, -1);
                    else
                        currLoc[j + 1] += new Vector2(-1, -1);
                }
            }
            if(j+1 == 9)
            {
                positionsT2.Add(currLoc[j + 1]); //add new location to list
            }
            
        }
    }
}
