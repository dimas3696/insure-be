using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Insure.Models.CRM.News
{
    public class Tag : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<New> News { get; set; } = new List<New>();
    }
}