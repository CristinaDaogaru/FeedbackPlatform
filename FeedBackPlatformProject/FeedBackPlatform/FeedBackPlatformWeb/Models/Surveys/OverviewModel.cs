using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class OverviewModel
    {
        public List<SurveyItem> Surveys { get; set; }
        public List<SelectListItem> SurveyCategories { get; set; }

        public OverviewModel()
        {
            this.Surveys = new List<SurveyItem>();
            this.SurveyCategories = new List<SelectListItem>();
        }
    }
}