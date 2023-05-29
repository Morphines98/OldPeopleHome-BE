using System.ComponentModel.DataAnnotations;

namespace MeerPflege.Domain
{
    public class WallItem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual int HomeId { get; set; }
        public virtual Home Home { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int? GroupId { get; set; }
        public bool? ForAllGroups { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<WallItemAttachment> WallItemAttachments { get; set; }
    }
}
