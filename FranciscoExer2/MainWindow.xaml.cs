using FranciscoExer2.Exceptions;
using System;
using System.Windows;

namespace FranciscoExer2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BinarySearchTree Bst;
        private const int DISPLAY_LIMIT = 110; // Controls how many characters can be displayed.
        public MainWindow()
        {
            Bst = new BinarySearchTree();
            InitializeComponent();
        }

        /* === Event Handlers === */
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetUserInput(out int valueToInsert)) 
            {
                Bst.Insert(valueToInsert);
                Display($"{valueToInsert} inserted!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetUserInput(out int valueToDelete))
            {
                if (Bst.Contains(valueToDelete))
                {
                    Bst.Delete(valueToDelete);
                    Display($"{valueToDelete} deleted!");
                }
                else
                {
                    Display($"{valueToDelete} is not in the tree.");
                }
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            Display(Bst.Print());
        }

        private void Minimum_Click(object sender, RoutedEventArgs e)
        {
            if (Bst.IsEmpty)
            {
                Display("Minimum not found because the tree is empty.");
                return;
            }

            Display($"The minimum is {Bst.GetMinimum()}.");
        }

        private void Maximum_Click(object sender, RoutedEventArgs e)
        {
            if (Bst.IsEmpty)
            {
                Display("Maximum not found because the tree is empty.");
                return;
            }

            Display($"The maximum is {Bst.GetMaximum()}.");
        }

        private void Predecessor_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetUserInput(out int value))
            {
                if (!Bst.Contains(value))
                {
                    Display($"{value} is not in the tree.");
                    return;
                }

                try
                {
                    int predecessor = Bst.GetPredecessor(value);
                    Display($"The predecessor of {value} is {predecessor}");
                }
                catch(PredecessorNotFoundException)
                {
                    Display($"{value} does not have a predecessor!");
                }
            }
        }

        private void Successor_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetUserInput(out int value))
            {
                if (!Bst.Contains(value))
                {
                    Display($"{value} is not in the tree.");
                    return;
                }

                try
                {
                    int successor = Bst.GetSuccessor(value);
                    Display($"The successor of {value} is {successor}");
                }
                catch(SuccessorNotFoundException)
                {
                    Display($"{value} does not have a successor!");
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetUserInput(out int value))
            {
                if (Bst.Contains(value))
                {
                    Display($"{value} is in the tree!");
                }
                else
                {
                    Display($"{value} is not in the tree.");
                }
            }
        }

        /* === Utility Methods === */

        // Returns true if valid user input is retrieved, or false otherwise. Clears the input field in both cases.
        private bool TryGetUserInput(out int result)
        {
            try
            {
                int userInput = int.Parse(InputBox.Text);
                result = userInput;
                return true;
            }
            catch (FormatException)
            {
                Display("Invalid input. Please enter an integer.");
                result = default;
                return false;
            }
            catch (OverflowException)
            {
                Display($"Invalid input. Please enter an integer from {int.MinValue} to {int.MaxValue}.");
                result = default;
                return false;
            }
            finally
            {
                InputBox.Text = string.Empty;
            }
        }

        // Procedure to print to the program's output.
        private void Display(string msg)
        {
            // Display elispsis when there the binary tree has too many items.
            if (msg.Length > DISPLAY_LIMIT) { msg = msg[0..DISPLAY_LIMIT] + "..."; }
            
            // MessageBox.Show()
            StatusText.Text = msg;
        }
    }
}
