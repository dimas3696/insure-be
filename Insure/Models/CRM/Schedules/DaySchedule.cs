using System;
using System.Text.Json.Serialization;
using Insure.Models.Helpers;

namespace Insure.Models.CRM.Schedules
{
    public class DaySchedule
    {
        public int Id { get; set; }
        public Days Day { get; set; }
        public int FromHour { get; set; }
        public int ToHour { get; set; }
        public int FromMinute { get; set; }
        public int ToMinute { get; set; }
        
        [JsonIgnore]
        public int OfficeId { get; set; }
        [JsonIgnore]
        public Office Office { get; set; }
    }
}