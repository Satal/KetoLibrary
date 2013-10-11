using System;
using KetoLibrary.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace KetoLibrary.Test.Utilities
{
    [TestClass]
    public class ValueRetrieverTests
    {
        [TestMethod]
        public void ReturnsDefaultForNullChild()
        {
            var test = new TestClass();
            var actual = ValueRetriever.RetrieveValue(test, info => info.Parent.TestClassName);
            Assert.AreEqual(default(string), actual);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void ThrowsNonNullRefExceptions()
        {
            var test = new TestClass();
            ValueRetriever.RetrieveValue(test, t => t.ThrowsException().Length);
        }

        [TestMethod]
        public void ReturnsActualValueWhenExists()
        {
            var expected = "Test";
            var test = new TestClass() { TestClassName = expected };
            var actual = ValueRetriever.RetrieveValue(test, t => t.TestClassName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnsSpecifiedWhenNull()
        {
            var expected = "ExpectedValue";
            var test = new TestClass();
            var actual = ValueRetriever.RetrieveValue(test, t => t.TestClassName, expected);
            Assert.AreEqual(expected, actual);
        }

        private class TestClass
        {
            public string TestClassName { get; set; }
            public TestClass Parent { get; set; }
            public string ThrowsException()
            {
                throw new ArgumentException();
            }
        }
    }
}
