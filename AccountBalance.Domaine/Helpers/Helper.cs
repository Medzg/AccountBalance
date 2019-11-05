using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBalance.Domaine.Helpers
{
    public static class Helper
    {
        public static DateTime GetNextBusinessDay(this DateTime date)
        {
            TimeSpan ts = new TimeSpan(09, 00, 0);
            if (date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek < DayOfWeek.Friday
               && date.Hour >= DateTime.Parse("09:00").Hour && 
               date.Hour <= DateTime.Parse("17:00").Hour)
            {
                DateTime bday = DateTime.UtcNow.AddDays(1).Date + ts;
                return date + bday.Subtract(date);
            }

            if (date.DayOfWeek == DayOfWeek.Friday
               && date.Hour >= DateTime.Parse("09:00").Hour && date.Hour <= DateTime.Parse("17:00").Hour)
                return DateTime.UtcNow.AddDays(3).Date + ts;
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return DateTime.UtcNow.AddDays(2).Date + ts;
            else
               
                return DateTime.UtcNow.AddDays(1).Date + ts;
        }


       
    }
}
