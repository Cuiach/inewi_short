namespace Inewi_Console.Entities
{
    internal class Menus
    {
        public static void ShowMainMenu()
        {
            Console.WriteLine("1 Add employee");
            Console.WriteLine("2 Display all employees");
            Console.WriteLine("3 Search employee");
            Console.WriteLine("4 Remove employee");
            Console.WriteLine("5 Add employee's leave");
            Console.WriteLine("6 Display all leaves (you can add D - ON DEMAND only or/and add E - for 1 employee only: 6, 6D, 6E, 6ED)");
            Console.WriteLine("7 Remove leave");
            Console.WriteLine("8 Edit leave");
            Console.WriteLine("9 Edit employee's settings");
            Console.WriteLine(" If you want to see this menu insert 'm'");
            Console.WriteLine(" To exit insert 'x'");
        }
    }
}