# BabyFeedingLog

A Windows desktop application for monitoring and tracking infant progress. This comprehensive tool helps parents keep detailed records of their baby's daily activities, growth, and development.

## Features

### 📊 Comprehensive Tracking
- **Feeding Log**: Record feeding times, types (breast, bottle, solid), amounts, and notes
- **Diaper Changes**: Track diaper changes with type (wet, dirty, both) and timing
- **Sleep Schedule**: Monitor sleep patterns with start/end times and duration
- **Growth Measurements**: Record weight, height, and head circumference over time
- **OCR Import**: Extract text from images (receipts, medical records, notes) and populate form fields automatically

### 💾 Data Persistence
- All data is automatically saved to your local machine
- Data is stored in JSON format in your application data folder
- Your records are available every time you open the application

### 📈 Statistics & Summary
- View today's summary including total feedings, diapers, and sleep hours
- Track all-time totals for each category
- Easy-to-read statistics dashboard

### 👶 Baby Information
- Store and update your baby's basic information
- Track age automatically from date of birth
- Personalized display showing baby's name and age

## Requirements

- Windows operating system
- .NET 8.0 Runtime or later

## Installation

### Option 1: Run from Source
1. Clone this repository
2. Open a terminal in the `BabyFeedingLog` folder
3. Run: `dotnet run`

### Option 2: Build Executable
1. Clone this repository
2. Open a terminal in the `BabyFeedingLog` folder
3. Run: `dotnet publish -c Release -r win-x64 --self-contained`
4. Find the executable in `bin/Release/net8.0-windows/win-x64/publish/`

## Usage

### First Time Setup
1. Launch the application
2. Go to the "Baby Info" tab
3. Enter your baby's name, date of birth, and gender
4. Click "Update Information"

### Recording Activities

#### Feeding
1. Go to the "Feeding" tab
2. Set the time, type, and amount
3. Add any notes if needed
4. Click "Add Feeding Entry"

#### Diaper Changes
1. Go to the "Diaper" tab
2. Set the time and type
3. Add any notes if needed
4. Click "Add Diaper Entry"

#### Sleep
1. Go to the "Sleep" tab
2. Set the start and end times
3. Add any notes if needed
4. Click "Add Sleep Entry"

#### Growth Measurements
1. Go to the "Growth" tab
2. Enter weight, height, and head circumference
3. Choose appropriate units
4. Click "Add Growth Entry"

#### OCR Import (Extract text from images)
1. Go to the "OCR Import" tab
2. Click "Select Image" and choose an image with text (photo of medical records, notes, etc.)
3. Click "Extract Text" to use OCR to read the text from the image
4. Click "Parse to Feeding" or "Parse to Growth" to automatically populate form fields
5. Review the populated data in the respective tab and save

The OCR feature can recognize:
- Feeding amounts (ml, oz)
- Feeding types (breast, bottle, solid)
- Growth measurements (weight in kg/lbs, height in cm/inches, head circumference)
- Automatically switches to the appropriate tab with pre-filled data

### Viewing Statistics
Go to the "Statistics" tab to view:
- Today's activity summary
- Total counts for all categories
- Quick overview of your baby's records

## Data Storage

Data is stored in:
- Windows: `%APPDATA%\BabyFeedingLog\infantData.json`

You can backup this file to preserve your data.

## Technologies Used

- .NET 8.0
- Windows Forms
- System.Text.Json for data serialization
- Tesseract OCR for text extraction from images

## Project Structure

```
BabyFeedingLog/
├── Models/                  # Data models
│   ├── InfantInfo.cs       # Main infant information
│   ├── FeedingEntry.cs     # Feeding record
│   ├── DiaperEntry.cs      # Diaper change record
│   ├── SleepEntry.cs       # Sleep session record
│   └── GrowthEntry.cs      # Growth measurement record
├── Services/               # Business logic
│   ├── DataManager.cs      # Data persistence service
│   └── OcrService.cs       # OCR text extraction service
├── tessdata/               # Tesseract language data
│   └── eng.traineddata     # English language training data
├── Form1.cs               # Main form logic
├── Form1.Designer.cs      # UI design
├── Program.cs             # Application entry point
└── BabyFeedingLog.csproj  # Project configuration
```

## License

This project is open source and available for personal use.

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests.
