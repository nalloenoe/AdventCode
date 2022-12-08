using System;

namespace AdventCode3
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            int totalScore = 0;
            char foundLetter;
            
            //input
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode3\input.txt");
            string[] backpack = bigInput.Split(Environment.NewLine);

            for (int i = 0; i < backpack.Length-2; i+=3)
            {
                //foundLetter = program.findSimLetter(backpack[i]); 
                foundLetter = program.findSimLetter2(backpack[i], backpack[i + 1], backpack[i + 2]);
                totalScore += program.priorityScore(foundLetter); //Imidetaly calculate the score of the found similar letter and add it to the total score.
            }

            Console.WriteLine("The total score is: ");
            Console.WriteLine(totalScore);

        }

        //Train of thought:
        //split every single line
        //split this entry in half
        //find the similar letter
        //calculate score of that letter

        //find the similar letter in three backpacks.
        public char findSimLetter2(string backpack1, string backpack2, string backpack3)
        {
            //turn each backpack into a char array. (You could also not do this, and use the string length)
            char[] letters1 = backpack1.ToCharArray();
            char[] letters2 = backpack2.ToCharArray();
            char[] letters3 = backpack3.ToCharArray();
            
            //go through the first backpack
            for(int i = 0; i< letters1.Length; i++)
            {   //go through the second backpack
                for (int j = 0; j < letters2.Length; j++)
                {   //go through the third backpack
                    for (int k = 0; k<letters3.Length; k++)
                    {   //compare all the letters and find a similar one. Return this one immidiately.
                        if(letters1[i] == letters2[j] && letters2[j] == letters3[k])
                        { return letters1[i]; }
                    }
                }
            }

            //Code (should) never come here.
            return 'a';
        }

        //Find the similar letter in the first and second half of a backpack.
        public char findSimLetter(string backpack)
        {
            //divide the backpack into a char array
            char[] letters = backpack.ToCharArray();
            int index = letters.Length / 2; 

            //go through the first part of the backpack
            for(int i = 0; i < index; i++)
            {   //go through the second part of the backpack
                for(int j = index; j< letters.Length; j++)
                {   //compare all the letters with each other. If they are the same, immediately return.
                    if (letters[i] == letters[j])
                    {
                        return letters[i];                        
                    }
                }    
            }

            //Code (should) never come here.
            return 'a';
        }

        //Calculate the 'priority Score' of a letter.
        public int priorityScore(char letter)
        {
            //This brings all the letters to 1-26 (also uppers)
            int score = (int)letter % 32;  //https://stackoverflow.com/questions/20044730/convert-character-to-its-alphabet-integer-position
            
            //The upper letters should be +26 so here is a check.
            if(Char.IsUpper(letter))
            { score += 26; }

            return score;
        }

    }
}
