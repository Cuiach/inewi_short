namespace Inewi_Short.Entities
{
    public class Employee(string firstName, string lastName, int id)
    {
        public int Id { get; set; } = id;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public DateTime DayOfJoining { get; set; } = new(2020, 01, 01);
        public List<LeaveLimit> LeaveLimits { get; set; } =
        [
            new(2020, 26),
            new(2021, 26),
            new(2022, 26),
            new(2023, 26),
            new(2024, 26),

        ];
        public enum YearsToTakeLeave
        {
            CurrentOnly,
            OneMore,
            TwoMore,
            NoLimit
        }
        public YearsToTakeLeave HowManyYearsToTakePastLeave;
        public int LeavesPerYear { get; set; } = 26;
    }
}