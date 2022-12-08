using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode5
{
    class Program
    {

        Stack<char>[] lanes = new Stack<char>[9];

        static void Main(string[] args)
        {

            Program program = new Program();

            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode5\input.txt");
            string[] inputs = bigInput.Split(Environment.NewLine + Environment.NewLine); //split into crates and instructions
            string[] instructions = inputs[1].Split(Environment.NewLine);

            for(int i =0; i< program.lanes.Length; i++) //Create new stack for each stack in lanes.
            {
                if ( program.lanes[i] == null)
                    program.lanes[i] = new Stack<char>();
            }

            //Put the input string into all the lane stacks.
            program.crateInput(inputs[0]);

            for (int i = 0; i < instructions.Length; i++)
            {
                program.craneInstructions(instructions[i]);
            }

            for(int i = 0; i< 9; i++) 
            {
                Console.WriteLine(program.lanes[i].Peek());
            }

            //Answer 1: SVFDLGLWV
            //Answer 2: DCVTCVPCL
        }

        //Train of Thought
        //Split input in two (instructions and crates)
        //Split crates into array/lists(?) (for each lane one)
        //Split instructions into usable input
        //Give an instruction line to a function that handles it
        //return top boxes


        //Fill the lane stacks with crates
        public void crateInput(string input)
        {
            string[] inputLines = input.Split(Environment.NewLine);
            
            //Go backwards over the input (starting from the bottom to the top)
            for(int i = inputLines.Length-2; i >=0; i--)
            {
                //keep track of the lane that we're in
                int lanePlacer = 0;
                //Jump 4 segments to get to a crate input (the spaces and brackets are ignored)
                for (int j = 1; j<inputLines[i].Length-1; j+=4)
                {
                    //if there is a crate, push it to the stack
                    if(inputLines[i][j] != ' ')
                    {
                        lanes[lanePlacer].Push(inputLines[i][j]);
                    }
                    lanePlacer++;
                }
            }            
        }

        //Create craneInstructions that can be send towards the crane
        public void craneInstructions(string inputline)
        {
            string[] instr = inputline.Split(' ');
            int move = int.Parse(instr[1]);
            int loc = int.Parse(instr[3]);
            int towards = int.Parse(instr[5]);
            
            //craneMovement(move, loc, towards); //This will move the crates one by one
            craneMovement2(move, loc, towards); //This will move a whole crate section at once
        }

        //Code for part 1
        public void craneMovement(int move, int loc, int towards)
        {
            char movingCrate;
            for (int i = 0; i < move; i++)
            {
                movingCrate = lanes[(loc - 1)].Pop();
                lanes[towards - 1].Push(movingCrate);
            }
        }

        //Code for part 2
        public void craneMovement2(int move, int loc, int towards)
        {
            char[] movingCrates = new char[move];
            for(int i =0; i< move; i++)
            {
                movingCrates[i] = lanes[loc - 1].Pop();
            }
            for(int i=move-1; i>=0; i--)
            {
                lanes[towards - 1].Push(movingCrates[i]);
            }
        }

    }
}
