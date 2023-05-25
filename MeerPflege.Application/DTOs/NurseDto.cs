namespace MeerPflege.Application.DTOs
{
  public class NurseDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual int HomeId { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public string NurseAvatarUrl { get; set; }
    public int RoleId { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime StartWorkingDate { get; set; }
  }
}