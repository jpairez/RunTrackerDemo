namespace Application.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Birthdate { get; set; }
        public int Age { get; set; }
        public int BMI { get; set; }

        public List<RunningActivity> RunningActivities { get; set; } = new List<RunningActivity>();
    }
}
