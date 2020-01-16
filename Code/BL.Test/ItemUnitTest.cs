using System;
using Xunit;
using Persistence.MODEL;
using BL;

namespace BL.Test{
    public class ItemUnitTest{
        private ItemBL itemBL = new ItemBL();
        [Fact]
        public void GetListitemsTest()
        {
            Assert.NotNull(itemBL.GetListItems());
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]

        public void GetAnItemByIDTest(int? itemID){
            Assert.NotNull(itemBL.GetItemByID(itemID));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]

        public void GetAnItemByIDTest1(int? itemID){
            Assert.Null(itemBL.GetItemByID(itemID));
        }
    }
}