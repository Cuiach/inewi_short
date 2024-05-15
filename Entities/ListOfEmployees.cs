using System.Globalization;

namespace Inewi_Console.Entities
{
    public class ListOfEmployees
    {
        public ListOfEmployees()
        {
            HistoryOfLeaves allLeavesInStorage = new();
            this.allLeavesInStorage = allLeavesInStorage;
        }
        public List<Employee> Employees { get; set; } = [];
        private HistoryOfLeaves allLeavesInStorage;

        //employee-related methods
        private void DisplayEmployeeDetails(Employee employee, int leaveDaysTaken)
        {
            LeaveLimit leaveLimitThisYear = employee.LeaveLimits.First(l => l.Year == DateTime.Now.Year);
            int previousYear = DateTime.Now.Year - 1;
            int leaveDaysAvailable = leaveLimitThisYear.Limit;

            if (employee.DayOfJoining.Year <= previousYear)
            {
                leaveDaysAvailable += CountExcessLeaveFromPast(employee, previousYear);
            }

            Console.Write($"Employee: {employee.FirstName}, {employee.LastName}, {employee.Id}, leaves: {leaveDaysTaken}/{leaveDaysAvailable}, " +
                $"joined: {employee.DayOfJoining.Year}.{employee.DayOfJoining.Month}.{employee.DayOfJoining.Day},");
            for (int i = employee.DayOfJoining.Year; i <= DateTime.Now.Year; i++)
            {
                LeaveLimit limitPerYear = employee.LeaveLimits.FirstOrDefault(l => l.Year == i);
                Console.Write($" {i}/{limitPerYear.Limit}");
            }
            Console.WriteLine();
            ShowLeaveAvailableForAllPastYears(employee);
        }

        public void AddEmployee()
        {
            Console.WriteLine("Insert first name");
            var firstName = Console.ReadLine();
            Console.WriteLine("Insert last name");
            var lastName = Console.ReadLine();
            Console.WriteLine("Leave limit policy (0, 1, 2 or 3==nolimit):");
            var leaveLimitPolicy = (Console.ReadLine());

            if (firstName != null && lastName != null)
            {
                int newEmployeeId = Employees.Count == 0 ? 1 : Employees.LastOrDefault().Id + 1;

                var newEmployee = new Employee(firstName, lastName, newEmployeeId);

                switch (leaveLimitPolicy)
                {
                    case "0":
                        newEmployee.HowManyYearsToTakePastLeave = Employee.YearsToTakeLeave.CurrentOnly;
                        break;
                    case "1":
                        newEmployee.HowManyYearsToTakePastLeave = Employee.YearsToTakeLeave.OneMore;
                        break;
                    case "2":
                        newEmployee.HowManyYearsToTakePastLeave = Employee.YearsToTakeLeave.TwoMore;
                        break;
                    case "3":
                        newEmployee.HowManyYearsToTakePastLeave = Employee.YearsToTakeLeave.NoLimit;
                        break;
                    default:
                        Console.WriteLine("Something went wrong - nothing set anew");
                        return;
                }

                Employees.Add(newEmployee);
                DisplayEmployeeDetails(newEmployee, 0);
            }
        }

        public void DisplayAllEmployees()        
        {
            foreach (var employee in Employees)
            {
                var leaveDaysTakenThisYear = allLeavesInStorage.GetSumOfDaysOnLeaveTakenByEmployeeInYear(employee.Id, DateTime.Now.Year);
                DisplayEmployeeDetails(employee, leaveDaysTakenThisYear);
            }
        }

        //leave-related methods

        private static bool IsLeaveAfterDateOfRecruitment(Employee employee, Leave leave)
        {
            if (leave.DateFrom < employee.DayOfJoining)
            {
                Console.WriteLine("Leave end cannot be older than the day of recruitment. Try again with correct dates.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsLeaveLimitPolicySatisfied(Employee employee)
        {
            for (int i = employee.DayOfJoining.Year; i <= DateTime.Now.Year; i++)
            {
                if (CountLeaveAvailable(employee, i, 0) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private int ExcessLeaveFromPastYearCurrentOnly(Employee employee, int k)
        {
            return 0;
        } //name may be misleading... "CurrentOnly" refers to for how many years not-taken past leave may be taken. In this method - only in current year.
        
        private int ExcessLeaveFromPastYearOneMore(Employee employee, int k)
        {
            LeaveLimit leaveLimitInYearK = employee.LeaveLimits.First(l => l.Year == k);
            int sumOfLeavesInYearK = allLeavesInStorage.CountSumOfPastYearLeaveDays(employee.Id, k);

            if (employee.DayOfJoining.Year != k)
            {
                return Math.Min(leaveLimitInYearK.Limit, (leaveLimitInYearK.Limit - sumOfLeavesInYearK + ExcessLeaveFromPastYearOneMore(employee, k - 1)));
            }
            else
            {
                return leaveLimitInYearK.Limit - sumOfLeavesInYearK;
            }
        }
        
        private int ExcessLeaveFromPastYearTwoMore(Employee employee, int k)
        {
            LeaveLimit leaveLimitInYearK = employee.LeaveLimits.First(l => l.Year == k);
            int sumOfLeavesInYearK = allLeavesInStorage.CountSumOfPastYearLeaveDays(employee.Id, k);

            if (employee.DayOfJoining.Year == k)
            {
                return (leaveLimitInYearK.Limit - sumOfLeavesInYearK);
            }
            else if (employee.DayOfJoining.Year == k - 1)
            {
                return (leaveLimitInYearK.Limit - sumOfLeavesInYearK + ExcessLeaveFromPastYearTwoMore(employee, k - 1));
            }
            else
            {
                LeaveLimit leaveLimitInYearPreviousToK = employee.LeaveLimits.FirstOrDefault(l => l.Year == k - 1);
                int result = Math.Min(leaveLimitInYearK.Limit + leaveLimitInYearPreviousToK.Limit, leaveLimitInYearK.Limit - sumOfLeavesInYearK + ExcessLeaveFromPastYearTwoMore(employee, k - 1));
                return result;
            }
        }
        
        private int ExcessLeaveFromPastYearNoLimit(Employee employee, int k)
        {
            LeaveLimit leaveLimitInYearK = employee.LeaveLimits.First(l => l.Year == k);
            int sumOfLeavesInYearK = allLeavesInStorage.CountSumOfPastYearLeaveDays(employee.Id, k);

            if (employee.DayOfJoining.Year != k)
            {
                return leaveLimitInYearK.Limit - sumOfLeavesInYearK + ExcessLeaveFromPastYearNoLimit(employee, k - 1);
            }
            else
            {
                return leaveLimitInYearK.Limit - sumOfLeavesInYearK;
            }
        }
        
        private int CountExcessLeaveFromPast(Employee employee, int year)
        {
            int countedExcess = 0;
            switch (employee.HowManyYearsToTakePastLeave)
            {
                case Employee.YearsToTakeLeave.CurrentOnly:
                    countedExcess += ExcessLeaveFromPastYearCurrentOnly(employee, year);
                    break;
                case Employee.YearsToTakeLeave.OneMore:
                    countedExcess += ExcessLeaveFromPastYearOneMore(employee, year);
                    break;
                case Employee.YearsToTakeLeave.TwoMore:
                    countedExcess += ExcessLeaveFromPastYearTwoMore(employee, year);
                    break;
                case Employee.YearsToTakeLeave.NoLimit:
                    countedExcess += ExcessLeaveFromPastYearNoLimit(employee, year);
                    break;
            }
            return countedExcess;
        }
        
        private int CountLeaveAvailable(Employee employee, int year, int whetherToDisplayLeaveDetails)
        {
            int result = 0;
            LeaveLimit leaveLimit = employee.LeaveLimits.First(l => l.Year == year);
            result = leaveLimit.Limit - allLeavesInStorage.GetSumOfDaysOnLeaveTakenByEmployeeInYear(employee.Id, year);

            if (whetherToDisplayLeaveDetails == 1)
            {
                Console.Write($"In {year} year: {leaveLimit.Limit} limit and {allLeavesInStorage.GetSumOfDaysOnLeaveTakenByEmployeeInYear(employee.Id, year)} taken.");
            }

            if (employee.DayOfJoining.Year < year)
            {
                result += CountExcessLeaveFromPast(employee, year - 1);
            }
            return result;
        }
        
        private void ShowLeaveAvailableForAllPastYears(Employee employee) //rather for test purpose
        {
            int startingYear = employee.DayOfJoining.Year;

            Console.WriteLine("TEST PURPOSE:");

            for (int i = startingYear; i <= DateTime.Now.Year; i++)
            {
                int k = CountLeaveAvailable(employee, i, 1);
                Console.WriteLine($" Accrued leave available in {i}: {k}");
            }
        }
        
        public void AddLeave(int employeeId)
        {
            Employee employee = Employees.FirstOrDefault(e => e.Id == employeeId);

            int lastAddedLeaveId = allLeavesInStorage.Leaves.Count == 0 ? 0 : allLeavesInStorage.Leaves.LastOrDefault().Id;
            Leave leave = new(employeeId, lastAddedLeaveId + 1);

            Console.WriteLine("Default leave dates are set to: from {0}, to {1}. Do you want to keep them? Press enter if yes. Put n and enter if you want to set the dates manually", leave.DateFrom.ToString("yyyy-MM-dd"), leave.DateTo.ToString("yyyy-MM-dd"));
            if (Console.ReadLine() == "n")
            {
                Console.WriteLine("Put date - beginning of leave");
                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime userDateFrom))
                {
                    Console.WriteLine("The day of the week is: " + userDateFrom.DayOfWeek);
                    leave.DateFrom = userDateFrom;
                    if (!IsLeaveAfterDateOfRecruitment(employee, leave))
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("You entered an incorrect value.");
                }

                Console.WriteLine("Put date - end of leave");
                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime userDateTo))
                {
                    Console.WriteLine("The day of the week is: " + userDateTo.DayOfWeek);
                    leave.DateTo = userDateTo;
                    if (leave.DateFrom > leave.DateTo)
                    {
                        Console.WriteLine("Leave end cannot be older than leave start.");
                        return;
                    }

                    if (!leave.IsLeaveInOneYear())
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("You entered an incorrect value. Leave was not added.");
                    return;
                }
            }

            if(!IsLeaveAfterDateOfRecruitment(employee, leave) || !leave.IsLeaveInOneYear())
            {
                Console.WriteLine("Leave is not added. Either recruitment day is after leave or leave is spans across 2 calendar years.");
                return;
            }

            allLeavesInStorage.AddLeave(leave);
           
            ShowLeaveAvailableForAllPastYears(employee); //TEST PURPOSE

            if (!IsLeaveLimitPolicySatisfied(employee))
            {
                RemoveLeave(leave.Id);
                Console.WriteLine("Leave is not added. Leave limit policy is violated.");
            }
        }
        
        public void DisplayAllLeaves()
        {
            allLeavesInStorage.DisplayAllLeaves();
        }
        
        public void DisplayAllLeavesForEmployee(int employeeId)
        {
            allLeavesInStorage.DisplayAllLeavesForEmployee(employeeId);
        }

        public void RemoveLeave(int intOfLeaveToRemove)
        {
            allLeavesInStorage.RemoveLeave(intOfLeaveToRemove);
        }
    }
}