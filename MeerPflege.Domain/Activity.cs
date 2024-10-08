namespace MeerPflege.Domain
{
    public class Activity
    {
        public int Id { get; set; } 
        public virtual int HomeId { get; set; }
        public virtual Home Home { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string SpecialCondition { get; set; }
        public string Description { get; set; }
        public string  Location { get; set; }
        public int GroupId { get; set; }
        public bool IsDeleted { get; set; }

    }
}