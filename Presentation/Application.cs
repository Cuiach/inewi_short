﻿using Inewi_Console.Entities;

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

        public void AddLeave(int employeeId)
        {
            listOfEmployees.AddLeave(employeeId);
        }

        public void DisplayAllLeaves()
        {
            listOfEmployees.DisplayAllLeaves();
        }

        public void DisplayAllLeavesForEmployee(int employeeId)
        {
            listOfEmployees.DisplayAllLeavesForEmployee(employeeId);
        }
        public void RemoveLeave(int intOfLeaveToRemove)
        {
            listOfEmployees.RemoveLeave(intOfLeaveToRemove);
        }
    }
}