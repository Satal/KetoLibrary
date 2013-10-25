using System.IO;
using KetoLibrary.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KetoLibrary.Test.Xml
{
    [TestClass]
    public class XsdValidatorTests
    {
        private readonly string _dataFilesLocation;
        public XsdValidatorTests()
        {
            _dataFilesLocation = AppDomain.CurrentDomain.BaseDirectory;
        }

        [TestMethod]
        public void AddValidSchema()
        {
            var validator = new XsdValidator();
            var actual = validator.AddSchema(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\PurchaseOrder.xsd"));
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsValidReturnsTrueForValidXml()
        {
            var validator = new XsdValidator();
            validator.AddSchema(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\PurchaseOrder.xsd"));
            var isValid = validator.IsValid(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\Valid PurchaseOrder1.xml"));
            Assert.IsTrue(isValid);
        }
        
        [TestMethod]
        public void IsValidReturnsTrueForReadOnlyValidXml()
        {
            var validator = new XsdValidator();
            validator.AddSchema(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\PurchaseOrder.xsd"));
            var isValid = validator.IsValid(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\Valid ReadOnlyPurchaseOrder1.xml"));
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidReturnsFalseForInvalidXml()
        {
            var validator = new XsdValidator();
            validator.AddSchema(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\PurchaseOrder.xsd"));
            var isValid = validator.IsValid(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\Invalid PurchaseOrder XML.xml"));
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidReturnsFalseForNonXsdXml()
        {
            var validator = new XsdValidator();
            validator.AddSchema(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\PurchaseOrder.xsd"));
            var isValid = validator.IsValid(Path.Combine(_dataFilesLocation, @"Xml\PurchaseOrder\Invalid PurchaseOrder1.xml"));
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MultipleSchemas()
        {
            var validator = new XsdValidator();
            validator.AddSchema(Path.Combine(_dataFilesLocation, @"Xml\Products\SchemaDoc1.xsd"));
            validator.AddSchema(Path.Combine(_dataFilesLocation, @"Xml\Products\SchemaDoc2.xsd"));
            var isValid = validator.IsValid(Path.Combine(_dataFilesLocation, @"Xml\Products\ValidXmlDoc1.xml"));
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void SchemaDoesntExist()
        {
            var validator = new XsdValidator();
            var isValid = validator.AddSchema(Path.Combine(_dataFilesLocation, @"Xml\Products\SchemaDoesntExist.xsd"));
            Assert.IsFalse(isValid);
        }
    }
}