using FeedBackPlatformDatabase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackPlatformDatabase.DAL
{
    public class UnitOfWork : IDisposable
    {
        private FeedBackPlatformContext context = new FeedBackPlatformContext();

        private UserRepository userRepository;
        private SurveyRepository surveyRepository;
        private SurveyCategoryRepository surveyCategoryRepository;
        private QuestionRepository questionRepository;
        private QuestionAnswerRepository questionAnswerRepository;
        private QuestionTypeRepository questionTypeRepository;
        private ResponseRepository responseRepository;
        private ShowResultDataRepository showResultDataRepository;


        public UserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }
        public SurveyRepository SurveyRepository
        {
            get
            {
                if (this.surveyRepository == null)
                {
                    this.surveyRepository = new SurveyRepository(context);
                }
                return surveyRepository;
            }
        }
        public SurveyCategoryRepository SurveyCategoryRepository
        {
            get
            {
                if (this.surveyCategoryRepository == null)
                {
                    this.surveyCategoryRepository = new SurveyCategoryRepository(context);
                }
                return surveyCategoryRepository;
            }
        }
        public QuestionRepository QuestionRepository
        {
            get
            {
                if (this.questionRepository == null)
                {
                    this.questionRepository = new QuestionRepository(context);
                }
                return questionRepository;
            }
        }
        public QuestionAnswerRepository QuestionAnswerRepository
        {
            get
            {
                if (this.questionAnswerRepository == null)
                {
                    this.questionAnswerRepository = new QuestionAnswerRepository(context);
                }
                return questionAnswerRepository;
            }
        }
        public QuestionTypeRepository QuestionTypeRepository
        {
            get
            {
                if (this.questionTypeRepository == null)
                {
                    this.questionTypeRepository = new QuestionTypeRepository(context);
                }
                return questionTypeRepository;
            }
        }
        public ResponseRepository ResponseRepository
        {
            get
            {
                if(this.responseRepository == null)
                {
                    this.responseRepository = new ResponseRepository(context);
                }
                return responseRepository;
            }
        }
        public ShowResultDataRepository ShowResultDataRepository
        {
            get
            {
                if (this.showResultDataRepository == null)
                {
                    this.showResultDataRepository = new ShowResultDataRepository(context);
                }
                return showResultDataRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
