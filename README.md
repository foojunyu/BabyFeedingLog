# 🍼 Baby Feeding & Progress Log

A comprehensive web application for monitoring and tracking your baby's daily activities, growth, and developmental milestones.

## Features

- **👶 Baby Profiles**: Manage multiple baby profiles with birth dates and basic information
- **🍼 Feeding Logs**: Track feeding sessions including:
  - Breast feeding
  - Bottle feeding
  - Solid food introduction
  - Amount and duration tracking
- **😴 Sleep Tracking**: Monitor sleep patterns with start/end times and duration
- **🧷 Diaper Changes**: Log diaper changes (wet, dirty, or both)
- **📏 Growth Records**: Track physical development:
  - Weight (kg)
  - Height (cm)
  - Head circumference (cm)
- **🎯 Developmental Milestones**: Record important achievements:
  - Motor skills
  - Cognitive development
  - Social/emotional milestones
  - Language and communication

## Installation

### Prerequisites

- Python 3.7 or higher
- pip (Python package manager)

### Setup Instructions

1. Clone the repository:
```bash
git clone https://github.com/foojunyu/BabyFeedingLog.git
cd BabyFeedingLog
```

2. Create a virtual environment (recommended):
```bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
```

3. Install dependencies:
```bash
pip install -r requirements.txt
```

4. Run the application:
```bash
python app.py
```

5. Open your web browser and navigate to:
```
http://localhost:5000
```

## Usage

### Getting Started

1. **Add Your Baby's Profile**
   - Click "Add New Baby" on the home page
   - Enter your baby's name, date of birth, and gender (optional)
   - Click "Add Baby" to create the profile

2. **Access Baby Dashboard**
   - Click on your baby's card from the home page
   - The dashboard shows all recent activities and statistics

3. **Log Daily Activities**
   - Use the buttons on the dashboard to log:
     - Feeding sessions
     - Sleep times
     - Diaper changes
     - Growth measurements
     - Developmental milestones

### Features Overview

#### Feeding Logs
- Record each feeding with timestamp
- Choose type: breast, bottle, or solid
- Track amount (ml) and duration (minutes)
- Add notes for any observations

#### Sleep Tracking
- Log start and end times
- Automatic duration calculation
- Track sleep patterns over time

#### Diaper Changes
- Quick logging with timestamp
- Categorize as wet, dirty, or both
- Add notes for unusual observations

#### Growth Records
- Regular measurements for health tracking
- Weight, height, and head circumference
- View growth trends over time

#### Milestones
- Document important achievements
- Categorize by development area
- Create a timeline of your baby's progress

## Database

The application uses SQLite database (`baby_log.db`) which is created automatically on first run. All data is stored locally on your machine.

## Technology Stack

- **Backend**: Python Flask
- **Database**: SQLite with Flask-SQLAlchemy
- **Frontend**: HTML5, CSS3, JavaScript
- **Design**: Responsive web design with gradient UI

## File Structure

```
BabyFeedingLog/
├── app.py                 # Main Flask application
├── models.py              # Database models
├── requirements.txt       # Python dependencies
├── README.md             # This file
├── templates/            # HTML templates
│   ├── base.html
│   ├── index.html
│   ├── add_baby.html
│   ├── dashboard.html
│   ├── add_feeding.html
│   ├── add_sleep.html
│   ├── add_diaper.html
│   ├── add_growth.html
│   ├── add_milestone.html
│   └── list_feedings.html
└── baby_log.db           # SQLite database (created on first run)
```

## Future Enhancements

Potential features for future development:
- Data export (CSV, PDF reports)
- Charts and graphs for visualizing trends
- Mobile app version
- Multi-user support with authentication
- Photo uploads for milestones
- Vaccination tracking
- Doctor's appointment reminders
- Feeding and sleep schedule recommendations

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is open source and available under the MIT License.

## Support

For issues, questions, or suggestions, please open an issue on GitHub.

---

**Happy tracking! 👶💙**