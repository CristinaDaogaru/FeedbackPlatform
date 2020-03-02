using FeedBackPlatformDatabase.Context;
using FeedBackPlatformDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.DAL
{
    public class ResponseRepository : GenericRepository<Response>
    {
        public ResponseRepository(FeedBackPlatformContext context) : base(context)
        {
        }
        public List<Response> GetAllByQuestionIdAndQuestionAnswer(int questionId,int questionAnswerId)
        {
            return dbSet.Where(r => r.QuestionId == questionId && r.QuestionAnswerId == questionAnswerId).ToList();
        }
        public List<Response> GetAllByQuestionId(int questionId)
        {
            return dbSet.Where(r => r.QuestionId == questionId).ToList();
        }
        public List<Response> GetAllBySurveyId(int surveyId)
        {
            return dbSet.Where(r => r.SurveyId == surveyId).ToList();
        }
    }
}
