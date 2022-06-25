using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelwanQuestionnaires.Models
{
    public class Choice
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int QuestionId { get; set; }

        public int SurveyId { get; set; }
    }
}