using System;
using System.Text.Json;
using BabyFeedingLog.Models;

namespace BabyFeedingLog.Tests;

/// <summary>
/// Simple verification program to test data models and JSON serialization
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("Baby Feeding Log - Data Model Test");
        Console.WriteLine("========================================");
        Console.WriteLine();

        try
        {
            // Test InfantInfo creation
            var infantInfo = new InfantInfo
            {
                Name = "Test Baby",
                DateOfBirth = new DateTime(2024, 1, 1),
                Gender = "Male"
            };
            Console.WriteLine("✓ InfantInfo created successfully");

            // Test FeedingEntry
            var feedingEntry = new FeedingEntry
            {
                Time = DateTime.Now,
                FeedingType = "Bottle",
                Amount = 120.5,
                Unit = "ml",
                Notes = "Test feeding"
            };
            infantInfo.FeedingLog.Add(feedingEntry);
            Console.WriteLine("✓ FeedingEntry added successfully");

            // Test DiaperEntry
            var diaperEntry = new DiaperEntry
            {
                Time = DateTime.Now,
                Type = "Wet",
                Notes = "Test diaper"
            };
            infantInfo.DiaperLog.Add(diaperEntry);
            Console.WriteLine("✓ DiaperEntry added successfully");

            // Test SleepEntry
            var sleepEntry = new SleepEntry
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2),
                Notes = "Test sleep"
            };
            infantInfo.SleepLog.Add(sleepEntry);
            Console.WriteLine($"✓ SleepEntry added successfully (Duration: {sleepEntry.Duration.Hours}h {sleepEntry.Duration.Minutes}m)");

            // Test GrowthEntry
            var growthEntry = new GrowthEntry
            {
                Date = DateTime.Now,
                Weight = 5.5,
                WeightUnit = "kg",
                Height = 55.5,
                HeightUnit = "cm",
                HeadCircumference = 38.5,
                HeadCircumferenceUnit = "cm",
                Notes = "Test growth"
            };
            infantInfo.GrowthLog.Add(growthEntry);
            Console.WriteLine("✓ GrowthEntry added successfully");

            // Test JSON serialization
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(infantInfo, options);
            Console.WriteLine("✓ JSON serialization successful");

            // Test JSON deserialization
            var deserializedInfo = JsonSerializer.Deserialize<InfantInfo>(jsonString);
            if (deserializedInfo != null && 
                deserializedInfo.Name == infantInfo.Name &&
                deserializedInfo.FeedingLog.Count == 1 &&
                deserializedInfo.DiaperLog.Count == 1 &&
                deserializedInfo.SleepLog.Count == 1 &&
                deserializedInfo.GrowthLog.Count == 1)
            {
                Console.WriteLine("✓ JSON deserialization successful");
            }
            else
            {
                Console.WriteLine("✗ JSON deserialization failed");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("All tests passed! ✓");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("Data Summary:");
            Console.WriteLine($"  Baby: {deserializedInfo.Name}");
            Console.WriteLine($"  Feeding entries: {deserializedInfo.FeedingLog.Count}");
            Console.WriteLine($"  Diaper entries: {deserializedInfo.DiaperLog.Count}");
            Console.WriteLine($"  Sleep entries: {deserializedInfo.SleepLog.Count}");
            Console.WriteLine($"  Growth entries: {deserializedInfo.GrowthLog.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Test failed: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
}
