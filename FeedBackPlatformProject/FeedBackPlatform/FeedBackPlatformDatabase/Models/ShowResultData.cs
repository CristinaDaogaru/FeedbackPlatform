using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.Models
{
    public class ShowResultData
    {
        public int Id { get; set; }
        public double NegativeResponse { get; set; }
        public double PositiveResult { get; set; }
        
        public int SurveyId { get; set; }
    }
}
