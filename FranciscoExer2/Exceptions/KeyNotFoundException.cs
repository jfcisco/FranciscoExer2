using System;
using System.Collections.Generic;
using System.Text;

namespace FranciscoExer2.Exceptions
{
    /// <summary>
    /// Exception for when the user tries to search for a given key that is not yet inserted in the BST.
    /// </summary>
    public class KeyNotFoundException : Exception { }
}
