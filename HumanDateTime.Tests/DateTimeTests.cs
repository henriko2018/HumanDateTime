namespace HumanDateTime.Tests;

public class DateTimeTests
{
    static DateTimeTests() => HumanDateTime.CurrentDateTimeProvider = () => new DateTime(2022, 06, 01, 13, 00, 00); // Wednesday

    [Theory]
    [MemberData(nameof(TestData))]
    public void OutputsExpected(DateTime date, string expected) => Assert.Equal(expected, date.ToHumanString());

    [Theory]
    [MemberData(nameof(NullableTestData))]
    public void NullableReturnsExpected(DateTime? date, string expected) => Assert.Equal(expected, date.ToHumanString());

    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { new DateTime(2022, 06, 01, 12, 59, 31), "nyss" },
            new object[] { new DateTime(2022, 06, 01, 12, 59, 29), "1 minut sedan" },
            new object[] { new DateTime(2022, 06, 01, 12, 58, 00), "2 minuter sedan" },
            new object[] { new DateTime(2022, 06, 01, 12, 01, 00), "59 minuter sedan" },
            new object[] { new DateTime(2022, 06, 01, 12, 00, 00), "12:00" },
            new object[] { new DateTime(2022, 06, 01, 00, 00, 00), "00:00" },
            new object[] { new DateTime(2022, 05, 31, 23, 59, 59), "i går 23:59"},
            new object[] { new DateTime(2022, 05, 31, 00, 00, 00), "i går 00:00"},
            new object[] { new DateTime(2022, 05, 30, 23, 59, 59), "i förrgår 23:59"},
            new object[] { new DateTime(2022, 05, 30, 00, 00, 00), "i förrgår 00:00" },
            new object[] { new DateTime(2022, 05, 29, 23, 59, 59), "i söndags 23:59"},
            new object[] { new DateTime(2022, 05, 26, 00, 00, 00), "i torsdags 00:00"},
            new object[] { new DateTime(2022, 05, 25, 00, 00, 00), "2022-05-25 00:00"},

            new object[] { new DateTime(2022, 06, 01, 13, 00, 29), "straxt" },
            new object[] { new DateTime(2022, 06, 01, 13, 00, 31), "om 1 minut" },
            new object[] { new DateTime(2022, 06, 01, 13, 02, 00), "om 2 minuter" },
            new object[] { new DateTime(2022, 06, 01, 13, 59, 00), "om 59 minuter" },
            new object[] { new DateTime(2022, 06, 01, 14, 00, 00), "14:00" },
            new object[] { new DateTime(2022, 06, 01, 23, 59, 59), "23:59" },
            new object[] { new DateTime(2022, 06, 02, 00, 00, 00), "i morgon 00:00"},
            new object[] { new DateTime(2022, 06, 02, 23, 59, 59), "i morgon 23:59"},
            new object[] { new DateTime(2022, 06, 03, 00, 00, 00), "i övermorgon 00:00"},
            new object[] { new DateTime(2022, 06, 03, 23, 59, 59), "i övermorgon 23:59" },
            new object[] { new DateTime(2022, 06, 04, 00, 00, 00), "på lördag 00:00"},
            new object[] { new DateTime(2022, 06, 07, 00, 00, 00), "på tisdag 00:00"},
            new object[] { new DateTime(2022, 06, 08, 00, 00, 00), "2022-06-08 00:00"},
        };

    public static IEnumerable<object?[]> NullableTestData =>
        new List<object?[]>
        {
            new object?[] { new DateTime?(new DateTime(2022, 06, 01, 12, 59, 31)), "nyss" },
            new object?[] { null, "" }
        };
}