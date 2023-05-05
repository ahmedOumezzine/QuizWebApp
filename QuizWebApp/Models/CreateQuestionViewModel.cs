using QuizWebApp.Models;
using QuizWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizWebApp.ViewModels
{
    public class CreateQuestionViewModel
    {
        public string QuestionDescription { get; set; }
        public string FillInBlank { get; set; }
        public string Explication { get; set; }
        public QuestionType QuestionType { get; set; } // nouvelle propriété
        public int ExamId { get; set; }
        public bool IsTrue { get; set; }
        public List<Choice> Choices { get; set; }
        public List<string> correctOption { get; set; }
        public List<string> fakeOption { get; set; }
        public List<Choice[]> responseOption { get; set; } 
    }
    public class Question
    {
        public int Id { get; set; }
        public string QuestionDescription { get; set; }
        public QuestionType QuestionType { get; set; }
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public int Id { get; set; }
        public string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
    }




}
namespace QuizWebApp.Models
{

    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string SkillsMeasured { get; set; }
        public virtual List<Question> Questions { get; set; }

    }

    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuestionDescription { get; set; }
        public string Explication { get; set; }
        public string FillInBlank { get; set; }
        public QuestionType QuestionType { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual List<Choice> Choices { get; set; }
    }
    public class Choice
    {
        [Key]
        public int Id { get; set; }
        public string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
        public string GroupBy { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}