namespace Inewi_Console.Entities
{
    public class Employee(string firstName, string lastName, int id)
    {
        public int Id { get; set; } = id;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public DateTime DayOfJoining { get; set; }
        public List<LeaveLimit> LeaveLimits { get; set; } = [];
        public enum YearsToTakeLeave
        {
            CurrentOnly,
            OneMore,
            TwoMore,
            NoLimit
        }
        public YearsToTakeLeave HowManyYearsToTakePastLeave = YearsToTakeLeave.OneMore;
        public int LeavesPerYear { get; set; }
        public int OnDemandPerYear { get; set; }

        internal void AdjustDateOfRecruitmentAndThusLeaveLimits(string newDateOfRecruitmentFromUser)
        {
            int oldYearOfRecruitment = this.DayOfJoining.Year;
            DateTime newDateOfRecruitment = DateTime.ParseExact(newDateOfRecruitmentFromUser, "yyyy-MM-dd", null);
            this.DayOfJoining = newDateOfRecruitment;
            Console.WriteLine($"Date of recruitment is set to: {newDateOfRecruitmentFromUser}");
            for (int i = oldYearOfRecruitment; i < newDateOfRecruitment.Year; i++)
            {
                LeaveLimit limitToRemove = this.LeaveLimits.FirstOrDefault(l => l.Year == i);
                this.LeaveLimits.Remove(limitToRemove);
            }
        }

        internal bool LeaveLimitForCurrentYearExists()
        {
            return this.LeaveLimits.FirstOrDefault(l => l.Year == DateTime.Now.Year) != null;
        }

        internal void PropagateLeaveLimitForCurrentYear(int leavesPerYear, bool isItNewEmployee)
        {
            int currentYear = DateTime.Today.Year;
            this.LeavesPerYear = leavesPerYear;

            if (isItNewEmployee)
            {
                LeaveLimit leavelimit = new(currentYear, this.LeavesPerYear);
                this.LeaveLimits.Add(leavelimit);
                Console.WriteLine($"Leave per year was set to: {this.LeavesPerYear}");
            }
            else
            {
                LeaveLimit leavelimit;
                leavelimit = this.LeaveLimits.FirstOrDefault(l => l.Year == currentYear);
                if (leavelimit != null)
                {
                    leavelimit.Limit = this.LeavesPerYear;
                }
                else
                {
                    leavelimit = new(currentYear, this.LeavesPerYear);
                    this.LeaveLimits.Add(leavelimit);
                }
            }
        }
    }
}