namespace Application.RunTracker.Contracts.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public DateTime Birthdate { get; set; }
        public int Age { get; set; }
        public int BMI { get; set; }
    }
}
