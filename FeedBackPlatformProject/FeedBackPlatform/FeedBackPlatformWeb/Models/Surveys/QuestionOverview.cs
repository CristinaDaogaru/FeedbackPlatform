using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class QuestionOverview
    {
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public string QuestionType { get; set; }
        public ResponseItem SingleResponse { get; set; }

        public List<QuestionAnswerOverview> QuestionAnswerOverview { get;set;}

        public List<ResponseItem> ResponseItems { get; set; }

        public QuestionOverview()
        {
            this.QuestionAnswerOverview = new List<QuestionAnswerOverview>();
            this.ResponseItems = new List<ResponseItem>();
        }
    }
}