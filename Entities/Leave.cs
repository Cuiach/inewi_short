namespace Inewi_Console.Entities
{
    public class Leave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsOnDemand { get; set; }

        public Leave(int employeeId, int numberAsId, bool isManuallyCreated)
        {
            Id = numberAsId;
            EmployeeId = employeeId;

            if (isManuallyCreated)
            {
                DateFrom = DateTime.Today.Date.AddDays(-6);
                DateTo = DateTime.Today.Date;
                IsOnDemand = false;
            }
            else
            {}
        }

        internal static void DisplayLeaveDetails(Leave leave)
        {
            string onDemand = "ON DEMAND";
            string notOnDemand = "NOT On Demand";
            string dateFrom = leave.DateFrom.ToString("yyyy-MM-dd");
            string dateTo = leave.DateTo.ToString("yyyy-MM-dd");
            Console.WriteLine($"Leave details Id={leave.Id}, Employee Id={leave.EmployeeId}, leave from: {dateFrom}, leave to: {dateTo}, {(leave.IsOnDemand ? onDemand : notOnDemand)}");
        }

        internal int GetLeaveLength()
        {
            WorkDaysCalculator workDaysCalculator = new();
            return workDaysCalculator.CountWorkDaysBetweenDates(DateFrom, DateTo);
        }
 
        internal int HowManyCalendarYearsLeaveSpans()
        {
            int yearFrom = DateFrom.Year;
            int yearTo = DateTo.Year;
                return yearFrom - yearTo + 1;
        }

        internal int GetWorkDaysOfLeave()
        {
            WorkDaysCalculator calculator = new();
            return calculator.CountWorkDaysBetweenDates(DateFrom, DateTo);
        }
    }
}