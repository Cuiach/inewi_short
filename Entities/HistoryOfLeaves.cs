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

        public void AddLeave(Leave leave, int ommitterOnDemandAsk)
        {
            if (leave.DateFrom.Year == DateTime.Now.Year && ommitterOnDemandAsk == 1)
            {
                Console.WriteLine("Is this leave On Demand? (click y or enter to skip)");
                if (Console.ReadLine() == "y")
                {
                    leave.IsOnDemand = true;
                }
            }

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

        public void DisplayAllLeavesOnDemand()
        {
            var onDemandLeaves = Leaves.Where(l => l.IsOnDemand).ToList();
            DisplayLeaves((List<Leave>)onDemandLeaves);
        }

        public void DisplayAllLeavesForEmployee(int employeeId)
        {
            var leavesOfEmployee = Leaves.Where(l => l.EmployeeId == employeeId).ToList();
            DisplayLeaves((List<Leave>)leavesOfEmployee);
        }

        public void DisplayAllLeavesForEmployeeOnDemand(int employeeId)
        {
            var leavesOfEmployeeOnDemand = Leaves.Where
                (l => l.EmployeeId == employeeId).Where
                (l => l.IsOnDemand).ToList();
            DisplayLeaves((List<Leave>)leavesOfEmployeeOnDemand);
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