using System.ComponentModel.DataAnnotations;

namespace MeerPflege.Domain
{
    public class Nurse
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Home Home { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public int GroupId { get; set; }
        public  string NurseAvatarUrl { get; set; }
        public DateTime StartWorkingDate { get; set; }

    }
}