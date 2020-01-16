using System;
using Xunit;
using DAL;
using Persistence.MODEL;
namespace DAL.Test
{
    public class UserUnitDALTest
    {
        private UserDAL userDAL = new UserDAL();
        [Theory]
        [InlineData("tu", "123456")]
        public void GetUserByUserNameAndPassWordTest(string username, string password)
        {
            User user = userDAL.GetUserByUserNameAndPassword(username, password);
            Assert.NotNull(user);
            Assert.Equal(username, user.UserName);
            Assert.Equal(password, user.PassWord);
        }
        [Theory]
        [InlineData("'?/:%'", "'.:=='")]
        [InlineData("tu2000", "03340647787")]
        [InlineData("saadasd", "saadasd")]
        [InlineData("adassad", "dasq")]

        public void GetUserByUserNameAndPassWordTest1(string username, string password)
        {
            Assert.Null(userDAL.GetUserByUserNameAndPassword(username, password));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetUserByIdTest(int? userId)
        {
            Assert.NotNull(userDAL.GetUserById(userId));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        
        public void GetUserByIdTest1(int? userId)
        {
            Assert.Null(userDAL.GetUserById(userId));
        }
    }
}
