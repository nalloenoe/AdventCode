using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode11
{
    class Program
    {
        int round = 1;
        int[] inspection;
        Monkey[] monkeys;
        int makeSmall = 1;
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("Hello World!");
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode\AdventCode11\input.txt");
            string[] monkeysI = bigInput.Split(Environment.NewLine + Environment.NewLine);
            program.inspection = new int[monkeysI.Length]; //places for each monkey
            program.monkeys = new Monkey[monkeysI.Length];

            program.InputHandler(monkeysI);


            for(int i =0; i<10000; i++)//amount of rounds
            {
                program.Round();
            }

            Array.Sort(program.inspection);
            Array.Reverse(program.inspection);
            Console.WriteLine(program.inspection[0] * (long)program.inspection[1]);
        }

        //Train of Thought
        //Split the monkeys
        //Make a function that handles a monkey
        //keep track of round
        //keep track of inspection by monkey
        //at end + the two highest ones.
        //Part 2
        //int limit = 2,147,483,647
        //something noticable: modulo with the division options are: 1, 2, 8 or 10 (5 for test)

        public struct Monkey
        {
            public List<long> items;
            public string operation;
            public int division;
            public int trueDiv;
            public int falseDiv;
        }

        public void InputHandler(string[] monkeysI)
        {
            //handle all the monkeys
            for(int i = 0; i<monkeysI.Length;i++ )
            {
                monkeys[i].items = new List<long>();
                //split the input into usable parts
                string[] monkeyLine = monkeysI[i].Split(Environment.NewLine);
                for (int j = 1; j < monkeyLine.Length; j++)
                {
                    string[] monkeyMoo = monkeyLine[j].Split(' ');
                    if (j == 1)
                    {
                        monkeyMoo = monkeyMoo.Skip(4).ToArray();
                        string[] itemsString = String.Concat(monkeyMoo).Split(',');
                        for (int k = 0; k < itemsString.Length; k++)
                            monkeys[i].items.Add(long.Parse(itemsString[k]));
                    }
                    else if (j == 2)
                        monkeys[i].operation = String.Join(" ", monkeyMoo.Skip(5).ToArray());
                    else if (j == 3)
                    {
                        monkeys[i].division = int.Parse(monkeyMoo[5]);
                        makeSmall *= int.Parse(monkeyMoo[5]);
                    }
                    else if (j == 4)
                        monkeys[i].trueDiv = int.Parse(monkeyMoo[9]);
                    else if (j == 5)
                        monkeys[i].falseDiv = int.Parse(monkeyMoo[9]);
                }
            }
            
        }
        public void Round()
        {
            //go through all the monkeys
            for(int i =0; i< monkeys.Length; i++)
            {
                MonkeyHandler(i);
            }
            round++;//next round
        }

        public void MonkeyHandler(int monkeyN)
        {
            for(int i=0; i< monkeys[monkeyN].items.Count; i++)
            {
                //inspectItem
                monkeys[monkeyN].items[i] = InspectItem(monkeyN, i);
                ThrowItem(monkeyN, i);
            }
            monkeys[monkeyN].items.Clear();
        }

        public long InspectItem(int monkeyN, int itemN)
        {
            inspection[monkeyN]++;

            long left = monkeys[monkeyN].items[itemN];
            long right = monkeys[monkeyN].items[itemN];
            string[] operationParts = monkeys[monkeyN].operation.Split(' ');

            if (operationParts[0] != "old")
                left = long.Parse(operationParts[0]);
            if (operationParts[2] != "old")
                right = long.Parse(operationParts[2]);

            if (operationParts[1] == "*")
                return ((left * right)%makeSmall); //for part 1 add /3
            else
                return ((left + right)%makeSmall);
        }

        public void ThrowItem(int monkeyN, int itemN)
        {
            if (monkeys[monkeyN].items[itemN] % monkeys[monkeyN].division == 0)
            {
               //throw to monkey number trueDiv
                monkeys[monkeys[monkeyN].trueDiv].items.Add(monkeys[monkeyN].items[itemN]); 
            }
            else
            {
                 monkeys[monkeys[monkeyN].falseDiv].items.Add(monkeys[monkeyN].items[itemN]); 
            }
        }
    }
}
