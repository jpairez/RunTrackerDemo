﻿namespace Application.RunTracker.Contracts.Dto
{
    public class CreateUpdateRunningActivityDto
    {
        public string Location { get; set; }
        public DateTime RunStart { get; set; }
        public DateTime RunEnd { get; set; }
        public int Distance { get; set; }
        public int Duration
        {
            get
            {
                TimeSpan duration = RunStart - RunEnd;
                return (int)duration.TotalSeconds;
            }
        }
        public int AveragePace
        {
            get
            {
                if (Duration <= 0)
                    return 0;

                return Duration / Distance;
            }
        }
        public int UserId { get; set; }
    }
}
