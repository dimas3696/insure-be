using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Insure.Models.CRM.Schedules;

namespace Insure.Models.CRM
{
    public class Office
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(13)]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        
        public List<DaySchedule> DaySchedules { get; set; } = new List<DaySchedule>();
    }
}