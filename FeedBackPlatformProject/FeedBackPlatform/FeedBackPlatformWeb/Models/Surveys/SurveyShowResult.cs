using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class SurveyShowResult
    {
        public string SurveyName { get; set; }
        public double ProcentajRaspunsNegativ { get; set; }
        public double ProcentajRaspunsPozitiv { get; set; }
    }
}