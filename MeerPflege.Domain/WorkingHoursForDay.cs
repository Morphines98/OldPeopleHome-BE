using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeerPflege.Domain
{
  public class WorkingHoursForDay
  {
    [Key]
    public int Id { get; set; }
    public virtual int HomeId { get; set; }
    public virtual Home Home { get; set; }
    public string DayId { get; set; }

    public virtual List<WorkingInterval> WorkingIntervals { get; set; }
  }
}