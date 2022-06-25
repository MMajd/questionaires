using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HelwanQuestionnaires.Models
{
    public class Answer
    {
        [Key] public int Id { get; set; }

        public int ResponseId { get; set; }

        public Response Response { get; set; }

        public int QuestionId { get; set; }

        public string Value { get; set; }

        public string Comment { get; set; }

        public Question Question { get; set; }

        public int Score { get; set; }
    }
}