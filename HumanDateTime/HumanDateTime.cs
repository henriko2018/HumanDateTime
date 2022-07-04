namespace HumanDateTime;

public static class HumanDateTime
{
    public static string ToHumanString(this DateTime dateTime)
    {
        var datePart = GetDatePart(dateTime);
        var timePart = GetTimePart(dateTime);
        if (datePart != "")
            datePart += " ";
        return datePart + timePart;
    }

    private static string GetTimePart(DateTime dateTime)
    {
        var diff = dateTime - CurrentDateTimeProvider();
        var past = diff.Ticks <= 0;
        var diffInMinutes = Math.Abs(Math.Round(diff.TotalMinutes));
        return past
            ? diffInMinutes switch
            {
                0 => "nyss",
                1 => "1 minut sedan",
                < 60 => diffInMinutes + " minuter sedan",
                _ => dateTime.TimeOfDay.ToString(TimeSpanFormat)
            }
            : diffInMinutes switch
            {
                0 => "straxt",
                1 => "om 1 minut",
                < 60 => "om " + diffInMinutes + " minuter",
                _ => dateTime.TimeOfDay.ToString(TimeSpanFormat)
            };
    }

    private static string GetDatePart(DateTime dateTime)
    {
        var diff = dateTime - CurrentDateTimeProvider();
        var past = diff.Ticks <= 0;
        var diffInDays = Math.Abs((dateTime.Date - CurrentDateTimeProvider().Date).TotalDays);
        return past
            ? diffInDays switch
            {
                0 => "",
                1 => "i går",
                2 => "i förrgår",
                < 7 => dateTime.DayOfWeek switch
                {
                    DayOfWeek.Sunday => "i söndags",
                    DayOfWeek.Monday => "i måndags",
                    DayOfWeek.Tuesday => "i tisdags",
                    DayOfWeek.Wednesday => "i onsdags",
                    DayOfWeek.Thursday => "i torsdags",
                    DayOfWeek.Friday => "i fredags",
                    DayOfWeek.Saturday => "i lördags",
                    _ => throw new ArgumentOutOfRangeException(nameof(dateTime), $"Unkown day of week \"{dateTime.DayOfWeek}\""),
                },
                _ => dateTime.ToString(DateFormat)
            }
            : diffInDays switch
            {
                0 => "",
                1 => "i morgon",
                2 => "i övermorgon",
                < 7 => dateTime.DayOfWeek switch
                {
                    DayOfWeek.Sunday => "på söndag",
                    DayOfWeek.Monday => "på måndag",
                    DayOfWeek.Tuesday => "på tisdag",
                    DayOfWeek.Wednesday => "på onsdag",
                    DayOfWeek.Thursday => "på torsdag",
                    DayOfWeek.Friday => "på fredag",
                    DayOfWeek.Saturday => "på lördag",
                    _ => throw new ArgumentOutOfRangeException(nameof(dateTime), $"Unkown day of week \"{dateTime.DayOfWeek}\""),
                },
                _ => dateTime.ToString(DateFormat)
            };
    }

    internal static Func<DateTime> CurrentDateTimeProvider = () => DateTime.Now; // Set to constant in tests.

    private const string DateFormat = "yyyy-MM-dd";
    private const string TimeSpanFormat = "hh\\:mm";
}
