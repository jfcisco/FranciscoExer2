using System;
using System.Collections.Generic;
using System.Text;

namespace FranciscoExer2
{
    // My implementation of the Binary Search Tree (BST) data structure.
    public class BinarySearchTree
    {
        private Node Root;

        // Declares an empty tree
        public BinarySearchTree() { }

        // Inserts a given value into the BST, maintaining the BST ordering property. The public interface to the recursive Insert method.
        public void Insert(int value)
        {
            Insert(ref Root, value);
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
            if (Root == null) throw new Exception("Error: BST is empty.");

            // Look for the maximum value iteratively.
            Node current = Root;

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
            if (Root != null)
            {
                Print(ref toPrint, Root);
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

        // Deletes the node with a key matching the given value.
        public void Delete(int value)
        {
            // Update the Root node
            Root = Delete(Root, value);
        }

        // Private recursive delete method
        // Returns the reference to the node or the new node took its place
        private Node Delete(Node node, int value)
        {
            // Node is null if either the value cannot be found or the tree is empty.
            if (node == null) { return null; }

            // node is the node to be deleted.
            if (value == node.Key)
            {
                // Case 1: Node has no children.
                if (node.Left == null && node.Right == null)
                {
                    return null; // Replace node with null reference.
                }
                // Case 2: Node has one child (either left or right)
                // 2.a. Only has left child.
                if (node.Left != null && node.Right == null)
                {
                    // Replace node with left child
                    return node.Left;
                }

                //2.b. Only has right child.
                if (node.Right != null && node.Left == null)
                {
                    // Replace node with right child
                    return node.Right;
                }

                // Case 3: Node has two children (successor has 0 or 1 child).
                // By this point node.Left and node.Right is not null.
                // 2^2 = 4 combinations

                // Find successor of node and check its children

                //3.a. Successor has no children

                //3.b. Successor has 1 child
                return new Node();


            }
            else if (value < node.Key)
            {
                // Traverse the node's left subtree to find the node to be deleted.
                // Update the node's left child.
                node.Left = Delete(node.Left, value);
                return node;
            }
            else // if value > node.Key
            {
                // Traverse the node's left subtree to find the node to be deleted.
                // Update the node's right child.
                node.Right = Delete(node.Right, value);
                return node;
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
