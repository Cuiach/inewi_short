using Inewi_Console.Entities;
using Inewi_Console.Presentation;

Console.WriteLine("--- Leave management app ---");

Menus.ShowMainMenu();

var userInput = Console.ReadLine();

Application application = new();

while (true)
{
    switch (userInput)
    {
        case "M":
        case "m":
            Menus.ShowMainMenu();
            break;
        case "1":
            application.AddEmployee();
            break;
        case "2":
            application.DisplayAllEmployees();
            break;
        case "3":
            Console.WriteLine("Insert search phrase");
            var searchPhrase = (Console.ReadLine() ?? ""); //null checker
            application.DisplayMatchingEmployees(searchPhrase);
            break;
        case "4":
            int intToRemove = AuxiliaryMethods.GetId();
            if (intToRemove != 0)
            {
                application.RemoveEmployee(intToRemove);
            }
            break;
        case "5":
            int employeeId = AuxiliaryMethods.GetId();
            if (employeeId != 0)
            {
                application.AddLeave(employeeId);
            }
            break;
        case "6":
            application.DisplayAllLeaves();
            break;
        case "6D":
            application.DisplayAllLeavesOnDemand();
            break;
        case "6E":
            employeeId = AuxiliaryMethods.GetId();
            if (employeeId != 0)
            {
                application.DisplayAllLeavesForEmployee(employeeId);
            }
            break;
        case "6ED":
        case "6DE":
            employeeId = AuxiliaryMethods.GetId();
            if (employeeId != 0)
            {
                application.DisplayAllLeavesForEmployeeOnDemand(employeeId);
            }
            break;
        case "7":
            int intOfLeaveToRemove = AuxiliaryMethods.GetId();
            if (intOfLeaveToRemove != 0)
            {
                application.RemoveLeave(intOfLeaveToRemove);
            }
            break;
        case "8":
            int intOfLeaveToEdit = AuxiliaryMethods.GetId();
            if (intOfLeaveToEdit != 0)
            {
                application.EditLeave(intOfLeaveToEdit);
            }
            break;
        case "9":
            int intOfEmployeeToEdit = AuxiliaryMethods.GetId();
            if (intOfEmployeeToEdit != 0)
            {
                application.EditSettings(intOfEmployeeToEdit);
            }
            break;
        case "x":
            return;
        default:
            Console.WriteLine("Invalid operation");
            break;
    }

    Console.WriteLine("Select operation");
    userInput = Console.ReadLine();
}