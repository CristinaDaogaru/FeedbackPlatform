using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.Models
{
    public class SurveyCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Survey> Surveys { get; set; }
    }
}
