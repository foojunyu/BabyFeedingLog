namespace BabyFeedingLog.Models;

public class GrowthEntry
{
    public DateTime Date { get; set; }
    public double Weight { get; set; } // in kg or lbs
    public string WeightUnit { get; set; } = "kg";
    public double Height { get; set; } // in cm or inches
    public string HeightUnit { get; set; } = "cm";
    public double HeadCircumference { get; set; } // in cm or inches
    public string HeadCircumferenceUnit { get; set; } = "cm";
    public string Notes { get; set; } = string.Empty;
}
