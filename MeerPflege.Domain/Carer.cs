namespace MeerPflege.Domain
{
    public class Carer
    {
        public int Id { get; set; }

        public int HomeId { get; set; }

        public virtual Home Home { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }  
        public string PhoneNumber { get; set; } 
        public string City { get; set; } 
        public string Adress { get; set; } 
        public string PostCode { get; set; } 
         public string CarerAvatarUrl { get; set; }
        public bool IsDeleted { get; set; } 

    }
}