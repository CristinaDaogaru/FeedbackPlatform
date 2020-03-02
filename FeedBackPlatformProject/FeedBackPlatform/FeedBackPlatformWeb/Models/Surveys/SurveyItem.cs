using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class SurveyItem
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyCategory { get; set; }
        public int QuestionNumber { get; set; }
        public DateTime LastModified { get; set; }
    }
}