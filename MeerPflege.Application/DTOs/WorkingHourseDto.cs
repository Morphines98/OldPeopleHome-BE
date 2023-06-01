namespace MeerPflege.Application.DTOs
{
  public class WorkingHourseDto
  {
    public string DayId { get; set; }
    public List<DayWorkingHours> WorkingIntervals { get; set; }
  }

  public class DayWorkingHours
  {
    public string StartHours { get; set; }
    public string EndHours { get; set; }
  }
}