using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HelwanQuestionnaires.Models;

namespace HelwanQuestionnaires.ViewModel
{
    public class SurveyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Question> Questions { get; set; }

    }

}