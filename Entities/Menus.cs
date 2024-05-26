namespace Inewi_Short.Entities
{
    internal class Menus
    {
        public static void ShowMainMenu()
        {
            Console.WriteLine("1 Add employee");
            Console.WriteLine("2 Display all employees");
            Console.WriteLine("5 Add employee's leave");
            Console.WriteLine("6 Display all leaves (you can add E - for 1 employee only: 6, 6E)");
            Console.WriteLine("7 Remove leave");
            Console.WriteLine(" If you want to see this menu insert 'm'");
            Console.WriteLine(" To exit insert 'x'");
        }
    }
}