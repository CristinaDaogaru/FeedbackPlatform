using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class SurveyModelOverview
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public List<QuestionOverview> QuestionOverviewList { get; set; }

        public SurveyModelOverview()
        {
            this.QuestionOverviewList = new List<QuestionOverview>();
        }
    }
}