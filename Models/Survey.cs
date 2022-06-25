using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace HelwanQuestionnaires.Models
{
    public class Survey
    {
        public Survey()
        {
            Questions = new List<Question>();
            Responses = new List<Response>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display (Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display (Name = "End Date")]
        public DateTime EndDate { get; set; }

        public List<Question> Questions { get; set; }

        public List<Response> Responses { get; set; }


        public bool IsActive => StartDate < DateTime.Now && EndDate > DateTime.Now;


        public string ToJson()
        {
            var js = JsonSerializer.Create(new JsonSerializerSettings());
            var jw = new StringWriter();
            js.Serialize(jw, this);
            return jw.ToString();            
        }
    }
}