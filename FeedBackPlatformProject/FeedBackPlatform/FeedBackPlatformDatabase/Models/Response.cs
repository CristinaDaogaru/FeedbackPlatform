using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.Models
{
    public class Response
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int? QuestionId { get; set; }
        public int? QuestionAnswerId { get; set; }
        public string Content { get; set; }

        public Survey Survey { get; set; }
        public Question Question { get; set; }
        public QuestionAnswer QuestionAnswer { get; set; }
    }
}
