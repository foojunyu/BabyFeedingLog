namespace BabyFeedingLog;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        TabControl tabControl;
        TabPage tabFeeding;
        TabPage tabDiaper;
        TabPage tabSleep;
        TabPage tabGrowth;
        TabPage tabBabyInfo;
        TabPage tabStatistics;

        tabControl = new TabControl();
        tabFeeding = new TabPage();
        tabDiaper = new TabPage();
        tabSleep = new TabPage();
        tabGrowth = new TabPage();
        tabBabyInfo = new TabPage();
        tabStatistics = new TabPage();

        SuspendLayout();

        // Main Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 700);
        Text = "Baby Feeding Log - Infant Progress Monitor";
        FormClosing += Form1_FormClosing;

        // Baby Info Label at top
        lblBabyInfo = new Label
        {
            Location = new Point(12, 12),
            Size = new Size(976, 30),
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            Text = "Baby Information"
        };
        Controls.Add(lblBabyInfo);

        // TabControl
        tabControl.Location = new Point(12, 50);
        tabControl.Size = new Size(976, 638);
        tabControl.Controls.Add(tabFeeding);
        tabControl.Controls.Add(tabDiaper);
        tabControl.Controls.Add(tabSleep);
        tabControl.Controls.Add(tabGrowth);
        tabControl.Controls.Add(tabBabyInfo);
        tabControl.Controls.Add(tabStatistics);
        Controls.Add(tabControl);

        // === Feeding Tab ===
        tabFeeding.Text = "Feeding";
        tabFeeding.Padding = new Padding(10);

        var lblFeedingTime = new Label { Location = new Point(20, 20), Size = new Size(100, 23), Text = "Time:" };
        dtpFeedingTime = new DateTimePicker { Location = new Point(130, 17), Size = new Size(200, 23), Format = DateTimePickerFormat.Custom, CustomFormat = "MM/dd/yyyy HH:mm", ShowUpDown = false };
        dtpFeedingTime.Value = DateTime.Now;

        var lblFeedingType = new Label { Location = new Point(20, 55), Size = new Size(100, 23), Text = "Type:" };
        cmbFeedingType = new ComboBox { Location = new Point(130, 52), Size = new Size(200, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbFeedingType.Items.AddRange(new object[] { "Breast", "Bottle", "Solid" });
        cmbFeedingType.SelectedIndex = 0;

        var lblFeedingAmount = new Label { Location = new Point(20, 90), Size = new Size(100, 23), Text = "Amount:" };
        nudFeedingAmount = new NumericUpDown { Location = new Point(130, 87), Size = new Size(100, 23), Maximum = 500, DecimalPlaces = 1 };

        var lblFeedingUnit = new Label { Location = new Point(240, 90), Size = new Size(40, 23), Text = "Unit:" };
        cmbFeedingUnit = new ComboBox { Location = new Point(285, 87), Size = new Size(80, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbFeedingUnit.Items.AddRange(new object[] { "ml", "oz" });
        cmbFeedingUnit.SelectedIndex = 0;

        var lblFeedingNotes = new Label { Location = new Point(20, 125), Size = new Size(100, 23), Text = "Notes:" };
        txtFeedingNotes = new TextBox { Location = new Point(130, 122), Size = new Size(500, 60), Multiline = true };

        btnAddFeeding = new Button { Location = new Point(130, 195), Size = new Size(150, 30), Text = "Add Feeding Entry" };
        btnAddFeeding.Click += btnAddFeeding_Click;

        var lblFeedingLog = new Label { Location = new Point(20, 240), Size = new Size(200, 23), Text = "Recent Feedings:", Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        lstFeedingLog = new ListBox { Location = new Point(20, 270), Size = new Size(900, 300) };

        tabFeeding.Controls.AddRange(new Control[] { lblFeedingTime, dtpFeedingTime, lblFeedingType, cmbFeedingType, lblFeedingAmount, nudFeedingAmount, lblFeedingUnit, cmbFeedingUnit, lblFeedingNotes, txtFeedingNotes, btnAddFeeding, lblFeedingLog, lstFeedingLog });

        // === Diaper Tab ===
        tabDiaper.Text = "Diaper";
        tabDiaper.Padding = new Padding(10);

        var lblDiaperTime = new Label { Location = new Point(20, 20), Size = new Size(100, 23), Text = "Time:" };
        dtpDiaperTime = new DateTimePicker { Location = new Point(130, 17), Size = new Size(200, 23), Format = DateTimePickerFormat.Custom, CustomFormat = "MM/dd/yyyy HH:mm" };
        dtpDiaperTime.Value = DateTime.Now;

        var lblDiaperType = new Label { Location = new Point(20, 55), Size = new Size(100, 23), Text = "Type:" };
        cmbDiaperType = new ComboBox { Location = new Point(130, 52), Size = new Size(200, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbDiaperType.Items.AddRange(new object[] { "Wet", "Dirty", "Both" });
        cmbDiaperType.SelectedIndex = 0;

        var lblDiaperNotes = new Label { Location = new Point(20, 90), Size = new Size(100, 23), Text = "Notes:" };
        txtDiaperNotes = new TextBox { Location = new Point(130, 87), Size = new Size(500, 60), Multiline = true };

        btnAddDiaper = new Button { Location = new Point(130, 160), Size = new Size(150, 30), Text = "Add Diaper Entry" };
        btnAddDiaper.Click += btnAddDiaper_Click;

        var lblDiaperLog = new Label { Location = new Point(20, 205), Size = new Size(200, 23), Text = "Recent Diapers:", Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        lstDiaperLog = new ListBox { Location = new Point(20, 235), Size = new Size(900, 335) };

        tabDiaper.Controls.AddRange(new Control[] { lblDiaperTime, dtpDiaperTime, lblDiaperType, cmbDiaperType, lblDiaperNotes, txtDiaperNotes, btnAddDiaper, lblDiaperLog, lstDiaperLog });

        // === Sleep Tab ===
        tabSleep.Text = "Sleep";
        tabSleep.Padding = new Padding(10);

        var lblSleepStart = new Label { Location = new Point(20, 20), Size = new Size(100, 23), Text = "Start Time:" };
        dtpSleepStart = new DateTimePicker { Location = new Point(130, 17), Size = new Size(200, 23), Format = DateTimePickerFormat.Custom, CustomFormat = "MM/dd/yyyy HH:mm" };
        dtpSleepStart.Value = DateTime.Now;

        var lblSleepEnd = new Label { Location = new Point(20, 55), Size = new Size(100, 23), Text = "End Time:" };
        dtpSleepEnd = new DateTimePicker { Location = new Point(130, 52), Size = new Size(200, 23), Format = DateTimePickerFormat.Custom, CustomFormat = "MM/dd/yyyy HH:mm" };
        dtpSleepEnd.Value = DateTime.Now.AddHours(2);

        var lblSleepNotes = new Label { Location = new Point(20, 90), Size = new Size(100, 23), Text = "Notes:" };
        txtSleepNotes = new TextBox { Location = new Point(130, 87), Size = new Size(500, 60), Multiline = true };

        btnAddSleep = new Button { Location = new Point(130, 160), Size = new Size(150, 30), Text = "Add Sleep Entry" };
        btnAddSleep.Click += btnAddSleep_Click;

        var lblSleepLog = new Label { Location = new Point(20, 205), Size = new Size(200, 23), Text = "Recent Sleep Sessions:", Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        lstSleepLog = new ListBox { Location = new Point(20, 235), Size = new Size(900, 335) };

        tabSleep.Controls.AddRange(new Control[] { lblSleepStart, dtpSleepStart, lblSleepEnd, dtpSleepEnd, lblSleepNotes, txtSleepNotes, btnAddSleep, lblSleepLog, lstSleepLog });

        // === Growth Tab ===
        tabGrowth.Text = "Growth";
        tabGrowth.Padding = new Padding(10);

        var lblGrowthDate = new Label { Location = new Point(20, 20), Size = new Size(100, 23), Text = "Date:" };
        dtpGrowthDate = new DateTimePicker { Location = new Point(130, 17), Size = new Size(200, 23), Format = DateTimePickerFormat.Short };
        dtpGrowthDate.Value = DateTime.Now;

        var lblWeight = new Label { Location = new Point(20, 55), Size = new Size(100, 23), Text = "Weight:" };
        nudWeight = new NumericUpDown { Location = new Point(130, 52), Size = new Size(100, 23), Maximum = 100, DecimalPlaces = 2 };
        var lblWeightUnit = new Label { Location = new Point(240, 55), Size = new Size(40, 23), Text = "Unit:" };
        cmbWeightUnit = new ComboBox { Location = new Point(285, 52), Size = new Size(80, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbWeightUnit.Items.AddRange(new object[] { "kg", "lbs" });
        cmbWeightUnit.SelectedIndex = 0;

        var lblHeight = new Label { Location = new Point(20, 90), Size = new Size(100, 23), Text = "Height:" };
        nudHeight = new NumericUpDown { Location = new Point(130, 87), Size = new Size(100, 23), Maximum = 200, DecimalPlaces = 1 };
        var lblHeightUnit = new Label { Location = new Point(240, 90), Size = new Size(40, 23), Text = "Unit:" };
        cmbHeightUnit = new ComboBox { Location = new Point(285, 87), Size = new Size(80, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbHeightUnit.Items.AddRange(new object[] { "cm", "in" });
        cmbHeightUnit.SelectedIndex = 0;

        var lblHeadCirc = new Label { Location = new Point(20, 125), Size = new Size(100, 23), Text = "Head Circum:" };
        nudHeadCirc = new NumericUpDown { Location = new Point(130, 122), Size = new Size(100, 23), Maximum = 100, DecimalPlaces = 1 };
        var lblHeadCircUnit = new Label { Location = new Point(240, 125), Size = new Size(40, 23), Text = "Unit:" };
        cmbHeadCircUnit = new ComboBox { Location = new Point(285, 122), Size = new Size(80, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbHeadCircUnit.Items.AddRange(new object[] { "cm", "in" });
        cmbHeadCircUnit.SelectedIndex = 0;

        var lblGrowthNotes = new Label { Location = new Point(20, 160), Size = new Size(100, 23), Text = "Notes:" };
        txtGrowthNotes = new TextBox { Location = new Point(130, 157), Size = new Size(500, 60), Multiline = true };

        btnAddGrowth = new Button { Location = new Point(130, 230), Size = new Size(150, 30), Text = "Add Growth Entry" };
        btnAddGrowth.Click += btnAddGrowth_Click;

        var lblGrowthLog = new Label { Location = new Point(20, 275), Size = new Size(200, 23), Text = "Growth Measurements:", Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        lstGrowthLog = new ListBox { Location = new Point(20, 305), Size = new Size(900, 265) };

        tabGrowth.Controls.AddRange(new Control[] { lblGrowthDate, dtpGrowthDate, lblWeight, nudWeight, lblWeightUnit, cmbWeightUnit, lblHeight, nudHeight, lblHeightUnit, cmbHeightUnit, lblHeadCirc, nudHeadCirc, lblHeadCircUnit, cmbHeadCircUnit, lblGrowthNotes, txtGrowthNotes, btnAddGrowth, lblGrowthLog, lstGrowthLog });

        // === Baby Info Tab ===
        tabBabyInfo.Text = "Baby Info";
        tabBabyInfo.Padding = new Padding(10);

        var lblBabyName = new Label { Location = new Point(20, 20), Size = new Size(100, 23), Text = "Name:" };
        txtBabyName = new TextBox { Location = new Point(130, 17), Size = new Size(200, 23) };

        var lblBabyDOB = new Label { Location = new Point(20, 55), Size = new Size(100, 23), Text = "Date of Birth:" };
        dtpBabyDOB = new DateTimePicker { Location = new Point(130, 52), Size = new Size(200, 23), Format = DateTimePickerFormat.Short };

        var lblBabyGender = new Label { Location = new Point(20, 90), Size = new Size(100, 23), Text = "Gender:" };
        cmbBabyGender = new ComboBox { Location = new Point(130, 87), Size = new Size(200, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbBabyGender.Items.AddRange(new object[] { "Male", "Female", "Not specified" });
        cmbBabyGender.SelectedIndex = 2;

        btnUpdateBabyInfo = new Button { Location = new Point(130, 125), Size = new Size(150, 30), Text = "Update Information" };
        btnUpdateBabyInfo.Click += btnUpdateBabyInfo_Click;

        var lblBabyInfoHelp = new Label 
        { 
            Location = new Point(20, 175), 
            Size = new Size(900, 400), 
            Text = "About Baby Feeding Log\r\n\r\n" +
                   "This application helps you monitor and track your infant's progress including:\r\n\r\n" +
                   "• Feeding times, types, and amounts\r\n" +
                   "• Diaper changes\r\n" +
                   "• Sleep schedules and duration\r\n" +
                   "• Growth measurements (weight, height, head circumference)\r\n\r\n" +
                   "All data is automatically saved to your computer and will be available the next time you open the application.\r\n\r\n" +
                   "Use the tabs above to record different types of information about your baby's daily activities and development."
        };

        tabBabyInfo.Controls.AddRange(new Control[] { lblBabyName, txtBabyName, lblBabyDOB, dtpBabyDOB, lblBabyGender, cmbBabyGender, btnUpdateBabyInfo, lblBabyInfoHelp });

        // === Statistics Tab ===
        tabStatistics.Text = "Statistics";
        tabStatistics.Padding = new Padding(10);

        var lblStats = new Label { Location = new Point(20, 20), Size = new Size(200, 23), Text = "Summary & Statistics:", Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        txtStats = new TextBox 
        { 
            Location = new Point(20, 50), 
            Size = new Size(900, 520), 
            Multiline = true, 
            ReadOnly = true, 
            Font = new Font("Consolas", 10F),
            ScrollBars = ScrollBars.Vertical
        };

        tabStatistics.Controls.AddRange(new Control[] { lblStats, txtStats });

        ResumeLayout(false);
    }

    #endregion

    // Control declarations
    private Label lblBabyInfo;

    // Feeding
    private DateTimePicker dtpFeedingTime;
    private ComboBox cmbFeedingType;
    private NumericUpDown nudFeedingAmount;
    private ComboBox cmbFeedingUnit;
    private TextBox txtFeedingNotes;
    private Button btnAddFeeding;
    private ListBox lstFeedingLog;

    // Diaper
    private DateTimePicker dtpDiaperTime;
    private ComboBox cmbDiaperType;
    private TextBox txtDiaperNotes;
    private Button btnAddDiaper;
    private ListBox lstDiaperLog;

    // Sleep
    private DateTimePicker dtpSleepStart;
    private DateTimePicker dtpSleepEnd;
    private TextBox txtSleepNotes;
    private Button btnAddSleep;
    private ListBox lstSleepLog;

    // Growth
    private DateTimePicker dtpGrowthDate;
    private NumericUpDown nudWeight;
    private ComboBox cmbWeightUnit;
    private NumericUpDown nudHeight;
    private ComboBox cmbHeightUnit;
    private NumericUpDown nudHeadCirc;
    private ComboBox cmbHeadCircUnit;
    private TextBox txtGrowthNotes;
    private Button btnAddGrowth;
    private ListBox lstGrowthLog;

    // Baby Info
    private TextBox txtBabyName;
    private DateTimePicker dtpBabyDOB;
    private ComboBox cmbBabyGender;
    private Button btnUpdateBabyInfo;

    // Statistics
    private TextBox txtStats;
}

