using System;

namespace AdventCode1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create object for class
            Program program= new Program();

            int totalCal;

            //Read Input
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode1\input.txt");
            //divide each elve into an array
            string[] elves = program.divideElves(bigInput);

            //calculate the total amount of cals each elve is carrying
            int[] elveswithNumbers = new int[elves.Length];
            for (int i = 0; i < elves.Length; i++)
            {
                totalCal = program.divideFoods(elves[i]);
                elveswithNumbers[i] = totalCal;
            }

            //write the biggest callorie to the console
            Console.WriteLine("The biggest Calorie is: ");
            Console.WriteLine(program.findBigFood(elveswithNumbers));
        }

        //Train of thought:
        //take input and divide between enters, put in list
        //divide each list input again on spaces, add up
        //find biggest number in list

 
        // Divide the big input that is still in a string into individual elve strings in an Array (split on the empty enters)
        public string[] divideElves(string bigInput)
        {
            //divide on empty spaces
            string[] elves = bigInput.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);            
            return elves;
        }

        //Divide a single elvesInventory string into all the individual food and calculate the total amount of carried cals
        public int divideFoods(string elvesFood)
        {
            string[] food = elvesFood.Split('\n');
            int totalCal=0;
            for(int i = 0; i<food.Length; i++)
            {
                totalCal += int.Parse(food[i]);
            }
            return totalCal;
        }

        //from all the calories carried by the elves, find the top 3 that carry the most and add them to each other.
        public int findBigFood(int[] elves)
        {
            int biggest1 = 0;
            int biggest2 = 0;
            int biggest3 = 0;
            for(int i=0; i<elves.Length; i++)
            {
                if(elves[i]>biggest1)
                {
                    biggest3 = biggest2;
                    biggest2 = biggest1;
                    biggest1 = elves[i];

                }
                else if(elves[i]>biggest2)
                {
                    biggest3 = biggest2;
                    biggest2 = elves[i];
                }
                else if(elves[i]>biggest3)
                {
                    biggest3 = elves[i];
                }
            }
            return biggest1+biggest2+biggest3;
        }

    }
}
