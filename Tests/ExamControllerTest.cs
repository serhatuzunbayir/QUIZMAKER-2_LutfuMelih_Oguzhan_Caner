using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizMakerApp.Controllers;
using QuizMakerApp.Entity;
using QuizMakerApp.Model;
using QuizMakerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMakerApp.UnitTests
{

    [TestClass]

    class ExamControllerTest
    {

        User user1 = null;
        User user2 = null;


        List<Quiz> quizList = null;

        QuizRepository quizRepo = null;
        QuizQuestionRepository quizQuestionRepo = null;

        OperationResult uow = null;
        ExamController controller = null;

        public UserContollerTest()
        {


            quiz1 = new Quiz { Name = "Quiz Test", CourseID = 2, CreatedDate = DateTime.Now, IsActive = true, UserID = loggedInCustomerId };
            quiz2 = new Quiz { Name = "Quiz Test 2", CourseID = 3, CreatedDate = DateTime.Now, IsActive = true, UserID = loggedInCustomerId };


            quizList = new List<Quiz> { quiz1, quiz2 };


            // Lets create our dummy repository
            quizRepo = new UserRepository(quizList);

            // Let us now create the Unit of work with our dummy repository
            uow = new OperationResult(quizRepo);

            // Now lets create the BooksController object to test and pass our unit of work
            controller = new QuizController(uow);
        }//end public


        [TestMethod]
        public void Quiz(ExamViewModel quizInfo)
        {
            List<Quiz> quizList = quizRepo.GetAllWithOrderByDesc(q => q.CourseID == quizInfo.CourseID, 0, 1000);
     


            ExamViewModel viewModel = new ExamViewModel();
            viewModel.Quizes = quizList;


        }//end method

        [TestMethod]
        public void SelectCourse(ExamViewModel quizInfo)
        {
            Quiz existQuiz = quizRepo.GetSingle(q => q.CourseID == quizInfo.CourseID);
   

            ExamViewModel viewModel = new ExamViewModel();

            viewModel.Quizes = quizRepo.GetItemListByQueryWithOrderByDesc(q => q.CourseID == existQuiz.CourseID, q => q.CreatedDate, 0, 1000);
            CollectionAssert.Contains(existQuiz, viewModel);

        }//end method


        [TestMethod]
        public void QuizQuestion(ExamViewModel quizQuestionInfo)
        {
            Quiz existQuiz = quizRepo.GetSingle(q => q.ID == quizQuestionInfo.QuizID);
        

            ExamViewModel viewModel = new ExamViewModel();
            viewModel.QuizQuestion = quizQuestionRepo.GetItemListByQueryWithOrderByDesc(q => q.QuizID == existQuiz.ID, q => q.Sequence, 0, 1000);



        }//end method


    }//end class
}//end namespace
