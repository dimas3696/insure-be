using System.Collections.Generic;
using Insure.Models.CRM.FAQs;

namespace Insure.Models.CRM
{
    public class FAQ
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        
        public List<FAQsDocument> FaqsDocuments { get; set; }
    }
}