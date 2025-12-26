namespace BabyFeedingLog.Models;

public class FeedingEntry
{
    public DateTime Time { get; set; }
    public string FeedingType { get; set; } = string.Empty; // Breast, Bottle, Solid
    public double Amount { get; set; } // in ml or oz
    public string Unit { get; set; } = "ml";
    public string Notes { get; set; } = string.Empty;
}
