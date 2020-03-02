using FeedBackPlatformDatabase.Context;
using FeedBackPlatformDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.DAL
{
    public class SurveyRepository : GenericRepository<Survey>
    {
        public SurveyRepository(FeedBackPlatformContext context) : base(context)
        {
        }

        public Survey GetByIdAndUser(int id, int userId)
        {
            return dbSet.FirstOrDefault(s => s.Id == id && s.UserId == userId);
        }

        public object GetByIdAndUser(int id, object userId)
        {
            throw new NotImplementedException();
        }

        public List<Survey> GetAllByUser(int userId)
        {
            return dbSet.Where(s => s.UserId == userId).ToList();
        }
    }
}
