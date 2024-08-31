namespace Application.RunTracker.Data.Models
{
    public class RunningActivity
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime RunStart { get; set; }
        public DateTime RunEnd { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public int AveragePace { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
