using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedBackPlatformDatabase.Models;

namespace FeedBackPlatformDatabase.Context
{
    public class FeedBackPlatformContext : DbContext
    {
        public FeedBackPlatformContext() : base("FeedBackConnectionString") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyCategory> SurveyCategories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<ShowResultData> ShowResultDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<SurveyCategory>()
                .HasMany<Survey>(c => c.Surveys)
                .WithRequired(s => s.SurveyCategory)
                .HasForeignKey<int>(s => s.SurveyCategoryId);

            modelBuilder.Entity<User>()
                .HasMany<Survey>(u => u.Surveys)
                .WithRequired(s => s.User)
                .HasForeignKey<int>(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Survey>()
                .HasMany<Question>(s => s.Questions)
                .WithRequired(q => q.Survey)
                .HasForeignKey<int>(q => q.SurveyId);
                
            modelBuilder.Entity<Question>()
               .HasMany<QuestionAnswer>(q => q.QuestionAnswers)
               .WithRequired(qa => qa.Question)
               .HasForeignKey<int>(qa => qa.QuestionId);

            modelBuilder.Entity<Survey>()
              .HasMany<Response>(s => s.Responses)
              .WithRequired(r => r.Survey)
              .HasForeignKey<int?>(r => r.SurveyId)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
              .HasMany<Response>(q => q.Responses)
              .WithOptional(r => r.Question)
              .HasForeignKey<int?>(r => r.QuestionId);

            modelBuilder.Entity<QuestionAnswer>()
              .HasMany<Response>(qa => qa.Responses)
              .WithOptional(r => r.QuestionAnswer)
              .HasForeignKey<int?>(r => r.QuestionAnswerId)
              .WillCascadeOnDelete(false);
        }
    }
}
