using System;
using System.Linq;

namespace AdventCode6
{
    class Program
    {
        static void Main(string[] args)
        {
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode6\input.txt");

            //Go through the string, but stop 3/13 digits earlier (as you want to check for +3/13)
            for (int q = 0; q < bigInput.Length - 13; q++)
            {
                //Put the range you want to check for duplicates in a new string
                //string check = bigInput[q..(q + 4)];
                string check2 = bigInput[q..(q + 14)];

                //Use the Distinct() function and check if the count is still the same as it should be 
                //(Distinct gives back a new list without duplicates, so if there are duplicates it would return a smaller number)
                if (check2.Distinct().Count() == 14)
                {
                    //If this is the case, return the last digit number and break the for loop.
                    Console.WriteLine(q + 14);
                    break;
                }
            }
        }
    }
}
