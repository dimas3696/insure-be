using System;
using System.Collections.Generic;
using System.Linq;
using Insure.Models.CRM;
using Insure.Models.CRM.FAQs;
using Insure.Models.CRM.News;
using Insure.Models.CRM.Schedules;
using Microsoft.EntityFrameworkCore;

namespace Insure.Models
{
    public sealed class CrmContext : DbContext
    {
        public DbSet<FAQ> Faqs { get; set; }
        public DbSet<FAQsDocument> FaqsDocuments { get; set; }
        public DbSet<Company> Companies { get; set; }
        
        public DbSet<DaySchedule> DaySchedules { get; set; }
        public DbSet<Office> Offices { get; set; }
        
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<New> News { get; set; }
        
        public DbSet<Review> Reviews { get; set; }

        public CrmContext(DbContextOptions<CrmContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var faqs = new List<FAQ>()
            {
                new FAQ() {Id = 1, Question = "How old are you?", Answer = "22 years"},
                new FAQ() {Id = 2, Question = "What is your name?", Answer = "Dmitriy"}
            };

            modelBuilder.Entity<FAQ>().HasData(faqs);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}