using FeedBackPlatformDatabase.Context;
using FeedBackPlatformDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.DAL
{
    public class QuestionTypeRepository : GenericRepository<QuestionType>
    {
        public QuestionTypeRepository(FeedBackPlatformContext context) : base(context)
        {
        }
    }
}
