
namespace MeerPflege.Application.DTOs
{
  public class NewsItemDto
  {
    public int Id { get; set; }
    public string Title { get; set; }

    public string Content { get; set; }
    public virtual int HomeId { get; set; }
    public bool? ForAllGroups { get; set; }
    public int? GroupId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime AddedDate { get; set; }
    public List<NewsItemAttachmentDto> NewsItemAttachments { get; set; }
  }
}