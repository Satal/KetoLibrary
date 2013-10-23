using KetoLibrary.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KetoLibrary.Test.Xml
{
    [TestClass]
    public class XsdValidatorTests
    {
        [TestMethod]
        public void AddValidSchema()
        {
            var validator = new XsdValidator();
            var actual = validator.AddSchema(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\PurchaseOrder.xsd");
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsValidReturnsTrueForValidXml()
        {
            var validator = new XsdValidator();
            validator.AddSchema(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\PurchaseOrder.xsd");
            var isValid = validator.IsValid(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\Valid PurchaseOrder1.xml");
            Assert.IsTrue(isValid);
        }
        
        [TestMethod]
        public void IsValidReturnsTrueForReadOnlyValidXml()
        {
            var validator = new XsdValidator();
            validator.AddSchema(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\PurchaseOrder.xsd");
            var isValid = validator.IsValid(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\Valid ReadOnlyPurchaseOrder1.xml");
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidReturnsFalseForInvalidXml()
        {
            var validator = new XsdValidator();
            validator.AddSchema(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\PurchaseOrder.xsd");
            var isValid = validator.IsValid(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\Invalid PurchaseOrder XML.xml");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidReturnsFalseForNonXsdXml()
        {
            var validator = new XsdValidator();
            validator.AddSchema(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\PurchaseOrder.xsd");
            var isValid = validator.IsValid(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\PurchaseOrder\Invalid PurchaseOrder1.xml");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void MultipleSchemas()
        {
            var validator = new XsdValidator();
            validator.AddSchema(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\Products\SchemaDoc1.xsd");
            validator.AddSchema(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\Products\SchemaDoc2.xsd");
            var isValid = validator.IsValid(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\Products\ValidXmlDoc1.xml");
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void SchemaDoesntExist()
        {
            var validator = new XsdValidator();
            var isValid = validator.AddSchema(@"C:\Users\satal_000\Documents\GitHub\XmlFileExplorer\trunk\Test Files\Products\SchemaDoesntExist.xsd");
            Assert.IsFalse(isValid);
        }
    }

}
