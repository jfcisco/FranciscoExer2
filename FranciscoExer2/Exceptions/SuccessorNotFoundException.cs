using System;
using System.Collections.Generic;
using System.Text;

namespace FranciscoExer2.Exceptions
{
    /// <summary>
    /// Exception for when the user tries to find the successor of a maximum node (i.e, a node with no successor).
    /// </summary>
    public class SuccessorNotFoundException : Exception { }
}
