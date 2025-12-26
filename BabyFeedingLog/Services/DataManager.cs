using System.Text.Json;
using BabyFeedingLog.Models;

namespace BabyFeedingLog.Services;

public class DataManager
{
    private readonly string _dataFilePath;

    public DataManager()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string appFolder = Path.Combine(appDataPath, "BabyFeedingLog");
        Directory.CreateDirectory(appFolder);
        _dataFilePath = Path.Combine(appFolder, "infantData.json");
    }

    public void SaveData(InfantInfo infantInfo)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(infantInfo, options);
            File.WriteAllText(_dataFilePath, jsonString);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public InfantInfo? LoadData()
    {
        try
        {
            if (File.Exists(_dataFilePath))
            {
                string jsonString = File.ReadAllText(_dataFilePath);
                return JsonSerializer.Deserialize<InfantInfo>(jsonString);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return null;
    }

    public string GetDataFilePath() => _dataFilePath;
}
