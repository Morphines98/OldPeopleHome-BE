using System.ComponentModel.DataAnnotations;

namespace MeerPflege.Domain
{
  public class NewsItemAttachment
  {
    [Key]
    public int Id { get; set; }

    public virtual int NewsItemId { get; set; }
    public virtual NewsItem NewsItem { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }
    public DateTime AddedDate { get; set; }
    public bool Deleted { get; set; }
  }
}