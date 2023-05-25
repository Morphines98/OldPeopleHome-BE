using System.ComponentModel.DataAnnotations;

namespace MeerPflege.Domain
{
    public class Nurse
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual int HomeId { get; set; }
        public virtual Home Home { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        public  string NurseAvatarUrl { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartWorkingDate { get; set; }

    }
}