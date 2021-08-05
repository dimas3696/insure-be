using System;
using System.Collections;
using System.Collections.Generic;
using Insure.Models.CRM.News;

namespace Insure.Models.CRM
{
    public class New
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ShortText { get; set; }
        public string FullText { get; set; }
        public DateTime PublishDate { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}