using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class QuestionAnswer
    {
        public int AnswerId { get; set; }
        public string Description { get; set; }
        public bool IsNewQuestion { get; set; }
        public bool IsDeleted { get; set; }
    }
}