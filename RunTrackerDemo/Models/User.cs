using System.ComponentModel.DataAnnotations;

namespace RunTrackerDemo.Models
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

        public List<RunningActivity> RunningActivity { get; set; } = new List<RunningActivity>();
    }
}
