using System;

namespace Insure.Models.CRM
{
    public class Review
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Text { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
    }
}