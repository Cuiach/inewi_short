namespace Inewi_Console.Entities
{
    public class HistoryOfLeaves
    {
        public List<Leave> Leaves { get; set; } = [];

        internal void SplitLeaveIntoConsecutiveBusinessDaysBits(Leave leave)
        {
            int leaveId = leave.Id;
            int employeeeId = leave.EmployeeId;
            DateTime dateFrom = leave.DateFrom;
            DateTime dateTo = leave.DateTo;
            bool isOnDemand = leave.IsOnDemand;
            RemoveLeave(leaveId);
            WorkDaysCalculator workDaysCalculator = new();
            List<List<DateTime>> listOfLeaves = workDaysCalculator.GetUninterruptedWorkDays(dateFrom, dateTo);

            int i = 0;
            foreach (var uninterruptedRange in listOfLeaves)
            {
                Leave leaveHere = new(employeeeId, leaveId + i, false);
                leaveHere.IsOnDemand = isOnDemand;
                leaveHere.DateFrom = uninterruptedRange.First();
                leaveHere.DateTo = uninterruptedRange.Last();

                Leaves.Add(leaveHere);
                i++;
            }
        }

        internal int GetLastLeaveYearOfEmployee(int employeeId)
        {
            int year;

            var leaves = Leaves.Where(l => l.EmployeeId == employeeId).ToList();

            if (leaves.Count == 0)
            {
                year = 0;
            }
            else
            {
                DateTime lastLeaveDate = leaves.Max(l => l.DateTo);
                year = lastLeaveDate.Year;
            }

            return year;
        }

        public bool CheckOverlapping(Leave leave)
        {
            List<Leave> leavesOverlapping = Leaves.Where
                (l => l.EmployeeId == leave.EmployeeId).Where
                (l => l.DateTo >= leave.DateFrom).Where
                (l => l.DateFrom <= leave.DateTo).ToList();

            if (leavesOverlapping.Count > 0)
            {
                Console.Write("Overlapping: ");
                foreach (Leave l in leavesOverlapping)
                {
                    Leave.DisplayLeaveDetails(l);
                }
                return false;
            }
            return true;
        }

        public void AddLeave(Leave leave, bool askIfOnDemand)
        {
            if (leave.DateFrom.Year == DateTime.Now.Year && askIfOnDemand == true)
            {
                Console.WriteLine("Is this leave On Demand? (click y to nod or n to deny or enter to skip)");

                string input = Console.ReadLine();

                bool _ = (input == "y") ? (leave.IsOnDemand = true) : ((input == "n") ? leave.IsOnDemand = false : true);
            }

            Leaves.Add(leave);
        }

        public void DisplayAllLeaves()
        {
            AuxiliaryMethods.DisplayLeaves(Leaves);
        }

        public void DisplayAllLeavesOnDemand()
        {
            var onDemandLeaves = Leaves.Where(l => l.IsOnDemand).ToList();
            AuxiliaryMethods.DisplayLeaves((List<Leave>)onDemandLeaves);
        }

        public void DisplayAllLeavesForEmployee(int employeeId)
        {
            var leavesOfEmployee = Leaves.Where(l => l.EmployeeId == employeeId).ToList();
            AuxiliaryMethods.DisplayLeaves((List<Leave>)leavesOfEmployee);
        }

        public void DisplayAllLeavesForEmployeeOnDemand(int employeeId)
        {
            var leavesOfEmployeeOnDemand = Leaves.Where
                (l => l.EmployeeId == employeeId).Where
                (l => l.IsOnDemand).ToList();
            AuxiliaryMethods.DisplayLeaves((List<Leave>)leavesOfEmployeeOnDemand);
        }

        public void RemoveLeave(int intOfLeaveToRemove)
        {
            var leaveToRemove = Leaves.FirstOrDefault(c => c.Id == intOfLeaveToRemove);
            if (leaveToRemove == null)
            {
                Console.WriteLine("Leave not found");
            }
            else
            {
                Leaves.Remove(leaveToRemove);
            }
        }

        public int GetSumOfDaysOnLeaveTakenByEmployeeInYear(int employeeId, int year)
        {
            var sumOfLeaveDays = 0;
            foreach (var leave in Leaves)
            {
                if (leave.EmployeeId == employeeId && leave.DateFrom.Year == year)
                {
                    sumOfLeaveDays += leave.GetLeaveLength();
                }
            }
            return sumOfLeaveDays;
        }

        public int GetSumOnDemand(int employeeId)
        {
            var sumOfOnDemandDays = 0;
            foreach (Leave leave in Leaves)
            {
                if (leave.EmployeeId == employeeId && leave.DateFrom.Year == DateTime.Now.Year && leave.IsOnDemand == true)
                {
                    sumOfOnDemandDays += leave.GetLeaveLength();
                }
            }
            return sumOfOnDemandDays;
        }

        public int CountSumOfPastYearLeaveDays(int employeeId, int year)
        {
            int sumOfPreviousYearLeaveDays = 0;
            foreach (var leave in Leaves)
            {
                if (leave.EmployeeId == employeeId && leave.DateFrom.Year == year)
                {
                    sumOfPreviousYearLeaveDays += leave.GetLeaveLength();
                }
            }
            return sumOfPreviousYearLeaveDays;
        }
    }
}