using FeedBackPlatformDatabase.DAL;
using FeedBackPlatformWeb.Controllers.Base;
using FeedBackPlatformWeb.Models;
using FeedBackPlatformWeb.Models.Surveys;
using FeedBackPlatformWeb.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;

namespace FeedBackPlatformWeb.Controllers
{
    [Utils.Attributes.AuthorizeUser]
    public class SurveysController : BaseController
    {
        #region Actions
        public ActionResult Overview()
        {
            var model = new OverviewModel();
            model.SurveyCategories.Add(new SelectListItem
            {
                Value = null,
                Text = "Select Category",
                Selected = true
            });


            using (var uow = new UnitOfWork())
            {
                var categories = uow.SurveyCategoryRepository.GetAll();
                var surveys = uow.SurveyRepository.GetAllByUser(this.UserContext.UserId);

                foreach (var category in categories)
                {
                    model.SurveyCategories.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name
                    });
                }

                foreach (var survey in surveys)
                {
                    var category = categories.FirstOrDefault(c => c.Id == survey.SurveyCategoryId);
                    var question = uow.QuestionRepository.GetAllBySurvey(survey.Id);

                    model.Surveys.Add(new SurveyItem
                    {
                        SurveyId = survey.Id,
                        SurveyName = survey.Name,
                        SurveyCategory = category != null ? category.Name : string.Empty,
                        LastModified = survey.ModifyDate,
                        QuestionNumber = question.Count()
                    });
                }
            }


            this.SetHeaderAndMenu(Constants.Surveys_OverviewPage);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = new SurveyDetailsModel();

            using (var uow = new UnitOfWork())
            {
                var survey = uow.SurveyRepository.GetByIdAndUser(id, this.UserContext.UserId);
                var category = uow.SurveyCategoryRepository.GetByID(survey.SurveyCategoryId);
                var question = uow.QuestionRepository.GetAllBySurvey(survey.Id);
                foreach (var q in question)
                {
                    var questionAnswers = uow.QuestionAnswerRepository.GetAllByQuestionId(q.Id);
                    List<QuestionAnswer> questionAnswersDetail = new List<QuestionAnswer>();
                    foreach (var qa in questionAnswers)
                    {
                        var qad = new QuestionAnswer
                        {
                            AnswerId = qa.Id,
                            Description = qa.Content
                        };
                        questionAnswersDetail.Add(qad);
                    }

                    var questionDetail = new QuestionDetails
                    {
                        QuestionId = q.Id,
                        SurveyId = q.SurveyId,
                        Description = q.Description,
                        QuestionType = q.QuestionType,
                        QuestionAnswers = questionAnswersDetail

                    };

                    model.QuestionDetailsList.Add(questionDetail);
                }

                model.SurveyId = survey.Id;
                model.SurveyName = survey.Name;
                model.SurveyCategory = category.Name;
                model.CreateDate = survey.CreateDate;
                model.ModifyDate = survey.ModifyDate;
            }
            /////aici python!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!#############################
            this.SetHeaderAndMenu(Constants.Surveys_OverviewPage);
            return View(model);
        }

        public ActionResult CreateSurvey(CreateSurveyModel model)
        {
            if (!string.IsNullOrEmpty(model.SurveyName) && model.SurveyCategory.HasValue)
            {
                var newSurvey = new FeedBackPlatformDatabase.Models.Survey
                {
                    Name = model.SurveyName,
                    SurveyCategoryId = model.SurveyCategory.Value,
                    UserId = this.UserContext.UserId,
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow
                };

                using (var uow = new UnitOfWork())
                {
                    uow.SurveyRepository.Insert(newSurvey);
                    uow.Save();
                }

                return Json(new { success = true, id = newSurvey.Id });
            }

            return Json(new { success = false, message = "Survey creation failed." });
        }

        [HttpPost]
        public JsonResult AddQuestion(QuestionItem question)
        {
            if (!string.IsNullOrEmpty(question.Description))
            {
                var newQuestion = new FeedBackPlatformDatabase.Models.Question
                {
                    Description = question.Description,
                    SurveyId = question.SurveyId,
                    QuestionType = question.QuestionType
                };

                using (var uow = new UnitOfWork())
                {
                    uow.QuestionRepository.Insert(newQuestion);
                    uow.Save();
                }
                if (question.QuestionAnswers != null && question.QuestionType != "Comment box")
                {
                    foreach (var qa in question.QuestionAnswers)
                    {
                        var questionAnswer = new FeedBackPlatformDatabase.Models.QuestionAnswer
                        {
                            Content = qa.Description,
                            QuestionId = newQuestion.Id
                        };
                        using (var uow = new UnitOfWork())
                        {
                            uow.QuestionAnswerRepository.Insert(questionAnswer);
                            uow.Save();
                        }
                    }
                }

                return Json(new { success = true, id = newQuestion.SurveyId });
            }

            return Json(new { success = false, message = "Question creation failed." });
        }

        public ActionResult DeleteQuestion(int questionId, int surveyId)
        {
            using (var uow = new UnitOfWork())
            {
                uow.QuestionRepository.Delete(questionId);
                uow.Save();
            }
            return RedirectToAction("Details", "Surveys", new { id = surveyId });
        }

        public ActionResult EditQuestion(QuestionDetails questionDetails)
        {
            if (!string.IsNullOrEmpty(questionDetails.Description))
            {
                var newQuestion = new FeedBackPlatformDatabase.Models.Question
                {
                    Id = questionDetails.QuestionId,
                    Description = questionDetails.Description,
                    SurveyId = questionDetails.SurveyId,
                    QuestionType = questionDetails.QuestionType
                };

                using (var uow = new UnitOfWork())
                {
                    uow.QuestionRepository.Update(newQuestion);
                    uow.Save();
                }
                if (questionDetails.QuestionAnswers != null && questionDetails.QuestionType != "Comment box")
                {
                    foreach (var qa in questionDetails.QuestionAnswers)
                    {
                        if (qa.IsNewQuestion)
                        {
                            var questionAnswer = new FeedBackPlatformDatabase.Models.QuestionAnswer
                            {
                                Content = qa.Description,
                                QuestionId = newQuestion.Id
                            };
                            using (var uow = new UnitOfWork())
                            {
                                uow.QuestionAnswerRepository.Insert(questionAnswer);
                                uow.Save();
                            }
                        }
                        else if (qa.IsDeleted)
                        {
                            using (var uow = new UnitOfWork())
                            {
                                uow.QuestionAnswerRepository.Delete(qa.AnswerId);
                                uow.Save();
                            }
                        }
                        else
                        {
                            var questionAnswer = new FeedBackPlatformDatabase.Models.QuestionAnswer
                            {
                                Id = qa.AnswerId,
                                Content = qa.Description,
                                QuestionId = newQuestion.Id
                            };
                            using (var uow = new UnitOfWork())
                            {
                                uow.QuestionAnswerRepository.Update(questionAnswer);
                                uow.Save();
                            }
                        }
                    }
                }

                return Json(new { success = true, id = newQuestion.SurveyId });
            }

            return Json(new { success = false, message = "Question creation failed." });
        }

        public ActionResult DeleteSurvey(SurveyItem surveyItem)
        {
            using (var uow = new UnitOfWork())
            {
                uow.SurveyRepository.Delete(surveyItem.SurveyId);
                uow.Save();
            }
            return RedirectToAction("Overview", "Surveys");
        }

        public ActionResult SendEmail(EmailAdressModel emailAdress)
        {
            if (!string.IsNullOrEmpty(emailAdress.Email))
            {
                var fromAddress = new MailAddress("feedback.platformapp@gmail.com");
                var fromPassword = "calculatoare";
                var toAddress = new MailAddress(emailAdress.Email);
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
                string subject = "subject";
                string body = "Can you complete the survey on the following link: " + baseUrl + Url.Action("SurveyForReview", "SendEmail", new { id = emailAdress.SurveyId });

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };

                using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })


                    smtp.Send(message);

                return Json(new { success = true, id = emailAdress.SurveyId });
            }

            return Json(new { success = false, message = "Email was not sent." });
        }

        public ActionResult DownloadPDF(int id)
        {
            Byte[] bytes = PdfHelper.GeneratePdf(id, this.UserContext.UserId, Server);
            return File(bytes, "application/pdf", "Survey.pdf");
        }

        public ActionResult ShowResult(int id)
        {
            var model = new SurveyShowResult();
            using (var uow = new UnitOfWork())
            {
                var survey = uow.SurveyRepository.GetByIdAndUser(id, this.UserContext.UserId);
                var showResults = uow.ShowResultDataRepository.GetAllBySurveyId(id);
                model.SurveyName = survey.Name;
                
                if (showResults != null && showResults.Count > 0)
                {
                    foreach (var sr in showResults)
                    {
                        model.ProcentajRaspunsNegativ = sr.PositiveResult;
                        model.ProcentajRaspunsPozitiv = sr.NegativeResponse;
                    }
                   
                }
            }
            return View(model);
        }
        #endregion


        #region Private Methods
        private void SetHeaderAndMenu(string pageKey)
        {
            ViewBag.Sidebar = new SidebarModel
            {
                CurrentPage = pageKey
            };
        }
        #endregion
    }
}