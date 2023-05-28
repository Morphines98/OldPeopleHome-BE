namespace MeerPflege.Domain
{
    public class Elder
    {
        public int Id { get; set; }
        public int HomeId { get; set; }
        public virtual Home Home { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int GroupId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Hobies { get; set; }
        public int CarerId { get; set; }
        public virtual Carer Carer { get; set; }
        public string MedicalConditions { get; set; }
        public bool IsDeleted { get; set; }
    }
}