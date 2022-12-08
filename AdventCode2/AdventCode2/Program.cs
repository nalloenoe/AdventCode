using System;

namespace AdventCode2
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();

            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode2\input.txt");
            string[] matchInput = bigInput.Split(Environment.NewLine); //apparently just doing '\n' there is a chance of \r still being there. This takes care of that
            int totalScore = 0;

            for (int i =0;i<matchInput.Length; i++)
            {
                totalScore += program.scoreMatch2(matchInput[i]);
            }

            Console.WriteLine("The final score is: ");
            Console.WriteLine(totalScore);
        }

        //Train of thought:
        //A = Rock, B = Paper, C = Scissors (opponent)
        //X = Rock, Y = Paper, Z = Scissors (me)
        //A -> Y = 6  A -> X = 3  A -> Z = 0
        //B -> Z = 6  B -> X = 0  B -> Y = 3
        //C -> X = 6  C -> Y = 0  C -> Z = 3

        //X = lose, Y = draw, Z = win
        //A -> Y = 3 + 1 = 4    A -> X = 0 + 3 = 3    A -> Z = 6 + 2 = 8
        //B -> Z = 6 + 3 = 9    B -> X = 0 + 1 = 1    B -> Y = 3 + 2 = 5
        //C -> X = 0 + 2 = 2    C -> Y = 3 + 3 = 6    C -> Z = 6 + 1 = 7


        public int scoreMatch(string match)
        {
            int playScore=0;
            int winScore=0;
            string[] player = match.Split(' '); 

            //Check the score for what you played
            switch (player[1])
            {
                case "X":
                    playScore = 1;
                    break;
                case "Y":
                    playScore = 2;
                    break;
                case "Z":
                    playScore = 3;
                    break;
                default:
                    break;
            }

            //Check the score for the match result
            switch(player[0])
            {
                case "A":
                    if(player[1] == "X") { winScore = 3; }
                    else if(player[1] == "Y") { winScore = 6; }
                    else if (player[1] == "Z") { winScore = 0; }
                    break;
                case "B":
                    if (player[1] == "X") { winScore = 0; }
                    else if (player[1] == "Y") { winScore = 3; }
                    else if (player[1] == "Z") { winScore = 6; }
                    break;
                case "C":
                    if (player[1] == "X") { winScore = 6; }
                    else if (player[1] == "Y") { winScore = 0; }
                    else if (player[1] == "Z") { winScore = 3; }
                    break;
                default:
                    break;
            }

            return playScore + winScore;
        }

        public int scoreMatch2(string match)
        {            
            int matchScore = 0;
            string[] player = match.Split(' ');

            //See logic for this switch statement in 'Train of Thought' comments
            switch (player[0])
            {
                case "A":
                    if (player[1] == "X") { matchScore = 3; }
                    else if (player[1] == "Y") { matchScore = 4; }
                    else if (player[1] == "Z") { matchScore = 8; }
                    break;
                case "B":
                    if (player[1] == "X") { matchScore = 1; }
                    else if (player[1] == "Y") { matchScore = 5; }
                    else if (player[1] == "Z") { matchScore = 9; }
                    break;
                case "C":
                    if (player[1] == "X") { matchScore = 2; }
                    else if (player[1] == "Y") { matchScore = 6; }
                    else if (player[1] == "Z") { matchScore = 7; }
                    break;
                default:
                    break;
            }

            return matchScore;
        }
    }
}
