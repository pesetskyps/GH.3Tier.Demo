using System;
using NUnit.Framework;

namespace GH.Northwind.UnitTest
{
    [TestFixture] 
    public class UnitTestGhin
    {
    	//test
        [Test]
        public void True()
        {
            Assert.AreEqual(1,1);
        }

        //test
        [Test]
        public void False()
        {
            Assert.AreEqual(0, 1);
        }
    }
}
