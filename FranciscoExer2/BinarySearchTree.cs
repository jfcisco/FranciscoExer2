using System;
using System.Collections.Generic;
using System.Text;

namespace FranciscoExer2
{
    // My implementation of the Binary Search Tree (BST) data structure.
    public class BinarySearchTree
    {
        private Node Head;

        // Explicit default constructor
        public BinarySearchTree() { }

        // Inserts a given value into the BST, maintaining the BST ordering property. The public interface to the recursive Insert method.
        public void Insert(int value)
        {
            Insert(ref Head, value);
        }

        // Method overload to insert the value to the BST, recursively.
        private void Insert(ref Node node, int value)
        {
            // Base case: Create a new node if the given node is null.
            if (node == null)
            {
                node = new Node { Key = value };
                return;
            }
            else if (value < node.Key)
            {
                Insert(ref node.Left, value);
            }
            else if (value >= node.Key)
            {
                Insert(ref node.Right, value);
            }
        }

        // Gives the maximum value of the BST
        public int Maximum()
        {
            // Handle scenario when BST is empty.
            if (Head == null) throw new Exception("Error: BST is empty.");

            // Look for the maximum value iteratively.
            Node current = Head;

            while (current.Right != null)
            {
                current = current.Right;
            }

            return current.Key;
        }

        // Returns a string representation of the BST (i.e., lists its elements in ascending order).
        public string Print()
        {
            string toPrint = "";
            if (Head != null)
            {
                Print(ref toPrint, Head);
            }

            // Remove the last comma at the end of the string
            if (toPrint.Length > 0)
            {
                // Use the range operator to cut off the last character in the string (which is always a comma)
                toPrint = toPrint[0..^1];
            }

            return toPrint;
        }

        // Construct the string representation of the BST by adding the values of the nodes through inorder traversal.
        private void Print(ref string printStr, Node node)
        {
            if (node.Left != null)
            {
                Print(ref printStr, node.Left);
            }

            printStr += node.Key.ToString() + ",";
            
            if (node.Right != null)
            {
                Print(ref printStr, node.Right);
            }
        }

        // Nested node class to represent the nodes of the BST.
        class Node
        {
            public int Key;
            public Node Left;
            public Node Right;
        }
    }
}
