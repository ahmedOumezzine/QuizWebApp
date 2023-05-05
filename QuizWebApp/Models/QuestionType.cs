using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApp.Models
{
    public enum QuestionType
    {
        SingleChoice, // question à choix unique
        MultiChoice, // question à choix multiples
        TrueFalse, // question Vrai/Faux
        ShortAnswer, // réponse courte
        LongAnswer, // réponse longue
        FillInBlank, // compléter les blancs 
        DragAndDrop ,// association
        DragAndDrop2 // glisser-déposer
    }
}