namespace MeerPflege.Application.DTOs
{
    public class ActivityElderDto
    {
        public int Id { get; set; }

        public int ActivityId { get; set; }

        public int ElderId { get; set; }

        public string ElderName { get; set; }

        public bool IsPresent { get; set;}
    }
}