using System.ComponentModel.DataAnnotations;

namespace MeerPflege.Domain
{
    public class WallItemAttachment
    {
        [Key]
        public int Id { get; set; }

        public virtual int WallItemId { get; set; }
        public virtual WallItem WallItem { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}