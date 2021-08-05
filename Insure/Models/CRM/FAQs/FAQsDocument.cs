namespace Insure.Models.CRM.FAQs
{
    public class FAQsDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DownloadLink { get; set; }
        
        public int FaqId { get; set; }
        public FAQ Faq { get; set; }
    }
}