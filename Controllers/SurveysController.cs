using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelwanQuestionnaires.Models;

namespace HelwanQuestionnaires.Controllers
{
    [Authorize]
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SurveysController()
        {
            _db = new ApplicationDbContext();
            
        }

        [HttpGet]
        public ActionResult Index()
        {
            var surveys = _db.Surveys.ToList();
            return View(surveys);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var survey = new Survey
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1)
                };

            return View(survey);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Survey survey, string action)
        {
            if (ModelState.IsValid)
            {
                survey.Questions.ForEach(q => q.CreatedOn = q.ModifiedOn = DateTime.Now);

                _db.Surveys.Add(survey);
                _db.SaveChanges(); 

                TempData["success"] = "The survey was successfully created!";

                return RedirectToAction("Edit", new { id = survey.Id });
            }
            else
            {
                TempData["error"] = "An error occurred while attempting to create this survey.";
                return View(survey);
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var survey = _db.Surveys.Single(s => s.Id == id);

            survey.Questions = _db.Questions.Include("Choices")
                .Where(q => q.SurveyId == id)
                .OrderBy(q => q.Priority)
                .ToList();

            return View(survey);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Survey model)
        {
            foreach (var question in model.Questions)
            {
                question.SurveyId = model.Id; 
                if (question.Id == 0)
                {
                    question.ModifiedOn = question.CreatedOn = DateTime.Now;
                    _db.Entry(question).State = EntityState.Added; 

                    foreach (var choice in question.Choices)
                    {
                        choice.QuestionId = question.Id;
                        _db.Entry(choice).State = EntityState.Added; 
                    }
                }
                else
                {
                    question.ModifiedOn = DateTime.Now;

                    var choices = _db.Choices.Where(c => c.QuestionId == question.Id).ToList(); 

                    foreach (var choice in choices)
                    {
                        _db.Entry(choice).State = EntityState.Deleted; 
                    }

                    foreach (var questionChoice in question.Choices)
                    {
                        questionChoice.Id = 0;
                        questionChoice.QuestionId = question.Id;
                        _db.Entry(questionChoice).State = EntityState.Added; 
                    }
                    
                    _db.Entry(question).State = EntityState.Modified;
                    _db.Entry(question).Property(x => x.CreatedOn).IsModified = false;
                }
            }

            
            _db.Entry(model).State = EntityState.Modified;
            
            _db.SaveChanges();
            return RedirectToAction("Edit", new { id = model.Id });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _db.Surveys.Single(s => s.Id == id); 
            return Delete(model); 
        }

        [HttpPost]
        public ActionResult Delete(Survey survey)
        {
            _db.Entry(survey).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
