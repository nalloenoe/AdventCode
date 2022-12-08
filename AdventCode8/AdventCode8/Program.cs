using System;

namespace AdventCode8
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("Hello World!");
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode8\input.txt");
            string[] rowInput = bigInput.Split(Environment.NewLine);
            int totalVisb = (rowInput[0].Length * 2) + (rowInput.Length * 2) - 4; //the top and bottom row and left and right column are always counted.

            totalVisb += program.visbCheck(rowInput);
            Console.WriteLine("Total Visible: {0}", totalVisb);
            Console.WriteLine("The highest Scenic Score = {0}", program.scenicScore(rowInput));
        }

        //Train of thought
        //make 2 functions. one that checks visibility in the collumn and one that checks visibility in the row
        //skip first and last row and first and last column
        //keep a counter
        //check if number is smaller than.
        //go back (up the row) and forward (through the row)

        //Checks the visibility of each tree and adds it to a total
        public int visbCheck(string[] input)
        {
            int total = 0;
            for (int i = 1; i < input.Length - 1; i++)
            {
                for (int j = 1; j < input[i].Length - 1; j++)
                {
                    if (visbRow(input, i, j) || visbCol(input, i, j))
                    {
                        total++;
                    }
                }
            }

            return total;
        }

        //Checks the row visibility of a given tree
        public bool visbRow(string[] forest, int row, int col)
        {
            int visbCheck = forest[row][col] - '0';

            //go forward through the row
            for (int i = col + 1; i < forest[row].Length; i++)
            {
                if (visbCheck <= (forest[row][i] - '0'))//tree is not visible
                {
                    //go backwards through the row
                    for (int j = col - 1; j >= 0; j--)
                    {
                        if (visbCheck <= (forest[row][j] - '0'))//tree is not visible
                            return false;
                    }
                    break;
                }
            }
            return true;
        }

        //Checks the col visibility of a given tree
        public bool visbCol(string[] forest, int row, int col)
        {
            int visbCheck = forest[row][col] - '0';

            //go forward through the col
            for (int i = row + 1; i < forest.Length; i++)
            {
                if (visbCheck <= (forest[i][col] - '0'))//tree is not visible
                {
                    //go backwards through the col
                    for (int j = row - 1; j >= 0; j--)
                    {
                        if (visbCheck <= (forest[j][col] - '0'))//tree is not visible
                            return false;
                    }
                    break;
                }
            }
            return true;
        }

        //Calculate the best scenic score of all the trees.
        public int scenicScore(string[] input)
        {
            int highestScore = 0;
            int tempScore;
            for (int i = 1; i < input.Length - 1; i++)
            {
                for (int j = 1; j < input[i].Length - 1; j++)
                {
                    tempScore = visbRowScore(input, i, j) * visbColScore(input, i, j);
                    if(tempScore > highestScore)//check if the score of this tree is higher than the highest measured scenic score
                    {
                        highestScore = tempScore;
                    }                       
                }
            }
            return highestScore;
        }

        //Calculate the row scenic score of a given tree
        public int visbRowScore(string[] forest, int row, int col)
        {
            int visbCheck = forest[row][col] - '0';
            int visbScore = 1;

            //go forward through the row
            for (int i = col + 1; i < forest[row].Length; i++)
            {
                if (visbCheck <= (forest[row][i] - '0'))
                {
                    visbScore *= i - col;
                    break;
                }
                if(i == forest[row].Length -1) //tree is visible from edge
                {
                    visbScore *= i - col;
                }
            }
            //go backwards through the row
            for (int j = col - 1; j >= 0; j--)
            {
                if (visbCheck <= (forest[row][j] - '0'))
                {
                    visbScore *= j - col;
                    break;
                }
                if(j == 0) //tree is visible from edge
                {
                    visbScore *= col;
                }
            }
            return visbScore;
        }

        //Calculate the col scenic score of a given tree
        public int visbColScore(string[] forest, int row, int col)
        {
            int visbCheck = forest[row][col] - '0';
            int visbScore = 1;

            //go forward through the col
            for (int i = row + 1; i < forest.Length; i++)
            {
                if (visbCheck <= (forest[i][col] - '0'))
                {
                    visbScore *= i - row;
                    break;
                }
                if(i == forest.Length -1)//tree is visible from edge
                {
                    visbScore *= i - row;
                }
            }
            //go backwards through the col
            for (int j = row - 1; j >= 0; j--)
            {
                if (visbCheck <= (forest[j][col] - '0'))
                {
                    visbScore *= row - j;
                    break;
                }
                if(j == 0)//tree is visible from edge
                {
                    visbScore *= row;
                }
            }
            return visbScore;
        }
    }
}
