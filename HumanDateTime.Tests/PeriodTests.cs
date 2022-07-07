namespace HumanDateTime.Tests;

public class PeriodTests
{
    static PeriodTests() => HumanDateTime.CurrentDateTimeProvider = () => new DateTime(2022, 06, 01, 13, 00, 00); // Wednesday

    [Theory]
    [MemberData(nameof(TestData))]
    public void OutputsExpected(DateTime from, DateTime to, string expected)
        => Assert.Equal(expected, HumanDateTime.ToHumanString(from, to));

    [Theory]
    [MemberData(nameof(NullableTestData))]
    public void NullableOutputsExpected(DateTime? from, DateTime? to, string expected)
        => Assert.Equal(expected, HumanDateTime.ToHumanString(from, to));

    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { new DateTime(2022, 06, 01, 08, 00, 00), new DateTime(2022, 06, 01, 17, 00, 00), "i dag 08:00-17:00" },
            new object[] { new DateTime(2022, 06, 01, 08, 00, 00), new DateTime(2022, 06, 02, 17, 00, 00), "i dag 08:00 - i morgon 17:00"}
        };

    public static IEnumerable<object?[]> NullableTestData =>
        new List<object?[]>
        {
            new object?[] { new DateTime?(new DateTime(2022, 06, 01, 08, 00, 00)), new DateTime?(new DateTime(2022, 06, 01, 17, 00, 00)), "i dag 08:00-17:00" },
            new object?[] { new DateTime?(new DateTime(2022, 06, 01, 08, 00, 00)), new DateTime?(new DateTime(2022, 06, 02, 17, 00, 00)), "i dag 08:00 - i morgon 17:00"},
            new object?[] { new DateTime?(new DateTime(2022, 06, 01, 08, 00, 00)), null, "i dag 08:00 -"},
            new object?[] { null, new DateTime?(new DateTime(2022, 06, 02, 17, 00, 00)), "- i morgon 17:00"},
        };
}