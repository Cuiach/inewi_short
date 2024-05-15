using System.Globalization;

namespace Inewi_Console.Entities
{
    internal class AuxiliaryMethods
    {
        public static int ToInt(string? value)
        {
            try
            {
                int num = int.Parse(value);
                return num;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public static int GetId() 
        {
            Console.WriteLine("insert id");
            var idAsString = (Console.ReadLine() ?? "0");
            bool _ = int.TryParse(idAsString, out int idOrZero);
            return idOrZero;
        }

        public static bool IsValidDate(string input)
        {
            if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime result))
            {
                return result.ToString("yyyy-MM-dd") == input;
            }
            else
            {
                return false;
            }
        }

        public static int SetNewLimit(string? inputFromUser, int defaultNumber)
        {
            int newLimit;
            newLimit = inputFromUser == "" ? defaultNumber : ToInt(inputFromUser);
            newLimit = newLimit < 0 ? defaultNumber : newLimit;
            return newLimit;
        }

        internal static void DisplayLeaves(List<Leave> setOfLeaves)
        {
            List<Leave> orderedLeaves = [.. setOfLeaves.OrderBy(l => l.DateFrom)];
            foreach (var leave in orderedLeaves)
            {
                Leave.DisplayLeaveDetails(leave);
            }
        }
    }
}