namespace MeerPflege.Domain
{
    public class ActivityElderPresence
    {
        public int Id { get; set; }

        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }

        public int ElderId { get; set; }

        public virtual Elder Elder { get; set; }

        public bool IsPresent { get; set;}
    }
}