using ParamTech.Core.Persistence.Utilities.Response;
using System.Globalization;
namespace ParamTech.Core.Persistence.Utilities.Helpers;
public class DateTimeProcess
{
    public static DateTimeInfo DateTimeInfo()
    {
        var Date =Convert.ToDateTime( DateTime.Now.ToShortDateString());
        DateTime dt_Yil_ilkGun = new DateTime(Date.Year, 1, 1);
        DateTime dt_Yil_sonGun = dt_Yil_ilkGun.AddYears(1).AddDays(-1);
        DateTime dt_Ay_ilkGun = new DateTime(Date.Year, DateTime.Now.Month, 1);
        DateTime dt_Ay_sonGun = dt_Ay_ilkGun.AddMonths(1).AddDays(-1);

        int thisWeekNumber = GetIso8601WeekOfYear(DateTime.Today);
        DateTime firstDayOfWeek = FirstDateOfWeek(Date.Year, thisWeekNumber, CultureInfo.CurrentCulture);
        DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);
        var dateTimeInfo = new DateTimeInfo
        {
            Bugün = Date,   
            Day = Date.Day,
            Month = Date.Month,
            Year = Date.Year,
            AyinIlkGunu= dt_Ay_ilkGun,
            AyinSonGunu= dt_Ay_sonGun,  
            YilinIlkGunu= dt_Yil_ilkGun,
            YilinSonGunu= dt_Yil_sonGun,
            HaftaninIlkGunu= firstDayOfWeek,
            HaftaninSonGunu= lastDayOfWeek, 
        };
        return dateTimeInfo;    
    }

    public static int GetIso8601WeekOfYear(DateTime time)
    {
        DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        {
            time = time.AddDays(3);
        }

        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    public static DateTime FirstDateOfWeek(int year, int weekOfYear,CultureInfo ci)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
        DateTime firstWeekDay = jan1.AddDays(daysOffset);
        int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
        {
            weekOfYear -= 1;
        }
        return firstWeekDay.AddDays(weekOfYear * 7);
    }
}