using FeedBackPlatformDatabase.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FeedBackPlatformWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //add dummy data
            var newCategories = new List<string> { "Community", "Customer Feedback", "Customer Satisfaction", "Demographics", "Education", "Events", "Healthcare", "Human Resources", "Industry Specific", "Just for Fun", "Market Research", "Non - Profit", "Political" };
            using (var uow = new UnitOfWork())
            {
                var categories = uow.SurveyCategoryRepository.GetAll();
                foreach (var newCategory in newCategories)
                {
                    if (!categories.Any(c => c.Name == newCategory))
                    {
                        uow.SurveyCategoryRepository.Insert(new FeedBackPlatformDatabase.Models.SurveyCategory
                        {
                            Name = newCategory
                        });
                    }
                }

                uow.Save();
            }

            var newQuestionTypes = new List<string> { "Radiobuttons", "Checkboxes", "Dropdown", "Single line textbox", "Multiple line textbox" };
            using (var uow = new UnitOfWork())
            {
                var questionTypes = uow.QuestionTypeRepository.GetAll();
                foreach (var newQuestionType in newQuestionTypes)
                {
                    if (!questionTypes.Any(c => c.Name == newQuestionType))
                    {
                        uow.QuestionTypeRepository.Insert(new FeedBackPlatformDatabase.Models.QuestionType
                        {
                            Name = newQuestionType
                        });
                    }
                }

                uow.Save();
            }
        }
    }
}
