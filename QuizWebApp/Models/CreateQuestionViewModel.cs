using QuizWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWebApp.ViewModels
{
    public class CreateQuestionViewModel
    {
        public string QuestionDescription { get; set; }
        public string FillInBlank { get; set; }
        public string Explication { get; set; }
        public QuestionType QuestionType { get; set; } // nouvelle propriété
        public int ExamId { get; set; }
        public int SkillId { get; set; }
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

    public class CreateReponseViewModel
    {
        public int ExamId { get; set; }
        public List<int> question { get; set; }
        public List<object> reponse { get; set; }
    }

    public class QuestionViewModel
    {
        public int questionId { get; set; }
        public List<int> reponseId { get; set; }
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
        public virtual List<Skill> Skills { get; set; }
        public virtual List<Question> Questions { get; set; }
    }

    public class Skill
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int? ExamId { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }
    }

    public class Question
    {
        [Key]
        public int Id { get; set; }

        public string QuestionDescription { get; set; }
        public string Explication { get; set; }
        public string FillInBlank { get; set; }
        public QuestionType QuestionType { get; set; }

        public int? ExamId { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        public int SkillId { get; set; }
        public virtual Skill skill { get; set; }

        public virtual List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        [Key]
        public int Id { get; set; }

        public string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
        public string GroupBy { get; set; }

        public int? QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }

    public class Reponse
    {
        [Key]
        public int Id { get; set; }

        public int ExamId { get; set; }
        public Guid SessionId { get; set; }
        public string userId { get; set; }
        public int questionId { get; set; }

        [ForeignKey("questionId")]
        public virtual Question Question { get; set; }

        public string reponseId { get; set; }
        public bool isCorrect { get; set; }
    }
}