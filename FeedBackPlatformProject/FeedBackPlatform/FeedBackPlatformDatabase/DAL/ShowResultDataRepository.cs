using FeedBackPlatformDatabase.Context;
using FeedBackPlatformDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.DAL
{
    public class ShowResultDataRepository : GenericRepository<ShowResultData>
    {
        public ShowResultDataRepository(FeedBackPlatformContext context) : base(context)
        {}
        public List<ShowResultData> GetAllBySurveyId(int surveyId)
        {
            return dbSet.Where(r => r.SurveyId == surveyId).ToList();
        }
    }
}
