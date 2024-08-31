namespace Application.RunTracker.Contracts.Dto
{
    public class RunningActivityDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime RunStart { get; set; }
        public DateTime RunEnd { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public int AveragePace { get; set; }
        public int UserId { get; set; }
    }
}
