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
        [ExpectedException(typeof(InvalidOperationException))]
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

            int[] expectedInts = { 1, 4, 6 };
            string expectedString = string.Join(',', expectedInts);

            Assert.AreEqual(expectedString, _bst.Print());
        }

        [TestMethod]
        public void Print_Success_EmptyTree()
        {
            Assert.AreEqual("", _bst.Print());
        }


        [TestMethod]
        [DataRow(5)] // O child
        [DataRow(6)] // 1 left child
        [DataRow(4)] // 1 right child
        [DataRow(2)] // 2 children, succ. 1 child
        [DataRow(0)] // 2 children, succ. 0 children
        public void Delete_Success(int toDelete)
        {
            int[] testData = { 2, 0, -1, 1, 6, 4, 5 };

            InsertMany(testData);

            _bst.Delete(toDelete);
            string actualContents = _bst.Print();

            testData = testData.Where(i => i != toDelete).ToArray();
            Array.Sort(testData);
            string expectedContents = string.Join(",", testData);
            Assert.AreEqual(expectedContents, actualContents);
        }

        [TestMethod]
        public void Delete_Failure_EmptyTree()
        {
            _bst.Delete(1);

            try
            {
                _bst.Maximum();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Error: BST is empty.", e.Message);
            }
        }

        [TestMethod]
        public void Delete_Failure_ValueDoesNotExist()
        {
            InsertMany(new int[] { 1, 2, 3 });

            _bst.Delete(4);
            string bstContents = _bst.Print();
            Assert.AreEqual("1,2,3", bstContents);
        }

        [TestMethod]
        public void Delete_Success_ValueIsDuplicated()
        {
            // TODO
        }

        [TestMethod]
        public void Minimum_Success_RootMinimum()
        {
            InsertMany(new int[] { -1, 2, 0 });

            int actualMin = _bst.Minimum();
            Assert.AreEqual(-1, actualMin);
        }

        [TestMethod]
        public void Minimum_Success_LeftTreeMinimum()
        {
            InsertMany(new int[] { 0, -1, 2 });

            int actualMin = _bst.Minimum();
            Assert.AreEqual(-1, actualMin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Minimum_Failure_EmptyTree()
        {
            int actualMin = _bst.Minimum();
        }

        // Helper method
        private void InsertMany(int[] testData)
        {
            foreach (int i in testData)
            {
                _bst.Insert(i);
            }
        }
    }
}
