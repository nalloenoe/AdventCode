using System;

namespace AdventCode10
{
    class Program
    {
        int cycle = 0;//Keep track of cycle
        int X = 1; //keep track of X
        int signalStrength = 0; //signal strength (necessary for part 1)
        string[] drawing = new string[7]; //store the drawing for the screen
        int currCycleline = 0; //Keep track of current drawing line
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("Hello World!");
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode\AdventCode10\input.txt");
            string[] inputLines = bigInput.Split(Environment.NewLine);

            program.HandleInstr(inputLines);
            Console.WriteLine("SignalStrength is {0} \n", program.signalStrength);

            for (int i = 0; i < program.drawing.Length; i++)
                Console.WriteLine(program.drawing[i]);
        }

        //Train of thought
        //Keep track of the cycle
        //Keep track of X
        //when cycle is a certain thing, get X * cycles
        //Store this and add them all together at the end.
        //Part2
        //make each line a string/cycle


        public void HandleInstr(string[] inputLines)
        {
            for (int i = 0; i < inputLines.Length; i++)
            {
                if (inputLines[i][0] == 'n')//noop
                {
                    cycle++;
                    SpecialCycle();
                }
                else //addX instruction
                {
                    cycle++;
                    SpecialCycle();
                    cycle++;
                    SpecialCycle();
                    //first add two cycles, then change X
                    string[] addLine = inputLines[i].Split(' ');
                    X += int.Parse(addLine[1]);

                }
            }
        }

        public void SpecialCycle()
        {
            //Check if the cycle is ready for next line
            if ((cycle) % 40 == 0)
            {
                signalStrength += cycle * X; //for part 1: change if statement to cycle-20
                Draw(); //draw pixel 40 (this is still on previous line)
                currCycleline++; //move to next line
            }
            else
                Draw();
        }

        public void Draw()
        {
            //if X aligns with cycle, draw #, otherwise .
            if ((cycle-1) % 40 == X || (cycle-1) % 40 == X - 1 || (cycle-1) % 40 == X + 1)
            {
                drawing[currCycleline] += '#';
            }
            else
                drawing[currCycleline] += '.';
        }
    }
}
