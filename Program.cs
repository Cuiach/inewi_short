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
        case "5":
            Console.WriteLine("Insert employee id");
            int employeeId = AuxiliaryMethods.GetId();
            if (employeeId != 0)
            {
                application.AddLeave(employeeId);
            }
            break;
        case "6":
            application.DisplayAllLeaves();
            break;
        case "6E":
            Console.WriteLine("Insert employee id");
            employeeId = AuxiliaryMethods.GetId();
            if (employeeId != 0)
            {
                application.DisplayAllLeavesForEmployee(employeeId);
            }
            break;
        case "7":
            Console.WriteLine("Insert leave id");
            int intOfLeaveToRemove = AuxiliaryMethods.GetId();
            if (intOfLeaveToRemove != 0)
            {
                application.RemoveLeave(intOfLeaveToRemove);
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