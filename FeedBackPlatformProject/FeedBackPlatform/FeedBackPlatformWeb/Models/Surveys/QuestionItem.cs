using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class QuestionItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int SurveyId { get; set; }
        public string QuestionType { get; set; }

        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}