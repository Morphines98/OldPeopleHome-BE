using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeerPflege.Domain
{
  public class WorkingInterval
  {
    [Key]
    public int Id { get; set; }
    public virtual int WorkingHoursForDayId { get; set; }
    public virtual WorkingHoursForDay WorkingHoursForDay { get; set; }
    public string StartHours { get; set; }
    public string EndHours { get; set; }
  }
}