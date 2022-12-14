using System;
using System.Collections.Generic;

namespace AdventCode7
{
    class Program
    {
        /*
        //IDictionary<string, int> fileSystems = new Dictionary<string, int>();
        //IDictionary<string, string> hierarchy = new Dictionary<string, string>();
        //int finalScore = 0;
        //Hierarchy works as follows. The keys are the children and the values the parent
        */
        Tree<DirFile> filesystem = new Tree<DirFile>();
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("Hello World!");
            string bigInput = System.IO.File.ReadAllText(@"C:\Users\nallo\OneDrive\Documenten\AdventofCode2022\AdventCode\AdventCode7\inputtest.txt");
            string[] inputLines = bigInput.Split(Environment.NewLine);

            
            program.filesystem.Root = new TreeNode<DirFile>()
            {
                Data = new DirFile("/", 0),
                Parent = null
            };

            program.filesystem.Root.Children = new List<TreeNode<DirFile>>();

            /*
            //program.fileSystems.Add("/", 0);
            //program.readInput(inputLines);
            //program.weirdHierarchy();
            //program.calcCorrectFileSpace();
            //foreach (var kvpair in program.fileSystems)
            //{
            //    if (kvpair.Value < 100000)
            //    {
            //        program.finalScore += kvpair.Value;
            //    }
            //}
            //Console.WriteLine(program.finalScore);
            //Console.WriteLine("Key: {0}, Value: {1}", kvpair.Key, kvpair.Value);
            //foreach (var kvpair in program.hierarchy)
            //    Console.WriteLine("Key: {0}, Value: {1}", kvpair.Key, kvpair.Value);
            */
        }

        //Train of thought:
        //Make a function for every possible command (cd, ls etc)
        //put all the directories in an array of some sort
        //Calculate the size of the array
        //if smaller than 100.000 add to a total. When smaller than 100.000, print to console.
        /*
        //public void readInput(string[] inputLines)
        //{
        //    string currentDir = "";
        //    for (int i = 0; i < inputLines.Length; i++)
        //    {
        //        string[] tempLines = inputLines[i].Split(' ');
        //        if (tempLines[0] == "$")
        //        {
        //            if (tempLines[1] == "cd") //its cd
        //            {
        //                if (tempLines[2] == "..")
        //                {
        //                    string upperDir;
        //                    if (hierarchy.TryGetValue(currentDir, out upperDir))
        //                    {
        //                        currentDir = upperDir;
        //                    }
        //                }
        //                else
        //                {
        //                    //if(!hierarchy.ContainsKey(tempLines[2]))
        //                    //  hierarchy.Add(tempLines[2], currentDir); //Add that currentDir is a parrent of templine
        //                    currentDir = tempLines[2];
        //                }
        //            }
        //            //else, its a ls
        //        }
        //        else
        //        {

        //            if (tempLines[0] == "dir")
        //            {
        //                if (!fileSystems.ContainsKey(tempLines[1]))
        //                    fileSystems.Add(tempLines[1], 0);
        //                if (!hierarchy.ContainsKey(tempLines[1]))
        //                    hierarchy.Add(tempLines[1], currentDir);
        //            }
        //            else
        //            {
        //                if (fileSystems.ContainsKey(currentDir))
        //                {
        //                    fileSystems[currentDir] += int.Parse(tempLines[0]); //add filesize
        //                }
        //            }
        //        }
        //    }
        //}
        

        //public void weirdHierarchy()
        //{
        //    string childChildKey;
        //    string childParent;
        //    string parentVal;

        //    foreach (var kvp in hierarchy)
        //    {
        //        if (hierarchy.TryGetValue(kvp.Key, out parentVal))
        //        {
        //            childParent = kvp.Key;
        //            foreach (var kvpair in hierarchy)
        //            {
        //                if (childParent == kvpair.Value)
        //                {
        //                    childChildKey = kvpair.Key;
        //                    //hierarchy.Add(childChildKey, parentVal);
        //                    if (fileSystems.ContainsKey(parentVal) && fileSystems.ContainsKey(childChildKey))
        //                        fileSystems[parentVal] += fileSystems[childChildKey];
        //                    break;
        //                }
        //            }

        //            //hierarchy.Add
        //        }
        //        //check if a key is also a value 
        //    }
        //}
        //public void calcCorrectFileSpace()
        //{
        //    int childValue;
        //    //int parentValue = 0;
        //    string childKey;
        //    string parKey;


        //    foreach (var kvp in hierarchy)
        //    {
        //        childKey = kvp.Key;
        //        parKey = kvp.Value;
        //        if (fileSystems.TryGetValue(childKey, out childValue))
        //        {
        //            if (fileSystems.ContainsKey(parKey))
        //            {
        //                //This still has a mistake in it. 
        //                //As the score of the child of this child is not yet added to the parent.
        //                fileSystems[parKey] += childValue;
        //            }
        //        }
        //    }
        //}
        */

        public void ReadInput(string[] input)
        {
            int height = 0;
            for(int i = 0; i< input.Length; i++)
            {
                string[] inputParts = input[i].Split(' ');
                if(inputParts[0] == "$")
                {
                    //command
                    if(inputParts[1] == "cd")
                    {
                        //do a CD command
                    }
                }
                if(inputParts[0] == "dir")
                {
                    //add child
                }
                else
                {
                    //add file size to current node
                    //filesystem.Root.Children[height].Data.DirFile.Size
                }
            }
        }

    }



    //See youtube video
    public class Tree<T>
    {
        public TreeNode<T> Root { get; set; }
    }
    //https://www.youtube.com/watch?v=K0-qs--naUo
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Parent { get; set; }
        public List<TreeNode<T>> Children { get; set; }
        public int GetHeight()
        {
            int height = 1;
            TreeNode<T> current = this;
            while(current.Parent != null)
            {
                height++;
                current = current.Parent;
            }
            return height;
        }
    }

    //see youtube video
    public class DirFile
    {
        public string Name { get; set; }//name of directorie
        public int FileSize { get; set; }

        public DirFile() { }
        public DirFile(string name, int fileSize)
        {
            Name = name;
            FileSize = fileSize;
        }
    }
}
