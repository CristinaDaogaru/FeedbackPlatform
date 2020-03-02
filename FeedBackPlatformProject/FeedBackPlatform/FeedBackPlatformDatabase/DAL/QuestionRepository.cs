using FeedBackPlatformDatabase.Context;
using FeedBackPlatformDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.DAL
{
    public class QuestionRepository : GenericRepository<Question>
    {
        public QuestionRepository(FeedBackPlatformContext context) : base(context)
        {
        }
        public List<Question> GetAllBySurvey(int surveyId)
        {
            return dbSet.Where(q => q.SurveyId == surveyId).ToList();
        }
    }
}
