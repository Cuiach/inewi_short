namespace Inewi_Console.Entities
{
    public class HistoryOfLeaves
    {
        public List<Leave> Leaves { get; set; } = [];

        private static void DisplayLeaves(List<Leave> SetOfLeaves)
        {
            foreach (var leave in SetOfLeaves)
            {
                Leave.DisplayLeaveDetails(leave);
            }
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

        public void AddLeave(Leave leave)
        {
            if (CheckOverlapping(leave))
            {
                Leaves.Add(leave);
            }
            else
            {
                Console.WriteLine("Leave cannot be added. Try again with correct dates.");
            }
        }

        public void DisplayAllLeaves()
        {
            DisplayLeaves(Leaves);
        }

        public void DisplayAllLeavesForEmployee(int employeeId)
        {
            var leavesOfEmployee = Leaves.Where(l => l.EmployeeId == employeeId).ToList();
            DisplayLeaves((List<Leave>)leavesOfEmployee);
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