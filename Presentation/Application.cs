using Inewi_Console.Entities;

namespace Inewi_Console.Presentation
{
    public class Application
    {
        ListOfEmployees listOfEmployees = new();

        public void AddEmployee()
        {
            listOfEmployees.AddEmployee();
        }

        public void DisplayAllEmployees()
        {
            listOfEmployees.DisplayAllEmployees();
        }

        public void DisplayMatchingEmployees(string searchPhrase)
        {
            listOfEmployees.DisplayMatchingEmployees(searchPhrase);
        }

        public void RemoveEmployee(int intToRemove)
        {
            listOfEmployees.RemoveEmployee(intToRemove);
        }

        public void AddLeave(int employeeId)
        {
            listOfEmployees.AddLeave(employeeId);
        }

        public void DisplayAllLeaves()
        {
            listOfEmployees.DisplayAllLeaves();
        }

        public void DisplayAllLeavesOnDemand()
        {
            listOfEmployees.DisplayAllLeavesOnDemand();
        }

        public void DisplayAllLeavesForEmployee(int employeeId)
        {
            listOfEmployees.DisplayAllLeavesForEmployee(employeeId);
        }

        public void DisplayAllLeavesForEmployeeOnDemand(int employeeId)
        {
            listOfEmployees.DisplayAllLeavesForEmployeeOnDemand(employeeId);
        }

        public void RemoveLeave(int intOfLeaveToRemove)
        {
            listOfEmployees.RemoveLeave(intOfLeaveToRemove);
        }

        public void EditLeave(int intOfLeaveToEdit)
        {
            listOfEmployees.EditLeave(intOfLeaveToEdit);
        }

        public void EditSettings(int EmployeeIdToEdit)
        {
            listOfEmployees.EditSettings(EmployeeIdToEdit);
        }
    }
}