using BabyFeedingLog.Models;
using BabyFeedingLog.Services;
using System.Text.RegularExpressions;

namespace BabyFeedingLog;

public partial class Form1 : Form
{
    private InfantInfo _infantInfo = new();
    private readonly DataManager _dataManager;
    private readonly OcrService _ocrService;
    private string? _selectedImagePath;
    private TabControl? _mainTabControl;

    // Compiled regex patterns for better performance
    private static readonly Regex MlPattern = new(@"(\d+\.?\d*)\s*ml", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex OzPattern = new(@"(\d+\.?\d*)\s*oz", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex WeightKgPattern = new(@"weight.*?(\d+\.?\d*)\s*kg", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex WeightLbsPattern = new(@"weight.*?(\d+\.?\d*)\s*(lbs|lb|pounds?)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex HeightCmPattern = new(@"(height|length).*?(\d+\.?\d*)\s*cm", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex HeightInPattern = new(@"(height|length).*?(\d+\.?\d*)\s*(in|inch)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex HeadCmPattern = new(@"head.*?(\d+\.?\d*)\s*cm", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex HeadInPattern = new(@"head.*?(\d+\.?\d*)\s*(in|inch)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public Form1()
    {
        InitializeComponent();
        _dataManager = new DataManager();
        _ocrService = new OcrService();
        LoadData();
        UpdateUI();
        
        // Cache reference to TabControl
        _mainTabControl = Controls.Find("tabControl", true).FirstOrDefault() as TabControl;
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
        if (entry.EndTime < entry.StartTime)
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

    // Helper method to switch to a specific tab
    private void SwitchToTab(string tabName)
    {
        if (_mainTabControl != null)
        {
            for (int i = 0; i < _mainTabControl.TabPages.Count; i++)
            {
                if (_mainTabControl.TabPages[i].Text == tabName)
                {
                    _mainTabControl.SelectedIndex = i;
                    break;
                }
            }
        }
    }

    // OCR Tab
    private void btnSelectImage_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff|All Files|*.*",
            Title = "Select an Image with Text"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                _selectedImagePath = openFileDialog.FileName;
                
                // Dispose previous image to prevent memory leak
                if (picImagePreview.Image != null)
                {
                    picImagePreview.Image.Dispose();
                    picImagePreview.Image = null;
                }
                
                // Load and display the image
                using var originalImage = Image.FromFile(_selectedImagePath);
                picImagePreview.Image = new Bitmap(originalImage);
                
                // Enable extract button
                btnExtractText.Enabled = true;
                txtExtractedText.Clear();
                btnPopulateFeeding.Enabled = false;
                btnPopulateGrowth.Enabled = false;
                
                lblOcrStatusMessage.Text = $"Image loaded: {Path.GetFileName(_selectedImagePath)}";
                lblOcrStatusMessage.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblOcrStatusMessage.Text = "Error loading image.";
                lblOcrStatusMessage.ForeColor = Color.DarkRed;
            }
        }
    }

    private void btnExtractText_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedImagePath))
        {
            MessageBox.Show("Please select an image first.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            lblOcrStatusMessage.Text = "Extracting text from image...";
            lblOcrStatusMessage.ForeColor = Color.DarkBlue;
            Application.DoEvents();

            string extractedText = _ocrService.ExtractTextFromImage(_selectedImagePath);
            
            if (string.IsNullOrWhiteSpace(extractedText))
            {
                txtExtractedText.Text = "(No text detected in image)";
                lblOcrStatusMessage.Text = "No text detected in image.";
                lblOcrStatusMessage.ForeColor = Color.DarkOrange;
            }
            else
            {
                txtExtractedText.Text = extractedText;
                btnPopulateFeeding.Enabled = true;
                btnPopulateGrowth.Enabled = true;
                lblOcrStatusMessage.Text = $"Text extracted successfully! ({extractedText.Length} characters)";
                lblOcrStatusMessage.ForeColor = Color.DarkGreen;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error extracting text: {ex.Message}", "OCR Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblOcrStatusMessage.Text = "Error extracting text.";
            lblOcrStatusMessage.ForeColor = Color.DarkRed;
        }
    }

    private void btnPopulateFeeding_Click(object sender, EventArgs e)
    {
        string text = txtExtractedText.Text;
        if (string.IsNullOrWhiteSpace(text))
        {
            MessageBox.Show("No text available to parse.", "No Text", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            // Simple parsing logic - look for feeding-related keywords and numbers
            var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            // Look for amount/volume
            foreach (var line in lines)
            {
                var lowerLine = line.ToLower();
                
                // Look for ml or oz amounts using compiled regex
                var mlMatch = MlPattern.Match(lowerLine);
                var ozMatch = OzPattern.Match(lowerLine);
                
                if (mlMatch.Success && double.TryParse(mlMatch.Groups[1].Value, out double mlAmount))
                {
                    nudFeedingAmount.Value = (decimal)Math.Min(mlAmount, (double)nudFeedingAmount.Maximum);
                    cmbFeedingUnit.SelectedItem = "ml";
                }
                else if (ozMatch.Success && double.TryParse(ozMatch.Groups[1].Value, out double ozAmount))
                {
                    nudFeedingAmount.Value = (decimal)Math.Min(ozAmount, (double)nudFeedingAmount.Maximum);
                    cmbFeedingUnit.SelectedItem = "oz";
                }
                
                // Look for feeding type keywords
                if (lowerLine.Contains("breast"))
                    cmbFeedingType.SelectedItem = "Breast";
                else if (lowerLine.Contains("bottle"))
                    cmbFeedingType.SelectedItem = "Bottle";
                else if (lowerLine.Contains("solid") || lowerLine.Contains("food"))
                    cmbFeedingType.SelectedItem = "Solid";
            }
            
            // Set the notes with the original text
            txtFeedingNotes.Text = $"Imported from OCR:\n{text}";
            
            // Switch to feeding tab
            SwitchToTab("Feeding");
            
            MessageBox.Show("Feeding form populated with extracted data. Please review and adjust before saving.", 
                "Data Populated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error parsing text: {ex.Message}", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnPopulateGrowth_Click(object sender, EventArgs e)
    {
        string text = txtExtractedText.Text;
        if (string.IsNullOrWhiteSpace(text))
        {
            MessageBox.Show("No text available to parse.", "No Text", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            // Simple parsing logic - look for weight, height, head circumference
            var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var line in lines)
            {
                var lowerLine = line.ToLower();
                
                // Look for weight (kg or lbs) using compiled regex
                var kgMatch = WeightKgPattern.Match(lowerLine);
                var lbsMatch = WeightLbsPattern.Match(lowerLine);
                
                if (kgMatch.Success && double.TryParse(kgMatch.Groups[1].Value, out double kgWeight))
                {
                    nudWeight.Value = (decimal)Math.Min(kgWeight, (double)nudWeight.Maximum);
                    cmbWeightUnit.SelectedItem = "kg";
                }
                else if (lbsMatch.Success && double.TryParse(lbsMatch.Groups[1].Value, out double lbsWeight))
                {
                    nudWeight.Value = (decimal)Math.Min(lbsWeight, (double)nudWeight.Maximum);
                    cmbWeightUnit.SelectedItem = "lbs";
                }
                
                // Look for height (cm or inches) using compiled regex
                var cmMatch = HeightCmPattern.Match(lowerLine);
                var inMatch = HeightInPattern.Match(lowerLine);
                
                if (cmMatch.Success && double.TryParse(cmMatch.Groups[2].Value, out double cmHeight))
                {
                    nudHeight.Value = (decimal)Math.Min(cmHeight, (double)nudHeight.Maximum);
                    cmbHeightUnit.SelectedItem = "cm";
                }
                else if (inMatch.Success && double.TryParse(inMatch.Groups[2].Value, out double inHeight))
                {
                    nudHeight.Value = (decimal)Math.Min(inHeight, (double)nudHeight.Maximum);
                    cmbHeightUnit.SelectedItem = "in";
                }
                
                // Look for head circumference using compiled regex
                var headCmMatch = HeadCmPattern.Match(lowerLine);
                var headInMatch = HeadInPattern.Match(lowerLine);
                
                if (headCmMatch.Success && double.TryParse(headCmMatch.Groups[1].Value, out double headCm))
                {
                    nudHeadCirc.Value = (decimal)Math.Min(headCm, (double)nudHeadCirc.Maximum);
                    cmbHeadCircUnit.SelectedItem = "cm";
                }
                else if (headInMatch.Success && double.TryParse(headInMatch.Groups[1].Value, out double headIn))
                {
                    nudHeadCirc.Value = (decimal)Math.Min(headIn, (double)nudHeadCirc.Maximum);
                    cmbHeadCircUnit.SelectedItem = "in";
                }
            }
            
            // Set the notes with the original text
            txtGrowthNotes.Text = $"Imported from OCR:\n{text}";
            
            // Switch to growth tab
            SwitchToTab("Growth");
            
            MessageBox.Show("Growth form populated with extracted data. Please review and adjust before saving.", 
                "Data Populated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error parsing text: {ex.Message}", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
