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
                Display($"Inserted {valueToInsert}!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetUserInput(out int valueToDelete))
            {
                if (Bst.Contains(valueToDelete))
                {
                    Bst.Delete(valueToDelete);
                    Display($"Deleted {valueToDelete}!");
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

            Display($"The minimum is: {Bst.GetMinimum()}");
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
