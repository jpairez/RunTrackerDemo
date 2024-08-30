﻿namespace Application.Contract.Dto
{
    public class CreateUserDto
    {
        public required string Name { get; set; }
        public int WeightInKilogram { get; set; }
        public int HeightInCentimeter { get; set; }
        public DateTime Birthdate { get; set; }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - Birthdate.Year;
                return age;
            }
        }
        public int BMI
        {
            get
            {
                if (HeightInCentimeter <= 0 || WeightInKilogram <= 0)
                {
                    return 0;
                }

                return WeightInKilogram / (HeightInCentimeter * HeightInCentimeter);
            }
        }
    }
}
