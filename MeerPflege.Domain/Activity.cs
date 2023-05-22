namespace MeerPflege.Domain
{
    public class Activity
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string  City { get; set; }
    }
}