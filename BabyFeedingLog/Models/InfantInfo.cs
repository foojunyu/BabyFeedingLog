namespace BabyFeedingLog.Models;

public class InfantInfo
{
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public List<FeedingEntry> FeedingLog { get; set; } = new();
    public List<DiaperEntry> DiaperLog { get; set; } = new();
    public List<SleepEntry> SleepLog { get; set; } = new();
    public List<GrowthEntry> GrowthLog { get; set; } = new();
}
