namespace MeerPflege.Application.DTOs
{
    public class ActivityDto
    {
         public int Id { get; set; } 
        public virtual int HomeId { get; set; }
        public string Title { get; set; }
        public string StringDate { get; set; }
        public DateTime Date { get; set; }
        public string SpecialCondition { get; set; }
        public string Description { get; set; }
        public string  Location { get; set; }
        public int GroupId { get; set; }
    }
}