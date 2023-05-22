using System.ComponentModel.DataAnnotations.Schema;

namespace MeerPflege.Domain
{
    public class Home
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }

        public int DailyBreakDuration { get; set; }
        public int GlobalMeetingFrequency { get; set; }
        public int GlobalMeetingDuration { get; set; }
        public int GlobalMeetingPreferredDay { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal WorkingHoursInGroupsPerWeek { get; set; }

        public TimeSpan? CoreTimeStartHours { get; set; }
        public TimeSpan? CoreTimeEndHours { get; set; }

        public TimeSpan? BreakTimeStart { get; set; }
        public TimeSpan? BreakTimeEnd { get; set; }

        public int NumberOfProfessionalEducatorsPresentInCoreTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TemporaryWorkersWorkHoursPerWeek { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PracticantsWorkHoursPerWeek { get; set; }

        public bool IsDeleted { get; set; }
    }
}