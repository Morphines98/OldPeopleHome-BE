using System.ComponentModel.DataAnnotations;

namespace MeerPflege.Domain
{
  public class NewsItem
  {
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }

    public string Content { get; set; }
    public virtual int HomeId { get; set; }
    public virtual Home Home { get; set; }
    public bool? ForAllGroups { get; set; }
    public int? GroupId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime AddedDate { get; set; }

    public virtual List<NewsItemAttachment> NewsItemAttachments { get; set; }
  }
}