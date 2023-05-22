namespace MeerPflege.Domain
{
    public class HomeGroup
    {
        public int Id { get; set; }
        public virtual int HomeId { get; set; }
        public virtual Home Home { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? NumberOfInhabitantsForOneNurse { get; set; }
        public string ConversationId { get; set; }
        public bool IsDeleted { get; set; }
    }
}