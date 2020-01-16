using System;
using Xunit;
using Persistence.MODEL;
using BL;
namespace BL.Test
{
    public class UserUnitTest
    {
        private UserBL userBL = new UserBL();
        [Theory]
        [InlineData("tu", "123456")]

        public void GetUserByUserNameAndPassword(string userName, string Password){
            User user = userBL.GetUserByUserNameAndPassword(userName, Password);
            Assert.NotNull(user);
            Assert.Equal(userName, user.UserName);
            Assert.Equal(Password, user.PassWord);

        }
        [Theory]
        [InlineData("'?/:%'", "'.:=='")]
        [InlineData("tu2000", "03340647787")]
        [InlineData("null", "'.:=='")]
        [InlineData("'.:=='", "null")]
        public void GetUserByUserNameAndPassWordTest1(string userName, string PassWord)
        {
            Assert.Null(userBL.GetUserByUserNameAndPassword(userName, PassWord));

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]

        public void getUserByIDTest(int? userID){
            Assert.NotNull(userBL.GetUserById(userID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        
        public void GetUserByIdTest1(int? userID){
            Assert.Null(userBL.GetUserById(userID));
        }
    }
}
