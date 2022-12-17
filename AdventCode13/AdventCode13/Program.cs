using System;
using System.Collections.Generic;

namespace AdventCode13
{
    class Program
    {
        static void Main(string[] args)
        {
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode\AdventCode13\input.txt");
            string[] pairs = bigInput.Split(Environment.NewLine + Environment.NewLine);
            int correctPairs = 0; //Counter for the correct pairs Part 1

            List<object>[] inputSort = new List<object>[(pairs.Length * 2) + 2]; //A list for Part 2 that will be sorted
            //The handling of the extra 2 and 6 and their indexes.
            List<object> two = ListParser.Parse("[[2]]");
            List<object> six = ListParser.Parse("[[6]]");
            inputSort[inputSort.Length - 2] = two;
            inputSort[inputSort.Length - 1] = six;
            int index2= inputSort.Length - 2;
            int index6 = inputSort.Length - 1;

            //loop through all the pairs Part1
            for (int i = 0; i < pairs.Length; i++)
            {
                string[] packetString = pairs[i].Split(Environment.NewLine);
                List<object> pair0 = ListParser.Parse(packetString[0].Trim());
                List<object> pair1 = ListParser.Parse(packetString[1].Trim());
                if (Packets.CompareList(pair0, pair1) <= 0)
                    correctPairs += i + 1;

                //Put everyting into the array Part 2
                inputSort[i] = pair0;
                inputSort[i + pairs.Length] = pair1;
            }

            //loop through the array and compare and move around the pairs.
            for(int i = 0; i< inputSort.Length-1; i++)
            {
                //If not in order, switch the two around and loop through the list again.
                if (Packets.CompareList(inputSort[i], inputSort[i + 1]) > 0)
                {
                    List<object> temp = inputSort[i];
                    inputSort[i] = inputSort[i + 1];
                    inputSort[i + 1] = temp;
                    i = 0;
                }
            }
            //find the indeces of 2 and 6      
            for(int i=0; i<inputSort.Length; i++)
            {
                if (inputSort[i] == two)
                    index2 = i;
                if (inputSort[i] == six)
                    index6 = i;
            }
            
            Console.WriteLine("The total amount of correct pairs is: {0}",correctPairs);
            Console.WriteLine("The decoder key is: {0}", (index2 +1) * (index6+1));
        }

        //Train of thought
        //Make a function that loops through the pairs
        //return bool if true        
        //I used Captain Coder's tutorial video to clear part 1 
        //https://www.youtube.com/watch?v=ApAC2ZdNYEQ 
    }

    //A class that compares the packets
    public static class Packets
    {
        //Compares two object elements. 
        //Checks if they are both ints, lists, or a mix of the two.
        public static int CompareElements(object first, object second)
        {
            switch(first, second)
            {
                case (int f, int s):
                    return Math.Sign(f - s); //if first is smaller it returns -1, if equal 0, if bigger 1
                case (List<object> f, List<object> s):
                    return CompareList(f, s); //first handle the lists
                case (int f, List<object> s):
                    return CompareList(new List<object>() { f }, s); //make the int object into a list and handle the lists
                case (List<object> f, int s):
                    return CompareList(f, new List<object>() { s }); //make the int object into a list and handle the lists
                default:
                    throw new Exception($"Could not compare elements {first} vs. {second}.");
            }
        }

        //Compares two lists for each index element
        public static int CompareList(List<object> first, List<object> second)
        {
            int maxIndex = Math.Min(first.Count, second.Count); //the max index is the count of the smallest list.
            for(int i =0; i< maxIndex; i++)
            {
                int diff = CompareElements(first[i], second[i]);
                if (diff < 0)
                    return -1; //left element is smaller than right
                else if (diff > 0)
                    return 1; //left element is bigger
            }
            //if we reached the end, we made it through the entire list (no smaller/bigger only same indexes)
            //if first list is smaller than second: -1, 0 if they're equal, and +1 if left is bigger
            return Math.Sign(first.Count - second.Count); 
        }
    }

    //A class that parses the input into a usable list
    public static class ListParser
    {
        //Puts every char of a string into a Queue
        public static Queue<char> StringToQueue(string input)
        {
            Queue<char> queue = new Queue<char>();
            foreach(char inp in input)
            {
                queue.Enqueue(inp);
            }
            return queue;
        }

        //Get a string and return a list
        public static List<object> Parse(string toParse)
        {
            Queue<char> data = StringToQueue(toParse);
            List<object> list = ParseList(data);
            return list;
        }

        //returns a digit of the char. (also works if the number consists of two chars (ex. 10))
        public static int ParseInt(Queue<char> data)
        {
            string intput = "";
            while(char.IsDigit(data.Peek()))
            {
                intput += data.Dequeue();
            }
            return int.Parse(intput);
        }

        //Goes through a char queue of a list (so first element is always [)
        public static List<object> ParseList(Queue<char> data)
        {
            List<object> elements = new List<object>();
            data.Dequeue(); //remove [ from queue (you always start with this)
            while(data.Peek() != ']')
            {
                if (data.Peek() == ',')
                    data.Dequeue();
                object inp = ParseElement(data);
                elements.Add(inp);
            }
            data.Dequeue(); //remove ] from queue
            return elements;
        }

        //Gets a  mystery char, if its a list -> parseList, if its a digit -> parseInt
        public static object ParseElement(Queue<char> data)
        {
            char next = data.Peek();
            if(char.IsDigit(next))
            {
                return ParseInt(data);
            }
            else if( next == '[')
            {
                return ParseList(data);
            }
            else
            {
                throw new Exception($"Expected int or list but found: {string.Join("", data)}");
            }
        }
    }
}
