using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SurveyCategoryId { get; set; }
        public SurveyCategory SurveyCategory { get; set; }
        public List<Question> Questions { get; set; }
        public List<Response> Responses { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        
    }
}
