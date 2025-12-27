namespace BabyFeedingLog.Models;

public class SleepEntry
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime > StartTime ? EndTime - StartTime : TimeSpan.Zero;
    public string Notes { get; set; } = string.Empty;
}
