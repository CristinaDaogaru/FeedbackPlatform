using FeedBackPlatformDatabase.DAL;
using FeedBackPlatformWeb.Controllers.Base;
using FeedBackPlatformWeb.Models.Surveys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FeedBackPlatformWeb.Controllers
{
    public class SendEmailController : BaseController
    {
        public ActionResult SurveyForReview(int id)
        {
            var model = new SurveyModelOverview();

            using (var uow = new UnitOfWork())
            {
                var survey = uow.SurveyRepository.GetByID(id);
                var category = uow.SurveyCategoryRepository.GetByID(survey.SurveyCategoryId);
                var question = uow.QuestionRepository.GetAllBySurvey(id);
                foreach (var q in question)
                {
                    var questionAnswers = uow.QuestionAnswerRepository.GetAllByQuestionId(q.Id);
                    List<QuestionAnswerOverview> questionAnswersOverview = new List<QuestionAnswerOverview>();
                    foreach (var qa in questionAnswers)
                    {
                        var qad = new QuestionAnswerOverview
                        {
                            AnswerId = qa.Id,
                            Description = qa.Content
                        };
                        questionAnswersOverview.Add(qad);
                    }

                    var questionDetail = new QuestionOverview
                    {
                        QuestionId = q.Id,
                        Description = q.Description,
                        QuestionType = q.QuestionType,
                        QuestionAnswerOverview = questionAnswersOverview

                    };

                    model.QuestionOverviewList.Add(questionDetail);
                }

                model.SurveyId = survey.Id;
                model.SurveyName = survey.Name;
            }

            return View(model);
        }

        public ActionResult SaveCompletedSurvey(SurveyModelOverview surveyModelOverview)
        {
            if (surveyModelOverview != null && surveyModelOverview.QuestionOverviewList.Count() > 0)
            {
                foreach (var question in surveyModelOverview.QuestionOverviewList)
                {
                    if (question.SingleResponse != null)
                    {
                        var newResponse = new FeedBackPlatformDatabase.Models.Response
                        {
                            SurveyId = question.SingleResponse.SurveyId,
                            QuestionId = question.SingleResponse.QuestionId,
                            QuestionAnswerId = question.SingleResponse.QuestionAnswerId,
                            Content = question.SingleResponse.Text
                        };

                        using (var uow = new UnitOfWork())
                        {
                            uow.ResponseRepository.Insert(newResponse);
                            uow.Save();
                        }
                    }
                    if (question.ResponseItems != null && question.ResponseItems.Count() > 0)
                    {
                        foreach (var response in question.ResponseItems)
                        {
                            if (response.IsSelected)
                            {
                                var newResponse = new FeedBackPlatformDatabase.Models.Response
                                {
                                    SurveyId = response.SurveyId,
                                    QuestionId = response.QuestionId,
                                    QuestionAnswerId = response.QuestionAnswerId,
                                    Content = response.Text
                                };

                                using (var uow = new UnitOfWork())
                                {
                                    uow.ResponseRepository.Insert(newResponse);
                                    uow.Save();
                                }
                            }
                        }
                    }
                }
                using (var uow = new UnitOfWork())
                {
                    var responses = uow.ResponseRepository.GetAllBySurveyId(surveyModelOverview.SurveyId);
                    List<string> text = new List<string>();

                    foreach (var response in responses)
                    {
                        text.Add(response.Content);
                    }

                    using (StreamWriter file = new StreamWriter(@"C:\WorkSpace\Programs\Bitbucket\FeedBackPlatformProject\FileForClasification\response_by_survey-" +surveyModelOverview.SurveyId + ".txt"))
                    {
                        foreach (var response in text)
                            file.WriteLine(response);
                    }

                }
                return Json(new { success = true, surveyModelOverview.SurveyId });
            }

            return Json(new { success = false, message = "Samething went wrong! Please try again." });
        }
    }
}