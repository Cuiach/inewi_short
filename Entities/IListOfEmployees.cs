namespace Leave_Manager_Short_Console.Entities
{
    public interface IListOfEmployees
    {
        List<Employee> Employees { get; set; }

        void AddEmployee();
        void AddLeave(int employeeId);
        void DisplayAllEmployees();
        void DisplayAllLeaves();
        void DisplayAllLeavesForEmployee(int employeeId);
        void RemoveLeave(int intOfLeaveToRemove);
    }
}