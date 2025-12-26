namespace BabyFeedingLog.Models;

public class DiaperEntry
{
    public DateTime Time { get; set; }
    public string Type { get; set; } = string.Empty; // Wet, Dirty, Both
    public string Notes { get; set; } = string.Empty;
}
