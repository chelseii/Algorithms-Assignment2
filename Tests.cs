using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace A2_Project_Files
{
    public class BTreeNode
    {
        public ITool tool;
        public BTreeNode? lchild; // reference to its left child 
        public BTreeNode? rchild; // reference to its right child

        public BTreeNode(ITool tool)
        {
            this.tool = tool;
            this.lchild = null;
            this.rchild = null;
        }
    }
    internal class Tests
    {
        static int counter;
        static int count;
        static BTreeNode root;
        private int number;

        public int Number
        {
            get { return number; }   // get method
        }
        static void Main(string[] args)
        {
            int executions = 20;

            Console.WriteLine("Size" + "    |      " + "Count");
            Console.WriteLine(("---------------------"));

            for (int n = 1000; n <= 20000; n += 1000)
            {
                double total = 0;
                for (int i = 1; i <= executions; i++)
                {
                    counter = 0;
                    count = n;

                    int[] test = randArray(n);
                    root = Tree(test);
                    ITool[] Array = ToArray();
                    total = total + counter;
                }
                double avg = total / executions;
                if (n < 10000)
                {
                    Console.WriteLine((n + "    |      " + avg));
                }
                if (n > 10000)
                {
                    Console.WriteLine((n + "   |      " + avg));
                }
            }
        }

        static int[] randArray(int n)
        {
            int i;
            int[] array = new int[n];
            int seed = (int)DateTime.Now.Ticks;
            Random rand = new Random(seed);

            for (i = 0; n - 1 > i; i++)
            {
                array[i] = rand.Next(int.MinValue, int.MaxValue);

            }

            array.ToArray();
            return array;
        }
        public static ITool[] ToArray()
        {
            //To be completed by students
            int i = 0;
            ITool[] array = new Tool[count];
            InOrderTraverse(ref i, root, array);
            return array;
        }
        private static void InOrderTraverse(ref int i, BTreeNode? root, ITool[] arr)
        {
            if (root != null)
            {
                InOrderTraverse(ref i, root.lchild, arr);
                arr[i] = root.tool; i++;
                counter++;
                InOrderTraverse(ref i, root.rchild, arr);
            }
        }
        public bool IsEmpty()
        {
            //To be completed by students
            if (Number == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static BTreeNode Tree(int[] test)
        {
            BTreeNode root = null;
            for (int i = 0; i < test.Length; i++)
            {
                var item = test[i];
                root = Insert(root, new Tool(item.ToString(), 1));
            }
            return root;
        }
        static BTreeNode Insert(BTreeNode root, ITool tool)
        {
            if (root == null)
            {
                root = new BTreeNode(tool);
                return root;
            }

            BTreeNode ptr = root;
            while (true)
            {
                if (ptr.tool.Name.CompareTo(tool.Name) == 0)
                {
                    return root;
                }
                else if (ptr.tool.Name.CompareTo(tool.Name) < 0)
                {
                    if (ptr.lchild == null)
                    {
                        ptr.lchild = new BTreeNode(tool);
                        return root;
                    }
                    else
                    {
                        ptr = ptr.lchild;
                    }
                }
                else
                {
                    if (ptr.rchild == null)
                    {
                        ptr.rchild = new BTreeNode(tool);
                        return root;
                    }
                    else
                    {
                        ptr = ptr.rchild;
                    }
                }
            }
            return ptr;
        }
    }
    partial class Tool : ITool
    {
        private string name;
        private int number;
        private string[] borrowerList;

        //constructor 
        public Tool(string name, int number = 0)
        {
            if (name == null)
                throw new ArgumentNullException("Name is null");
            else
            {
                this.name = name;
                this.number = number;
                borrowerList = new string[0];
            }
        }

        //get the name of this tool
        public string Name
        {
            get { return name; }   // get method
        }

        //get the number of this tool currently available in the tool library
        public int Number
        {
            get { return number; }   // get method
        }

        //check if a person is in the borrower list of this tool (is holding this tool)
        //Pre-condition: nil
        //Post-condition: return true if the person is in the borrower list; return false otherwise. The information about this tool remains unchanged.
        public bool IsInBorrowerList(string personName)
        {
            //To be completed by students
            for (int i = 0; borrowerList.Length > i; i++)
            {
                string current = borrowerList[i];
                if (current.Equals(personName))
                {
                    return true;
                }
            }
            return false;
        }


        //add a person to the borrower list
        //Pre-condition: the borrower is not in the borrower list and Number > 0
        //Post-condition: the borrower is added to the borrower list and new Number = old Number - 1
        public bool AddBorrower(string personName)
        {
            //To be completed by students
            if (number > 0 & IsInBorrowerList(personName) == false)
            {
                borrowerList[borrowerList.Length - 1] = personName;
                number = number - 1;
                return true;
            }
            return false;
        }


        //remove a borrower from the borrower list
        //Pre-condition: the borrower is in the borrower list
        //Post-condition: the borrower is removed from the borrower list and new Number = old Number + 1
        public bool RemoveBorrower(string personName)
        {
            //To be completed by students
            int length = borrowerList.Length;
            for (int i = 0; length > i; i++)
            {
                if (borrowerList[i] == personName)
                {
                    for (int j = 0; length - 1 > j; j++)
                    {
                        borrowerList[j] = borrowerList[j + 1];
                    }
                    number++;
                    borrowerList[length - 1] = null;
                    return true;
                }
            }
            return false;
        }


        //Compare this tool's name to another tool's name 
        //Pre-condition: anotherTool =! null
        //Post-condition:  return -1, if this tool's name is less than another tool's name by alphabetical order
        //                 return 0, if this tool's name equals to another tool's name by alphabetical order
        //                 return +1, if this tool's name is greater than another tool's name by alphabetical order

        public int CompareTo(ITool? anotherTool)
        {
            //To be completed by students
            Tool tool = anotherTool as Tool;
            if (this.Name.CompareTo(anotherTool.Name) < 0)
            {
                return -1;
            }
            else
            {
                if (this.Name.CompareTo(anotherTool.Name) == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }


        //Return a string containing the name and the number of this tool currently in the tool library 
        //Pre-condition: nil
        //Post-condition: A string containing the name and number of this tool is returned

        public override string ToString()
        {
            //To be completed by students
            return name + "," + number;
        }
    }
    public interface ITool
    {

        // get the name of this tool

        string Name // get the name of this tool
        {
            get;
        }

        //get the number of this tool currently available in the tool library
        int Number
        {
            get;
        }

        //check if a person is in the borrower list of this tool (is holding this tool)
        //Pre-condition: nil
        //Post-condition: return true if the person is in the borrower list; return false otherwise. The information about this tool remains unchanged.
        bool IsInBorrowerList(string personName);


        //add a person to the borrower list
        //Pre-condition: the borrower is not in the borrower list and Number > 0
        //Post-condition: the borrower is added to the borrower list and New Number = Old Number - 1
        bool AddBorrower(string personName);


        //remove a borrower from the borrower list
        //Pre-condition: the borrower is in the borrower list
        //Post-condition: the borrower is removed from the borrower list and and new Number = old Number + 1
        bool RemoveBorrower(string personName);


        //Compare this tool's name to another tool's name 
        //Pre-condition: anotherTool =! null
        //Post-condition:  return -1, if this tool's name is less than another tool's name by alphabetical order
        //                 return 0, if this tool's name equals to another tool's name by alphabetical order
        //                 return +1, if this tool's name is greater than another tool's name by alphabetical order
        int CompareTo(ITool? anotherTool);


        //Return a string containing the name and the number of this tool currently in the tool library 
        //Pre-condition: nil
        //Post-condition: A string containing the name and number of this tool is returned

        string ToString();

    }
}
