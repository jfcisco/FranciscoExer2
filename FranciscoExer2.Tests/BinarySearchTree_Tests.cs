using FranciscoExer2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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


        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void Delete_Success(int toDelete)
        {
            int[] testData = { 1, 2, 3 };

            foreach(int i in testData)
            {
                _bst.Insert(i);
            }

            _bst.Delete(toDelete);

            string actualContents = _bst.Print();

            testData = testData.Where(i => i != toDelete).ToArray();
            string expectedContents = string.Join(",", testData);
            Assert.AreEqual(expectedContents, actualContents);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_Failure_EmptyTree()
        {
            _bst.Delete(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_Failure_ValueDoesNotExist()
        {
            int[] testData = { 1, 2, 3 };

            foreach (int i in testData)
            {
                _bst.Insert(i);
            }

            _bst.Delete(4);
        }

        [TestMethod]
        public void Delete_Success_ValueIsDuplicated()
        {
            // TODO
        }
    }
}
