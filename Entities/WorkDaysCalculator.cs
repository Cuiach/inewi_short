namespace Inewi_Console.Entities
{
    public class WorkDaysCalculator
    {
        public int CountWorkDaysBetweenDates(DateTime startDate, DateTime endDate)
        {
            int howManyWorkDays = 0;
            int daysBetween = (int)(endDate - startDate).TotalDays;
            for (int i = 0; i <= daysBetween; i++)
            {
                DateTime auxiliaryDay = startDate.AddDays(i);
                if (IsBusinessDay(auxiliaryDay))
                {
                    howManyWorkDays++;
                }
            }
            return howManyWorkDays;
        }

        public List<List<DateTime>> GetUninterruptedWorkDays(DateTime startDate, DateTime endDate)
        {
            List<List<DateTime>> uninterruptedWorkDays = new List<List<DateTime>>();

            DateTime currentDay = startDate.Date;
            List<DateTime> currentSequence = new List<DateTime>();

            while (currentDay <= endDate.Date)
            {
                if (IsBusinessDay(currentDay))
                {
                    currentSequence.Add(currentDay);
                }
                else
                {
                    if (currentSequence.Count > 0)
                    {
                        uninterruptedWorkDays.Add(new List<DateTime>(currentSequence));
                        currentSequence.Clear();
                    }
                }

                currentDay = currentDay.AddDays(1);
            }

            if (currentSequence.Count > 0)
            {
                uninterruptedWorkDays.Add(new List<DateTime>(currentSequence));
            }

            return uninterruptedWorkDays;
        }

        private bool IsBusinessDay(DateTime date)
        {
            FreeDays freeAdditionalDaysCalculator = new FreeDays();
            var freeAdditionalDays = freeAdditionalDaysCalculator.GetFreeDaysOfYear(date.Year);
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !freeAdditionalDays.ContainsKey(date);
        }
    }
}