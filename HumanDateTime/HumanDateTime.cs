namespace HumanDateTime;

public static class HumanDateTime
{
    public static string ToHumanString(this DateTime dateTime)
    {

        var diff = dateTime - CurrentDateTimeProvider();
        var past = diff.Ticks <= 0;
        var diffInMinutes = Math.Abs(Math.Round(diff.TotalMinutes));
        var diffInDays = Math.Abs((dateTime.Date - CurrentDateTimeProvider().Date).TotalDays);
        string datePart;
        string timePart;

        if (past)
        {
            timePart = diffInMinutes switch
            {
                0 => "nyss",
                1 => "1 minut sedan",
                < 60 => diffInMinutes + " minuter sedan",
                _ => dateTime.TimeOfDay.ToString(TimeSpanFormat)
            };
            datePart = diffInDays switch
            {
                0 => "",
                1 => "i går ",
                2 => "i förrgår ",
                < 7 => dateTime.DayOfWeek switch
                {
                    DayOfWeek.Sunday => "i söndags ",
                    DayOfWeek.Monday => "i måndags ",
                    DayOfWeek.Tuesday => "i tisdags ",
                    DayOfWeek.Wednesday => "i onsdags ",
                    DayOfWeek.Thursday => "i torsdags ",
                    DayOfWeek.Friday => "i fredags ",
                    DayOfWeek.Saturday => "i lördags ",
                    _ => throw new ArgumentOutOfRangeException(nameof(dateTime), $"Unkown day of week \"{dateTime.DayOfWeek}\""),
                },
                _ => dateTime.ToString(DateFormat) + " "
            };
        }
        else
        {
            timePart = diffInMinutes switch
            {
                0 => "straxt",
                1 => "om 1 minut",
                < 60 => "om " + diffInMinutes + " minuter",
                _ => dateTime.TimeOfDay.ToString(TimeSpanFormat)
            };
            datePart = diffInDays switch
            {
                0 => "",
                1 => "i morgon ",
                2 => "i övermorgon ",
                < 7 => dateTime.DayOfWeek switch
                {
                    DayOfWeek.Sunday => "på söndag ",
                    DayOfWeek.Monday => "på måndag ",
                    DayOfWeek.Tuesday => "på tisdag ",
                    DayOfWeek.Wednesday => "på onsdag ",
                    DayOfWeek.Thursday => "på torsdag ",
                    DayOfWeek.Friday => "på fredag ",
                    DayOfWeek.Saturday => "på lördag ",
                    _ => throw new ArgumentOutOfRangeException(nameof(dateTime), $"Unkown day of week \"{dateTime.DayOfWeek}\""),
                },
                _ => dateTime.ToString(DateFormat) + " "
            };
        }
        return datePart + timePart;
    }

    internal static Func<DateTime> CurrentDateTimeProvider = () => DateTime.Now; // Set to constant in tests.

    private const string DateFormat = "yyyy-MM-dd";
    private const string TimeSpanFormat = "hh\\:mm";
}
