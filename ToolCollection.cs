// CAB301 - Assignment 2
// ToolCollection ADT implementation


using System;
//A class that models a node of Binary Tree
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

// Invariants: no duplicate tools in this tool collection (all the tools in this tool collection have different names) and the number of tools in this tool collection is greater than equals to 0


partial class ToolCollection : IToolCollection
{
	private BTreeNode? root; // tools are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of different tools currently stored in this tools collection 


    // constructor - create an object of ToolCollection object
    public ToolCollection()
    {
        root = null;
        count = 0;
    }

    // get the number of tools in this movie colllection 
    public int Number { get { return count; } }



    // Check if this tool collection is empty
    // Pre-condition: nil
    // Post-condition: return true if this tool collection is empty; otherwise, return false. This tool collection remains unchanged and new Number = old Number
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


    // Insert a new tool into this tool collection
    // Pre-condition: the new tool is not in this tool collection
    // Post-condition: the new tool is added into this tool collection, new Number = old Number + 1 and return true; otherwise, the new tool is not added into this tool collection, new Number = old Number and return false.

    public bool Insert(ITool tool)
	{
        //To be completed by students
        BTreeNode? ptr = root;
        if (IsEmpty() == true)
        {
            count++;
            root = new BTreeNode(tool);
            return true;
        }
        if (ptr.tool.Name.CompareTo(tool.Name) == 0)
        {
            return false;
        }
        if (ptr.tool.Name.CompareTo(tool.Name) < 0)
        {
            if (ptr.lchild == null)
            {
                count++;
                ptr.lchild= new BTreeNode(tool);
                return true;
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
                count++;
                ptr.rchild = new BTreeNode(tool) ;
                return true;
            }
        }
        return false;
    }



    // Delete a tool from this tool collection
    // Pre-condition: nil
    // Post-condition: the tool is removed out of this tool collection, new Number = old Number - 1 and return true, if the tool is present in this tool collection; 
    // otherwise, this tool collection remains unchanged, and new Number = old Number, and return false, if the tool is not present in this tool collection.

    public bool Delete(ITool tool)
    {
        //To be completed by students
        BTreeNode ptr = root; // search reference
        BTreeNode parent = null; // parent of ptr

        if (ptr == null)
        {
            return false;
        }

        if (!ptr.tool.Equals(tool))
        {
            return false;
        }

        while ((ptr != null) && (tool.CompareTo(ptr.tool) != 0))
        {
            parent = ptr;
            if (tool.CompareTo(ptr.tool) < 0) // move to the left child of ptr
                ptr = ptr.lchild;
            else
                ptr = ptr.rchild;
        }

        if (ptr != null) // if the search was successful
        {
            // case 3: item has two children
            if ((ptr.lchild != null) && (ptr.rchild != null))
            {
                // find the right-most node in left subtree of ptr
                if (ptr.lchild.rchild == null) // a special case: the right subtree of ptr.LChild is empty
                {
                    ptr.tool = ptr.lchild.tool;
                    ptr.lchild = ptr.lchild.lchild;
                }
                else
                {
                    BTreeNode p = ptr.lchild;
                    BTreeNode pp = ptr; // parent of p
                    while (p.rchild != null)
                    {
                        pp = p;
                        p = p.rchild;
                    }
                    // copy the item at p to ptr
                    ptr.tool = p.tool;
                    pp.rchild = p.lchild;
                }
            }
            else // cases 1 & 2: item has no or only one child
            {
                BTreeNode c;
                if (ptr.lchild != null)
                { 
                    c = ptr.lchild; 
                }
                else
                { 
                    c = ptr.rchild; 
                }

                // remove node ptr
                if (ptr == root) //need to change root
                { 
                    root = c; 
                }
                else
                {
                    if (ptr == parent.lchild)
                    { 
                        parent.lchild = c; 
                    }
                    else
                    { 
                        parent.rchild = c;
                    }
                }
            }
        }
        count--;
        return true;
    }



    // Search for a tool by its name in this tool collection  
    // pre: nil
    // post: return the reference of the tool object if the tool is in this tool collection;
    //	     otherwise, return null. New Number = old Number.
    public ITool? Search(string toolName)
	{
        //To be completed by students;
        BTreeNode? r = root;
        if (root == null)
        {
            return null;
        }
        if (r != null)
        {
            if (!toolName.Equals(r.tool.Name))
            {
                return null;
            }
            if (toolName.CompareTo(r.tool.Name) == 0)
            {
                return r.tool;
            }
            else if (0 > toolName.CompareTo(r.tool.Name))
            {
                return r.tool;
                r = r.lchild;
            }
            else 
            {
                r = r.rchild;
                return r.tool;

            }
        }
        return null;
    }


    // Return an array that contains all the tools in this tool collection and the tools in the array are sorted in the dictionary order by their names
    // Pre-condition: nil
    // Post-condition: return an array that contains all the tools in this tool collection and the tools in the array are sorted in alphabetical order by their names and new Number = old Number.
    public ITool[] ToArray()
	{
        //To be completed by students
        int i = 0;
        ITool[] array = new Tool[this.Number];
        InOrderTraverse(ref i, root, array);
        return array;
    }
    private void InOrderTraverse(ref int i, BTreeNode? root, ITool[] arr)
    {
        if (root != null)
        {
            InOrderTraverse(ref i, root.lchild, arr);
            arr[i] = root.tool; i++;
            InOrderTraverse(ref i, root.rchild, arr);
        }
    }

    // Clear this tool collection
    // Pre-condition: nil
    // Post-condition: all the tools in this tool collection are removed from this tool collection and Number = 0. 
    public void Clear()
	{
        root = null;
		count = 0;
	}
}