using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizMakerApp.Controllers;
using QuizMakerApp.Entity;
using QuizMakerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMakerApp.UnitTests
{

    [TestClass]

    class HomeControllerTest
    {


        User user1 = null;
        User user2 = null;


        List<User> userList = null;

        UserRepository userRepo = null;
        OperationResult uow = null;
        HomeController controller = null;

        public UserContollerTest()
        {

            user1 = new User { ID = 5, UserName = "UTest_1", Email = "unit@testing.com", Password = "UnitTest", IsActive = true, CreatedDate = DateTime.Now, Adress = "", ImageFilePath = "", Phone = "", IsAdmin = false };
            user2 = new User { ID = 6, UserName = "UTest_2", Email = "unit@testing.com", Password = "UnitTest", IsActive = true, CreatedDate = DateTime.Now, Adress = "", ImageFilePath = "", Phone = "", IsAdmin = false };

            userList = new List<User> { user1, user2 };


            // Lets create our dummy repository
            userRepo = new UserRepository(userList);

            // Let us now create the Unit of work with our dummy repository
            uow = new OperationResult(userRepo);

            // Now lets create the BooksController object to test and pass our unit of work
            controller = new HomeController(uow);
        }//end public



        [TestMethod]
        public void Login()
        {

            string email = "unit@testing.com";
            string username = "UnitTesting";
            string password = "Password";
            string redirectValue = "redirecturl";

            LoginInfo logininfo = new LoginInfo();


            OperationResult opResult = logininfo.SetLogin(email, username, password);


            CollectionAssert.Contains(opResult);





        }//end method

        [TestMethod]

        public void LogOut()
        {

            LoginInfo loginInfo = new LoginInfo();
            OperationResult logOut = loginInfo.LogOut();

            CollectionAssert.Contains(logOut);

        }//end method










    }//end class
}//end namespace
