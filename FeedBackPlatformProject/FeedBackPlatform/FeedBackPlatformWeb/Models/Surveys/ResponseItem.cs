using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class ResponseItem
    {
        public int? QuestionId { get; set; }
        public int? QuestionAnswerId { get; set; }
        public int SurveyId { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }
}