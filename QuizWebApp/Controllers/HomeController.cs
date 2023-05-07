using Microsoft.AspNet.Identity;
using QuizWebApp.Models;
using QuizWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Question = QuizWebApp.Models.Question;

namespace QuizWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.exams.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.exams.Include(x => x.Skills).Where(x => x.Id == id).SingleOrDefault();
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        public ActionResult Take(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Question> questions = db.questions.Include(y => y.Exam).Include(x => x.Choices).Where(x => x.ExamId == id).ToList();

            ViewBag.ExamId = id;
            return View(questions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Take(CreateReponseViewModel createReponseViewModel)
        {
            var SessionId = Guid.NewGuid();
            List<Question> questions = db.questions.Include(y => y.Exam).Include(x => x.Choices).Where(x => x.ExamId == createReponseViewModel.ExamId).ToList();
            var liste = new List<Reponse>();
            foreach (var item in createReponseViewModel.reponse.Select((value, i) => new { i, value }))
            {
                var resultQuestion = new Reponse();
                resultQuestion.SessionId = SessionId;
                resultQuestion.ExamId = createReponseViewModel.ExamId;
                resultQuestion.userId = User.Identity.GetUserId();
                resultQuestion.questionId = createReponseViewModel.question[item.i];

                var question = questions.Where(x => x.Id == resultQuestion.questionId).SingleOrDefault();
                if (question.QuestionType == QuestionType.SingleChoice || question.QuestionType == QuestionType.TrueFalse)
                {
                    var reponseId = Convert.ToInt32(((string[])item.value)[0]);

                    var correctReponse = question.Choices.Where(x => x.IsCorrect).SingleOrDefault();
                    resultQuestion.isCorrect = (correctReponse.Id == reponseId);
                    resultQuestion.reponseId = reponseId.ToString();
                }
                if (question.QuestionType == QuestionType.MultiChoice || question.QuestionType == QuestionType.DragAndDrop)
                {
                    var correctReponse = question.Choices.Where(x => x.IsCorrect).ToList();
                    int[] reponseId = ((string[])item.value).Select(n => Convert.ToInt32(n)).ToArray();

                    resultQuestion.isCorrect = Enumerable.SequenceEqual(correctReponse.Select(x => x.Id), reponseId);
                    resultQuestion.reponseId = String.Join(", ", reponseId);
                }
                if (question.QuestionType == QuestionType.DragAndDrop2)
                {
                    var correctReponse = question.Choices.Where(x => x.IsCorrect).OrderBy(x => x.Id).ToList();
                    int[] reponseId = ((string[])item.value).Select(n => Convert.ToInt32(n)).ToArray();
                    resultQuestion.isCorrect = true;
                    if (reponseId.Length == correctReponse.Count)
                    {
                        for (int i = 0; i <= correctReponse.Count; i++)
                        {
                            if (correctReponse[i].Id != reponseId[i])
                            {
                                resultQuestion.isCorrect = false;
                            }
                        }
                    }
                    else
                    {
                        resultQuestion.isCorrect = false;
                    }

                    resultQuestion.reponseId = String.Join(", ", reponseId);
                }
                if (question.QuestionType == QuestionType.FillInBlank)
                {
                    resultQuestion.isCorrect = false;
                    var reponseIds = new List<int>();
                    foreach (string key in Request.Form.AllKeys)
                    {
                        if (key.StartsWith("reponse[" + item.i + "]"))
                        {
                            var groupBy = key.Replace("reponse[" + item.i + "]", "").Replace("[", "").Replace("]", "");
                            var correctReponse = question.Choices.Where(x => x.IsCorrect && x.GroupBy == groupBy).OrderBy(x => x.Id).SingleOrDefault();
                            var reponseId = Convert.ToInt32(Request.Form[key]);
                            resultQuestion.isCorrect = resultQuestion.isCorrect && (correctReponse.Id == reponseId);
                            reponseIds.Add(reponseId);
                        }
                    }
                    resultQuestion.reponseId = String.Join(", ", reponseIds);
                }
                liste.Add(resultQuestion);
            }
            db.reponses.AddRange(liste);
            var res = db.SaveChanges();
            return RedirectToAction("resultat", new { Id = SessionId });
        }

        public ActionResult Resultat(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Reponse> reponses = db.reponses.Include(x => x.Question.Choices).Where(x => x.SessionId == Id).ToList();

            return View(reponses);
        }
    }
}