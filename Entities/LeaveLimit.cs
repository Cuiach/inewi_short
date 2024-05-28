namespace Leave_Manager_Short_Console.Entities
{
    public class LeaveLimit
    {
        public int Year { get; set; }
        public int Limit { get; set; }
        public LeaveLimit(int year, int limit)
        {
            this.Year = year;
            this.Limit = limit;
        }
        public LeaveLimit()
        {
        }
    }
}