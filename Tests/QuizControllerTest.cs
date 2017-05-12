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
    class QuizControllerTest
    {

        Quiz quiz1 = null;
        Quiz quiz2 = null;


        List<Quiz> quizList = null;
        QuizRepository quizRepo = null;
        QuizQuestionRepository quizQuestionRepo = null;

        OperationResult uow = null;
        QuizController controller = null;

        public QuizContollerTest()
        {
            int loggedInCustomerId = new LoginInfo().GetAuthenticatedId();


            quiz1 = new Quiz { Name = "Quiz Test", CourseID = 2, CreatedDate = DateTime.Now, IsActive = true, UserID = loggedInCustomerId };
            quiz2 = new Quiz { Name = "Quiz Test 2", CourseID = 3, CreatedDate = DateTime.Now, IsActive = true, UserID = loggedInCustomerId };


            quizList = new List<Quiz> { quiz1, quiz2 };


            // Lets create our dummy repository
            quizRepo = new UserRepository(quizList);

            // Let us now create the Unit of work with our dummy repository
            uow = new OperationResult(quizRepo);

            // Now lets create the BooksController object to test and pass our unit of work
            controller = new QuizController(uow);
        }//end UserControllerTest


        [TestMethod]
        public void Create()
        {
            int loggedInCustomerId = new LoginInfo().GetAuthenticatedId();


            Quiz newQuiz = new Quiz()
            {
                Name = "Quiz Test",
                CourseID = 3,
                CreatedDate = DateTime.Now,
                IsActive = true,
                UserID = loggedInCustomerId,
            };

            // Lets call the action method now
            controller.Create(newQuiz);

            // get the list of books
            List<Quiz> quizes = quizRepo.GetAll();

            CollectionAssert.Contains(quizes, newQuiz);
        }//end method



        [TestMethod]
        public void Detail(QuizDetailViewModel quizDetailInfo)
        {

            QuizDetailViewModel detailViewModel = new QuizDetailViewModel();

            detailViewModel.QuizQuestions = quizQuestionRepo.GetItemListByQueryWithOrderByDesc(q => q.QuizID == quizDetailInfo.ID, q => q.CreatedDate, 0, 1000);
            detailViewModel.QuizID = quizDetailInfo.ID;


            CollectionAssert.Contains(true);

        }//end method


        [TestMethod]
        public void QuizQuestion(QuizQuestion quizQuestionInfo)
        {
            Quiz existQuiz = quizRepo.GetSingle(q => q.ID == quizQuestionInfo.QuizID);

            QuizQuestion newQuizQuestion = new QuizQuestion()
            {
                QuizID = existQuiz.ID,
                Question = quizQuestionInfo.Question,
                A = quizQuestionInfo.A,
                B = quizQuestionInfo.B,
                C = quizQuestionInfo.C,
                D = quizQuestionInfo.D,
                E = quizQuestionInfo.E,
                CreatedDate = DateTime.Now,
                Answer = quizQuestionInfo.Answer,
                Sequence = quizQuestionInfo.Sequence,
            };

            List<QuizQuestion> quizesQuestions = quizQuestionRepo.GetAll();

            CollectionAssert.Contains(quizesQuestions, newQuizQuestion);


        }//end method


        [TestMethod]
        public void QuizQuestionDelete(QuizQuestion quizDeleteInfo)
        {
            QuizQuestion existQuizQuestion = quizQuestionRepo.GetSingle(q => q.ID == quizDeleteInfo.ID);

            OperationResult opResult = quizQuestionRepo.DeleteItem(existQuizQuestion);


            List<QuizQuestion> quizesQuestions = quizQuestionRepo.GetAll();


            CollectionAssert.Contains(quizesQuestions, opResult);


        }//end method

    }//end class
}//end namespace
