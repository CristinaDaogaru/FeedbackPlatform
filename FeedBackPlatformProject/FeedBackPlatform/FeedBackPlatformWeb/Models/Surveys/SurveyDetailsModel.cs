using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class SurveyDetailsModel
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyCategory { get; set; }
        public QuestionItem Question { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public EmailAdressModel EmailAdress { get; set; }

        public List<QuestionDetails> QuestionDetailsList { get; set; }

        public SurveyDetailsModel()
        {
            this.QuestionDetailsList = new List<QuestionDetails>();
        }
    }
}