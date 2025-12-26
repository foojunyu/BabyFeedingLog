using BabyFeedingLog.Models;
using BabyFeedingLog.Services;

namespace BabyFeedingLog;

public partial class Form1 : Form
{
    private InfantInfo _infantInfo = new();
    private readonly DataManager _dataManager;

    public Form1()
    {
        InitializeComponent();
        _dataManager = new DataManager();
        LoadData();
        UpdateUI();
    }

    private void LoadData()
    {
        _infantInfo = _dataManager.LoadData() ?? new InfantInfo
        {
            Name = "Baby",
            DateOfBirth = DateTime.Today,
            Gender = "Not specified"
        };
    }

    private void SaveData()
    {
        _dataManager.SaveData(_infantInfo);
    }

    private void UpdateUI()
    {
        lblBabyInfo.Text = $"Baby: {_infantInfo.Name} | DOB: {_infantInfo.DateOfBirth:MM/dd/yyyy} | Age: {GetAge()}";
        
        // Update Baby Info Tab
        txtBabyName.Text = _infantInfo.Name;
        dtpBabyDOB.Value = _infantInfo.DateOfBirth;
        if (!string.IsNullOrEmpty(_infantInfo.Gender))
        {
            int genderIndex = cmbBabyGender.Items.IndexOf(_infantInfo.Gender);
            if (genderIndex >= 0)
                cmbBabyGender.SelectedIndex = genderIndex;
        }
        
        RefreshFeedingLog();
        RefreshDiaperLog();
        RefreshSleepLog();
        RefreshGrowthLog();
        UpdateStatistics();
    }

    private string GetAge()
    {
        var age = DateTime.Today - _infantInfo.DateOfBirth;
        if (age.TotalDays < 1)
            return "< 1 day";
        if (age.TotalDays < 30)
            return $"{(int)age.TotalDays} days";
        if (age.TotalDays < 365)
            return $"{(int)(age.TotalDays / 30)} months";
        return $"{(int)(age.TotalDays / 365)} years";
    }

    // Feeding Tab
    private void btnAddFeeding_Click(object sender, EventArgs e)
    {
        var entry = new FeedingEntry
        {
            Time = dtpFeedingTime.Value,
            FeedingType = cmbFeedingType.SelectedItem?.ToString() ?? "Breast",
            Amount = (double)nudFeedingAmount.Value,
            Unit = cmbFeedingUnit.SelectedItem?.ToString() ?? "ml",
            Notes = txtFeedingNotes.Text
        };
        _infantInfo.FeedingLog.Add(entry);
        SaveData();
        RefreshFeedingLog();
        UpdateStatistics();
        MessageBox.Show("Feeding entry added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void RefreshFeedingLog()
    {
        lstFeedingLog.Items.Clear();
        foreach (var entry in _infantInfo.FeedingLog.OrderByDescending(f => f.Time).Take(20))
        {
            lstFeedingLog.Items.Add($"{entry.Time:MM/dd/yyyy HH:mm} - {entry.FeedingType} - {entry.Amount}{entry.Unit}");
        }
    }

    // Diaper Tab
    private void btnAddDiaper_Click(object sender, EventArgs e)
    {
        var entry = new DiaperEntry
        {
            Time = dtpDiaperTime.Value,
            Type = cmbDiaperType.SelectedItem?.ToString() ?? "Wet",
            Notes = txtDiaperNotes.Text
        };
        _infantInfo.DiaperLog.Add(entry);
        SaveData();
        RefreshDiaperLog();
        UpdateStatistics();
        MessageBox.Show("Diaper entry added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void RefreshDiaperLog()
    {
        lstDiaperLog.Items.Clear();
        foreach (var entry in _infantInfo.DiaperLog.OrderByDescending(d => d.Time).Take(20))
        {
            lstDiaperLog.Items.Add($"{entry.Time:MM/dd/yyyy HH:mm} - {entry.Type}");
        }
    }

    // Sleep Tab
    private void btnAddSleep_Click(object sender, EventArgs e)
    {
        var entry = new SleepEntry
        {
            StartTime = dtpSleepStart.Value,
            EndTime = dtpSleepEnd.Value,
            Notes = txtSleepNotes.Text
        };
        if (entry.EndTime <= entry.StartTime)
        {
            MessageBox.Show("End time must be after start time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _infantInfo.SleepLog.Add(entry);
        SaveData();
        RefreshSleepLog();
        UpdateStatistics();
        MessageBox.Show("Sleep entry added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void RefreshSleepLog()
    {
        lstSleepLog.Items.Clear();
        foreach (var entry in _infantInfo.SleepLog.OrderByDescending(s => s.StartTime).Take(20))
        {
            lstSleepLog.Items.Add($"{entry.StartTime:MM/dd/yyyy HH:mm} - {entry.Duration.Hours}h {entry.Duration.Minutes}m");
        }
    }

    // Growth Tab
    private void btnAddGrowth_Click(object sender, EventArgs e)
    {
        var entry = new GrowthEntry
        {
            Date = dtpGrowthDate.Value,
            Weight = (double)nudWeight.Value,
            WeightUnit = cmbWeightUnit.SelectedItem?.ToString() ?? "kg",
            Height = (double)nudHeight.Value,
            HeightUnit = cmbHeightUnit.SelectedItem?.ToString() ?? "cm",
            HeadCircumference = (double)nudHeadCirc.Value,
            HeadCircumferenceUnit = cmbHeadCircUnit.SelectedItem?.ToString() ?? "cm",
            Notes = txtGrowthNotes.Text
        };
        _infantInfo.GrowthLog.Add(entry);
        SaveData();
        RefreshGrowthLog();
        UpdateStatistics();
        MessageBox.Show("Growth entry added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void RefreshGrowthLog()
    {
        lstGrowthLog.Items.Clear();
        foreach (var entry in _infantInfo.GrowthLog.OrderByDescending(g => g.Date).Take(20))
        {
            lstGrowthLog.Items.Add($"{entry.Date:MM/dd/yyyy} - Weight: {entry.Weight}{entry.WeightUnit}, Height: {entry.Height}{entry.HeightUnit}");
        }
    }

    // Statistics
    private void UpdateStatistics()
    {
        var today = DateTime.Today;
        var todayFeedings = _infantInfo.FeedingLog.Count(f => f.Time.Date == today);
        var todayDiapers = _infantInfo.DiaperLog.Count(d => d.Time.Date == today);
        var todaySleep = _infantInfo.SleepLog.Where(s => s.StartTime.Date == today).Sum(s => s.Duration.TotalHours);

        txtStats.Text = $"Today's Summary ({today:MM/dd/yyyy})\r\n" +
                       $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\r\n" +
                       $"Feedings: {todayFeedings}\r\n" +
                       $"Diapers: {todayDiapers}\r\n" +
                       $"Sleep: {todaySleep:F1} hours\r\n" +
                       $"\r\nTotal Entries:\r\n" +
                       $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\r\n" +
                       $"Feedings: {_infantInfo.FeedingLog.Count}\r\n" +
                       $"Diapers: {_infantInfo.DiaperLog.Count}\r\n" +
                       $"Sleep Sessions: {_infantInfo.SleepLog.Count}\r\n" +
                       $"Growth Measurements: {_infantInfo.GrowthLog.Count}";
    }

    // Baby Info
    private void btnUpdateBabyInfo_Click(object sender, EventArgs e)
    {
        _infantInfo.Name = txtBabyName.Text;
        _infantInfo.DateOfBirth = dtpBabyDOB.Value;
        _infantInfo.Gender = cmbBabyGender.SelectedItem?.ToString() ?? "Not specified";
        SaveData();
        UpdateUI();
        MessageBox.Show("Baby information updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveData();
    }
}
