public static class TimeFormatter
{
    public static string Format(int seconds)
    {
        var formattedSeconds = seconds % 60;
        var formattedMin = seconds / 60 % 60;
        var formattedHours = seconds / 3600;
        string detailed = string.Format("{0:D2}:{1:D2}:{2:D2}", formattedHours, formattedMin, formattedSeconds); // e.g., "04:30:15"
        return detailed;
    }
}