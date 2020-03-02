using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class CreateSurveyModel
    {
        public string SurveyName { get; set; }
        public int? SurveyCategory { get; set; }
    }
}