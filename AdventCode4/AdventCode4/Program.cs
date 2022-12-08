using System;

namespace AdventCode4
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            int totalWrongIDs = 0;

            Console.WriteLine("Hello World!");
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode4\input.txt");
            string[] cleanList = bigInput.Split(Environment.NewLine);

            for(int i = 0; i < cleanList.Length; i++)
            {
                totalWrongIDs += program.splitIDList(cleanList[i]);
            }

            Console.WriteLine("The total amount of wrong IDs are: ");
            Console.WriteLine(totalWrongIDs);
        }

        //Train of Thought:
        //Split everything right.
        //Go through the whole splitted list
        //Check for each list if they are in each other (both sides!) 
        //(right number (2) minus right number (1) other guy, if its still bigger than left number (1), (2) is fully in (1)
        //Safe this number

        public int splitIDList (string IDs)
        {
            string[] elves = IDs.Split(',');
            string[] elve1 = elves[0].Split('-');
            string[] elve2 = elves[1].Split('-');
            int[] elf1_2 = { int.Parse(elve1[0]), int.Parse(elve1[1]), int.Parse(elve2[0]), int.Parse(elve2[1]) };

            return checkOverlap2(elf1_2);
        }

        public int checkOverlap(int[] IDs)
        {
            if (IDs[3] <= IDs[1] && IDs[2]>= IDs[0] )
            { return 1; }
            if(IDs[1] <= IDs[3] && IDs[0] >= IDs[2])
            { return 1; }
            return 0;
        }

        public int checkOverlap2(int[] IDs)
        {
            if ((IDs[3] <= IDs[1] && IDs[3] >= IDs[0]) || (IDs[2] >= IDs[0] && IDs[2] <= IDs[1]))
            { return 1; }
            if ((IDs[1] <= IDs[3] && IDs[1] >= IDs[2]) || (IDs[0] >= IDs[2] && IDs[0] <= IDs[3]))
            { return 1; }
            return 0;
        }


    }
}
