using Xunit;
using System;
using DAL;
namespace DAL.Test
{
    public class ItemUnitDALTest
    {

        ItemDAL itemDAL = new ItemDAL();
        [Fact]
        public void GetListItemsTest()
        {
            Assert.NotNull(itemDAL.GetListItems());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAnItemByIdTest(int? itemID)
        {
            Assert.NotNull(itemDAL.GetItemByID(itemID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void GetAnItemByIdTest1(int? itemID)
        {
            Assert.Null(itemDAL.GetItemByID(itemID));
        }

    }
}
