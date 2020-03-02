using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedBackPlatformWeb.Models.Surveys
{
    public class ShowResultItem
    {
        public int SurveyId { get; set; }
        public double NegativeRespinse { get; set; }
        public double PositiveResponse { get; set; }
    }
}