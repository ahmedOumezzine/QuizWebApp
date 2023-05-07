using QuizWebApp.Models;
using QuizWebApp.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Question = QuizWebApp.Models.Question;

namespace QuizWebApp.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index(int ExamId)
        {
            ViewBag.ExamId = ExamId;
            var questions = db.questions.Include(q => q.Exam).Where(x => x.ExamId == ExamId);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.questions.Include(x => x.skill).Include(x => x.Exam).Include(x => x.Choices).Where(x => x.Id == id).FirstOrDefault();
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create(int ExamId)
        {
            ViewBag.ExamId = ExamId;
            ViewBag.SkillId = new SelectList(db.skills.Where(x => x.ExamId == ExamId), "Id", "Name");
            return View();
        }

        // POST: Questions/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateQuestionViewModel model)
        {
            var question = new Question();
            if (ModelState.IsValid)
            {
                question.QuestionDescription = model.QuestionDescription;
                question.QuestionType = model.QuestionType;
                question.Explication = model.Explication;
                question.FillInBlank = model.FillInBlank;
                question.ExamId = model.ExamId;
                question.SkillId = model.SkillId;
                question.Choices = new List<Models.Choice>();
                if (model.QuestionType == QuestionType.SingleChoice || model.QuestionType == QuestionType.MultiChoice || model.QuestionType == QuestionType.DragAndDrop)
                {
                    foreach (var item in model.Choices)
                    {
                        var choice = new Models.Choice();
                        choice.ChoiceText = item.ChoiceText;
                        choice.IsCorrect = item.IsCorrect;
                        choice.Question = question;
                        question.Choices.Add(choice);
                    }
                }
                if (model.QuestionType == QuestionType.TrueFalse)
                {
                    var choice = new Models.Choice();
                    choice.ChoiceText = "True";
                    choice.IsCorrect = model.IsTrue;
                    choice.Question = question;
                    question.Choices.Add(choice);
                    var choiceFalse = new Models.Choice();
                    choiceFalse.ChoiceText = "False";
                    choiceFalse.IsCorrect = !model.IsTrue;
                    choiceFalse.Question = question;
                    question.Choices.Add(choiceFalse);
                }

                if (model.QuestionType == QuestionType.DragAndDrop2)
                {
                    foreach (var item in model.correctOption)
                    {
                        var choice = new Models.Choice();
                        choice.ChoiceText = item;
                        choice.IsCorrect = true;
                        choice.Question = question;
                        question.Choices.Add(choice);
                    }
                    foreach (var item in model.fakeOption)
                    {
                        var choice = new Models.Choice();
                        choice.ChoiceText = item;
                        choice.IsCorrect = false;
                        choice.Question = question;
                        question.Choices.Add(choice);
                    }
                }
                if (model.QuestionType == QuestionType.FillInBlank)
                {
                    foreach (var item in model.responseOption.Select((value, i) => new { i, value }))
                    {
                        foreach (var itemchild in item.value)
                        {
                            var choice = new Models.Choice();
                            choice.ChoiceText = itemchild.ChoiceText;
                            choice.IsCorrect = itemchild.IsCorrect;
                            choice.Question = question;
                            choice.GroupBy = "question_" + item.i;
                            question.Choices.Add(choice);
                        }
                    }
                }

                db.questions.Add(question);
                var count = db.SaveChanges();
                return RedirectToAction("Index", new { ExamId = question.ExamId });
            }

            ViewBag.ExamId = question.ExamId;
            ViewBag.SkillId = new SelectList(db.skills.Where(x => x.ExamId == question.ExamId), "Id", "Name");

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id, int ExamId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.SkillId = new SelectList(db.skills.Where(x => x.ExamId == ExamId), "Id", "Name");

            ViewBag.ExamId = ExamId;
            return View(question);
        }

        // POST: Questions/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuestionDescription,QuestionType,ExamId,SkillId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { ExamId = question.ExamId });
            }
            ViewBag.ExamId = question.ExamId;
            ViewBag.Skills = new SelectList(db.skills.Where(x => x.ExamId == question.ExamId), "Id", "Name");

            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.questions.Find(id);
            db.questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index", new { ExamId = question.ExamId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}