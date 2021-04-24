using FranciscoExer2;
using FranciscoExer2.Exceptions;
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
            Assert.AreEqual(testData, _bst.GetMaximum());
        }

        [TestMethod]
        public void Maximum_Success()
        {
            _bst.Insert(4);
            _bst.Insert(1);
            _bst.Insert(6);

            Assert.AreEqual(6, _bst.GetMaximum());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Maximum_Failure()
        {
            _bst.GetMaximum();
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
            Assert.AreEqual("", _bst.Print());
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

            Assert.AreEqual(-1, _bst.GetMinimum());
        }

        [TestMethod]
        public void Minimum_Success_LeftTreeMinimum()
        {
            InsertMany(new int[] { 0, -1, 2 });

            Assert.AreEqual(-1, _bst.GetMinimum());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Minimum_Failure_EmptyTree()
        {
            int actualMin = _bst.GetMinimum();
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        public void Successor_Success(int testInt)
        {
            InsertMany(new int[] { 1, 0, 2 });

            Assert.AreEqual(testInt + 1, _bst.GetSuccessor(testInt));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Successor_Failure_EmptyTree()
        {
            _bst.GetSuccessor(1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Successor_Failure_KeyNotFound()
        {
            InsertMany(new int[] { 1, 0, 2 });
            _bst.GetSuccessor(3);
        }

        [TestMethod]
        [ExpectedException(typeof(SuccessorNotFoundException))]
        public void Successor_Failure_NoSuccessor()
        {
            InsertMany(new int[] { 1, 0, 2 });
            _bst.GetSuccessor(2);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void Predecessor_Success(int testInt)
        {
            InsertMany(new int[] { 1, 0, 2 });

            Assert.AreEqual(testInt -1 , _bst.GetPredecessor(testInt));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Predecessor_Failure_EmptyTree()
        {
            _bst.GetPredecessor(0);
        }


        [TestMethod]
        [ExpectedException(typeof(PredecessorNotFoundException))]
        public void Predecessor_Failure_NoPredecessor()
        {
            InsertMany(new int[] { 1, 0, 2 });

            _bst.GetPredecessor(0);
        }

        // HELPER METHODS
        private void InsertMany(int[] testData)
        {
            foreach (int i in testData)
            {
                _bst.Insert(i);
            }
        }
    }
}
