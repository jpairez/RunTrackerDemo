namespace Application.RunTracker.Contracts.Dto
{
    public class CreateUpdateRunningActivityDto
    {
        public string Location { get; set; }
        public DateTime RunStart { get; set; }
        public DateTime RunEnd { get; set; }
        public int DistanceInKilometer { get; set; }
        public int Duration
        {
            get
            {
                TimeSpan duration = RunEnd - RunStart;
                return (int)duration.TotalSeconds / 60;
            }
        }
        public int AveragePace
        {
            get
            {
                if (Duration <= 0)
                    return 0;

                return Duration / DistanceInKilometer;
            }
        }
        public int UserId { get; set; }
    }
}
