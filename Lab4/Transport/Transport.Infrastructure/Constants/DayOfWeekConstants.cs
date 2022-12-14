namespace Transport.Infrastructure.Constants;

public static class DayOfWeekConstants
{
    public static readonly IReadOnlyDictionary<int, string> DayOfWeeks = new Dictionary<int, string>
    {
        { 1, "Понедельник" },
        { 2, "Вторник" },
        { 3, "Среда" },
        { 4, "Четверг" },
        { 5, "Пятница" },
        { 6, "Субботу" },
        { 7, "Воскресенье" }
    };
}