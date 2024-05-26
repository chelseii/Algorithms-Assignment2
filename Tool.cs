//CAB301 - Assignment 2 
//Tool ADT implementation


using System;
using System.Reflection;
using System.Text;


//Invariants: Name=!null and Number >=1

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
        return false ;
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
                number ++;
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
