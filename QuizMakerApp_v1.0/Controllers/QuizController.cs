using QuizMakerApp.Entity;
using QuizMakerApp.IoC;
using QuizMakerApp.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizMakerApp.Controllers
{
    public class QuizController : Controller
    {

        #region Definitions

        QuizRepository _repQuiz = RepositoryFactory<QuizRepository>.GetRepositoryInstance();


        #endregion

        #region Create
        // GET: Quiz
        public ActionResult Create()
        {
           

          
            return View();
        }//end method

        // POST :/ Quiz / Create
        [HttpPost]
        public ActionResult Create(Quiz quizInfo)
        {
            try
            {
                Quiz newQuiz = new Quiz() {

                    CourseID = 1,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    UserID = 1,

                };

                OperationResult opResult = _repQuiz.AddItem(newQuiz);
                if (opResult.IsFailed)
                {
                   
                    //error message
                }//end if




            }
            catch (Exception)
            {

                return Redirect("/Quiz/Create");
            }

            
            return Redirect("/Home/Index");


        }//end method



        #endregion



    }//end class
}//end namespace