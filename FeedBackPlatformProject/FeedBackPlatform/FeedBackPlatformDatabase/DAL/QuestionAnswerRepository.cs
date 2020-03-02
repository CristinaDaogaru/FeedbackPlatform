using FeedBackPlatformDatabase.Context;
using FeedBackPlatformDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.DAL
{
    public class QuestionAnswerRepository : GenericRepository<QuestionAnswer>
    {
        public QuestionAnswerRepository(FeedBackPlatformContext context) : base(context)
        {
        }
        public List<QuestionAnswer> GetAllByQuestionId(int questionId)
        {
            return dbSet.Where(qa => qa.QuestionId == questionId).ToList();
        }
    }
}
