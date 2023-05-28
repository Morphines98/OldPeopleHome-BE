namespace MeerPflege.Application.DTOs
{
    public class ElderDto
    {
         public int Id { get; set; }
        public int HomeId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int GroupId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Hobies { get; set; }
        public int CarerId { get; set; }
        public string MedicalConditions { get; set; }
    }
}