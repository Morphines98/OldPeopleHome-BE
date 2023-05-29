namespace MeerPflege.Application.DTOs
{
    public class WallItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int HomeId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int? GroupId { get; set; }
        public bool? ForAllGroups { get; set; }
        public virtual List<WallItemAttachmentDto> WallItemAttachments { get; set; }
    }
}