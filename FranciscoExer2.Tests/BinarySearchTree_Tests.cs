using FranciscoExer2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FranciscoExer2.Tests
{
    [TestClass]
    public class BinarySearchTree_Tests
    {
        private BinarySearchTree _bst;
        
        [TestInitialize]
        public void InitializeTests()
        {
            _bst = new BinarySearchTree();
            Assert.IsNotNull(_bst);
        }
                
        [TestMethod]
        public void Insert_Success()
        {
            int testData = 4;
            _bst.Insert(testData);
            Assert.AreEqual(testData, _bst.Maximum());
        }

        [TestMethod]
        public void Maximum_Success()
        {
            _bst.Insert(4);
            _bst.Insert(1);
            _bst.Insert(6);

            Assert.AreEqual(6, _bst.Maximum());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Maximum_Failure()
        {
            _bst.Maximum();
        }

        [TestMethod]
        public void Print_Success_NonEmptyTree()
        {
            _bst.Insert(4);
            _bst.Insert(1);
            _bst.Insert(6);
            Assert.AreEqual("1,4,6", _bst.Print());
        }

        [TestMethod]
        public void Print_Success_EmptyTree()
        {
            Assert.AreEqual("", _bst.Print());
        }
    }
}
