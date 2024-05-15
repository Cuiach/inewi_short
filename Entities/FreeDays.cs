using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inewi_Console.Entities
{
    internal class FreeDays
    {
        Dictionary<DateTime, string> immovableFreeDays = new Dictionary<DateTime, string>
        {
            { new DateTime(2000, 1, 1), "Solemnity of Mary, Mother of God / New Year" },
            { new DateTime(2000, 1, 6), "Epiphany / Three Kings' Day" },
            { new DateTime(2000, 5, 1), "May Day / Labor Day" },
            { new DateTime(2000, 5, 3), "Consitution Day / Mary the Queen of Poland Feast" },
            { new DateTime(2000, 8, 15), "Assumption of the Blessed Virgin Mary" },
            { new DateTime(2000, 11, 1), "All Saints Feast" },
            { new DateTime(2000, 11, 11), "Independence Day" },
            { new DateTime(2000, 12, 25), "Christmas" },
            { new DateTime(2000, 12, 26), "Christmas 2nd Day /  St. Stephen's Day / Boxing Day" }
        };

        readonly Dictionary<int, DateTime> easterInParticularYears = new Dictionary<int, DateTime>
        {
            {2000, new DateTime(2000, 4, 23)},
            {2001, new DateTime(2001, 4, 15)},
            {2002, new DateTime(2002, 3, 31)},
            {2003, new DateTime(2003, 4, 20)},
            {2004, new DateTime(2004, 4, 11)},
            {2005, new DateTime(2005, 3, 27)},
            {2006, new DateTime(2006, 4, 16)},
            {2007, new DateTime(2007, 4, 8)},
            {2008, new DateTime(2008, 3, 23)},
            {2009, new DateTime(2009, 4, 12)},
            {2010, new DateTime(2010, 4, 4)},
            {2011, new DateTime(2011, 4, 24)},
            {2012, new DateTime(2012, 4, 8)},
            {2013, new DateTime(2013, 3, 31)},
            {2014, new DateTime(2014, 4, 20)},
            {2015, new DateTime(2015, 4, 5)},
            {2016, new DateTime(2016, 3, 27)},
            {2017, new DateTime(2017, 4, 16)},
            {2018, new DateTime(2018, 4, 8)},
            {2019, new DateTime(2019, 4, 21)},
            {2020, new DateTime(2020, 4, 12)},
            {2021, new DateTime(2021, 4, 4)},
            {2022, new DateTime(2022, 4, 17)},
            {2023, new DateTime(2023, 4, 9)},
            {2024, new DateTime(2024, 3, 31)},
            {2025, new DateTime(2025, 4, 20)},
            {2026, new DateTime(2026, 4, 5)},
            {2027, new DateTime(2027, 3, 28)},
            {2028, new DateTime(2028, 4, 16)},
            {2029, new DateTime(2029, 4, 1)},
            {2030, new DateTime(2030, 4, 21)},
        };

        private void AddMovableFeastsToDictionary(DateTime dateOfEaster, Dictionary<DateTime, string> dictionary)
        {
            dictionary.Add(dateOfEaster.AddDays(+1), "Easter Monday");
            dictionary.Add(dateOfEaster.AddDays(+49), "Pentecost / Green Week");
            dictionary.Add(dateOfEaster.AddDays(+60), "Corpus Christi");
        }

        public Dictionary<DateTime, string> GetFreeDaysOfYear(int year)
        {
            Dictionary<DateTime, string> freeDaysOfYear = new();

            foreach (KeyValuePair<DateTime, string>  freeDay in immovableFreeDays)
            {
                DateTime newDay = new DateTime(year, freeDay.Key.Month, freeDay.Key.Day);
                freeDaysOfYear[newDay] = freeDay.Value;
            }

            DateTime easter;

            if (easterInParticularYears.ContainsKey(year))
            {
                easter = easterInParticularYears[year];
                AddMovableFeastsToDictionary(easter, freeDaysOfYear);
            }
            
            return freeDaysOfYear;
        }

        
    }
}
