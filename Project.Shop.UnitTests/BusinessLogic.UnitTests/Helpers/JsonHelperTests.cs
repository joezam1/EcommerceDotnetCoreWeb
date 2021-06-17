using System;
using NUnit.Framework;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.BusinessLogic.Helpers;

namespace Project.Shop.UnitTests.BusinessLogicUnitTests.Helpers
{
    [TestFixture]
    public class JsonHelperTests
    {
        private Size _size;
        private IJsonHelper _jsonHelper;

        [SetUp]
        public void SetUp()
        {
            _size = new Size(){
                SizeId=1,
                Description = "size_a",
                IsInactive=false
            };
            _jsonHelper = new JsonHelper();
        }

        [Test]
        public void JsonSerializeObject_WhenCalled_ReturnsObjectAsString()
        {
            var result = _jsonHelper.JsonSerializeObject(_size);
            Assert.That(result, Is.TypeOf( typeof(String)));
        } 
    }

}