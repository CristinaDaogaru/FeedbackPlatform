using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string QuestionType { get; set; }

        public List<QuestionAnswer> QuestionAnswers { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public List<Response> Responses { get; set; }
    }
}
