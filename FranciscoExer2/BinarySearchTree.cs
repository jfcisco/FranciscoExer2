using FranciscoExer2.Exceptions;
using System;
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

        // Returns the minimum value inserted to the BST.
        // Throws InvalidOperationException if tree is empty.
        public int GetMinimum()
        {
            if (Root == null) { throw new InvalidOperationException("Cannot find minimum if BST is empty.");  }

            return GetMinimum(Root).Key;
        }

        // Generalized GetMinimum method
        // Returns the node containing the minimum node of a subtree
        private Node GetMinimum(Node root)
        {
            if (root == null) throw new InvalidOperationException("Cannot find minimum if BST is empty.");

            // Look for the minimum value iteratively.
            Node current = root;

            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }

        // Returns the maximum value of the BST
        // Throws InvalidOperationException if tree is empty.
        public int GetMaximum()
        {
            // Handle scenario when BST is empty.
            if (Root == null) throw new InvalidOperationException("Cannot find maximum if BST is empty.");

            return GetMaximum(Root).Key;
        }

        // Returns the maximum node of the subtree with the given root, or null if the maximum node does not exist;
        private Node GetMaximum(Node root)
        {
            // Look for the maximum value iteratively.
            Node current = root;

            while (current.Right != null)
            {
                current = current.Right;
            }

            return current;
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
                // By this point node.Left and node.Right are not null.

                Node delSuccess = GetMinimum(node.Right);

                int tempKey = delSuccess.Key;
                // TODO: Can make this more elegant, this is a hack:
                // Delete already covers both cases (0 child and 1 child)
                Delete(node, delSuccess.Key);
                node.Key = tempKey;

                return node;
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
        /* Gives the predecessor of the value in the list.
         * Throws InvalidOperationException if the tree is empty.
         * Throws KeyNotFound if no node has a key matching the given value.
         * Throws PredecessorNotFoundException if the given value has no predecessor (i.e., minimum is passed in).
         */
        public int GetPredecessor(int value)
        {
            if (Root == null) { throw new InvalidOperationException();  }

            // Search the entire BST for the node with the given value.
            Node node = Search(Root, value);
            return GetPredecessor(node).Key;
        }

        private Node GetPredecessor(Node node)
        {
            if (node.Left != null)
            {
                return GetMaximum(node.Left);
            }

            // Look for the predecessor higher in the tree
            Node current = node;
            Node higherNode = GetParent(Root, node);

            while(higherNode != null && higherNode.Left == current)
            {
                current = higherNode;
                higherNode = GetParent(Root, current);
            }

            // Return higherNode only if not null, otherwise throw an exception.
            return higherNode ?? throw new PredecessorNotFoundException();
        }

        /* Gives the successor of the value in the list
           Throws InvalidOperationException if the tree is empty.
           Throws KeyNotFoundException if the value is not a key in the BST.
           Throws SuccessorNotFoundException if successor cannot be found.*/
        public int GetSuccessor(int value)
        {
            if (Root == null) { throw new InvalidOperationException(); }

            // Search the whole subtree for the integer after value in the BST.
            Node node = Search(Root, value);
            return GetSuccessor(node).Key;
        }

        private Node GetSuccessor(Node node)
        {
            if (node.Right != null)
            {
                return GetMinimum(node.Right);
            }

            // Look for the successor higher in the tree
            Node currentNode = node;
            Node higherNode = GetParent(Root, node);

            while (higherNode != null && higherNode.Right == currentNode)
            {
                currentNode = higherNode;
                higherNode = GetParent(Root, currentNode);
            }

            // Return higherNode only if it's not null, otherwise thrown an exception.
            return higherNode ?? throw new SuccessorNotFoundException();
        }

        // Finds the parent of the node in the BST given with the root root.
        // Returns the parent of the node, or null if the Root of the overall BST is given.
        private Node GetParent(Node root, Node node)
        {
            // Only the root node does not have a parent.
            if (node == Root) { return null; }

            // Base case: Parent found!
            if (root.Left == node || root.Right == node) 
            {
                return root;
            }

            // Traverse to find the parent of the node.
            if (node.Key < root.Key)
            {
                return GetParent(root.Left, node);
            }
            else // node.Key >= root.Key
            {
                return GetParent(root.Right, node);
            }
        }

        // Searches for the Node with a key matching the given value in the subtree with the given root, or throws a KeyNotFoundException if no such node exists.
        private Node Search(Node root, int value)
        {
            // Base Case
            if (value == root.Key) { return root; }

            // If a leaf was reached, throw not found exception
            if (root.Left == null && root.Right == null)
            {
                throw new KeyNotFoundException();
            }

            // Traverse subtree to find Node with key matching value.
            if (value < root.Key)
            {
                return Search(root.Left, value);
            }
            else // value > root.Key
            {
                return Search(root.Right, value);
            }

        }

        bool IsEmpty => Root == null;

        // Nested node class to represent the nodes of the BST.
        class Node
        {
            public int Key;
            public Node Left;
            public Node Right;
        }
    }
}
