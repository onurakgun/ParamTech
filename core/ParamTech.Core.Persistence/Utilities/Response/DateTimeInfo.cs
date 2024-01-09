using System;

namespace ParamTech.Core.Persistence.Utilities.Response;
public class DateTimeInfo
{
    public int Year { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public DateTime Bugün { get; set; }
    public DateTime HaftaninIlkGunu { get; set; }
    public DateTime HaftaninSonGunu { get; set; }
    public DateTime AyinIlkGunu { get; set; }
    public DateTime AyinSonGunu { get; set; }
    public DateTime YilinIlkGunu { get; set; }
    public DateTime YilinSonGunu { get; set; }
}