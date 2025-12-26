"""
Baby Feeding and Progress Monitoring Application
Main Flask application
"""
from flask import Flask, render_template, request, jsonify, redirect, url_for
from datetime import datetime, date
from models import db, Baby, Feeding, SleepLog, DiaperChange, GrowthRecord, Milestone
import os

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///baby_log.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
# Use environment variable for SECRET_KEY in production, fallback to generated key for development
app.config['SECRET_KEY'] = os.environ.get('SECRET_KEY') or os.urandom(24).hex()

db.init_app(app)


def init_db():
    """Initialize the database"""
    with app.app_context():
        db.create_all()


# Home page
@app.route('/')
def index():
    """Home page showing list of babies"""
    babies = Baby.query.all()
    return render_template('index.html', babies=babies)


# Baby management
@app.route('/baby/add', methods=['GET', 'POST'])
def add_baby():
    """Add a new baby"""
    if request.method == 'POST':
        data = request.form
        dob = datetime.strptime(data['date_of_birth'], '%Y-%m-%d').date()
        baby = Baby(
            name=data['name'],
            date_of_birth=dob,
            gender=data.get('gender', '')
        )
        db.session.add(baby)
        db.session.commit()
        return redirect(url_for('baby_dashboard', baby_id=baby.id))
    return render_template('add_baby.html')


@app.route('/baby/<int:baby_id>')
def baby_dashboard(baby_id):
    """Dashboard for a specific baby"""
    baby = Baby.query.get_or_404(baby_id)
    
    # Get recent activities
    recent_feedings = Feeding.query.filter_by(baby_id=baby_id).order_by(Feeding.timestamp.desc()).limit(5).all()
    recent_sleep = SleepLog.query.filter_by(baby_id=baby_id).order_by(SleepLog.start_time.desc()).limit(5).all()
    recent_diapers = DiaperChange.query.filter_by(baby_id=baby_id).order_by(DiaperChange.timestamp.desc()).limit(5).all()
    growth_records = GrowthRecord.query.filter_by(baby_id=baby_id).order_by(GrowthRecord.date.desc()).all()
    milestones = Milestone.query.filter_by(baby_id=baby_id).order_by(Milestone.date.desc()).all()
    
    # Calculate age
    today = date.today()
    age_days = (today - baby.date_of_birth).days
    age_months = age_days // 30
    age_display = f"{age_months} months" if age_months > 0 else f"{age_days} days"
    
    return render_template('dashboard.html', 
                         baby=baby, 
                         age=age_display,
                         recent_feedings=recent_feedings,
                         recent_sleep=recent_sleep,
                         recent_diapers=recent_diapers,
                         growth_records=growth_records,
                         milestones=milestones)


# Feeding routes
@app.route('/baby/<int:baby_id>/feeding/add', methods=['GET', 'POST'])
def add_feeding(baby_id):
    """Add a feeding record"""
    baby = Baby.query.get_or_404(baby_id)
    if request.method == 'POST':
        data = request.form
        timestamp = datetime.strptime(data['timestamp'], '%Y-%m-%dT%H:%M')
        feeding = Feeding(
            baby_id=baby_id,
            timestamp=timestamp,
            feeding_type=data['feeding_type'],
            amount=float(data['amount']) if data.get('amount') else None,
            duration=int(data['duration']) if data.get('duration') else None,
            notes=data.get('notes', '')
        )
        db.session.add(feeding)
        db.session.commit()
        return redirect(url_for('baby_dashboard', baby_id=baby_id))
    return render_template('add_feeding.html', baby=baby)


@app.route('/baby/<int:baby_id>/feeding/list')
def list_feedings(baby_id):
    """List all feeding records"""
    baby = Baby.query.get_or_404(baby_id)
    feedings = Feeding.query.filter_by(baby_id=baby_id).order_by(Feeding.timestamp.desc()).all()
    return render_template('list_feedings.html', baby=baby, feedings=feedings)


# Sleep routes
@app.route('/baby/<int:baby_id>/sleep/add', methods=['GET', 'POST'])
def add_sleep(baby_id):
    """Add a sleep log"""
    baby = Baby.query.get_or_404(baby_id)
    if request.method == 'POST':
        data = request.form
        start_time = datetime.strptime(data['start_time'], '%Y-%m-%dT%H:%M')
        end_time = datetime.strptime(data['end_time'], '%Y-%m-%dT%H:%M') if data.get('end_time') else None
        duration = None
        if end_time:
            duration = int((end_time - start_time).total_seconds() / 60)
        
        sleep_log = SleepLog(
            baby_id=baby_id,
            start_time=start_time,
            end_time=end_time,
            duration=duration,
            notes=data.get('notes', '')
        )
        db.session.add(sleep_log)
        db.session.commit()
        return redirect(url_for('baby_dashboard', baby_id=baby_id))
    return render_template('add_sleep.html', baby=baby)


# Diaper routes
@app.route('/baby/<int:baby_id>/diaper/add', methods=['GET', 'POST'])
def add_diaper(baby_id):
    """Add a diaper change record"""
    baby = Baby.query.get_or_404(baby_id)
    if request.method == 'POST':
        data = request.form
        timestamp = datetime.strptime(data['timestamp'], '%Y-%m-%dT%H:%M')
        diaper = DiaperChange(
            baby_id=baby_id,
            timestamp=timestamp,
            change_type=data['change_type'],
            notes=data.get('notes', '')
        )
        db.session.add(diaper)
        db.session.commit()
        return redirect(url_for('baby_dashboard', baby_id=baby_id))
    return render_template('add_diaper.html', baby=baby)


# Growth routes
@app.route('/baby/<int:baby_id>/growth/add', methods=['GET', 'POST'])
def add_growth(baby_id):
    """Add a growth record"""
    baby = Baby.query.get_or_404(baby_id)
    if request.method == 'POST':
        data = request.form
        record_date = datetime.strptime(data['date'], '%Y-%m-%d').date()
        growth = GrowthRecord(
            baby_id=baby_id,
            date=record_date,
            weight=float(data['weight']) if data.get('weight') else None,
            height=float(data['height']) if data.get('height') else None,
            head_circumference=float(data['head_circumference']) if data.get('head_circumference') else None,
            notes=data.get('notes', '')
        )
        db.session.add(growth)
        db.session.commit()
        return redirect(url_for('baby_dashboard', baby_id=baby_id))
    return render_template('add_growth.html', baby=baby)


# Milestone routes
@app.route('/baby/<int:baby_id>/milestone/add', methods=['GET', 'POST'])
def add_milestone(baby_id):
    """Add a milestone"""
    baby = Baby.query.get_or_404(baby_id)
    if request.method == 'POST':
        data = request.form
        milestone_date = datetime.strptime(data['date'], '%Y-%m-%d').date()
        milestone = Milestone(
            baby_id=baby_id,
            date=milestone_date,
            milestone_type=data['milestone_type'],
            description=data['description'],
            achieved=True
        )
        db.session.add(milestone)
        db.session.commit()
        return redirect(url_for('baby_dashboard', baby_id=baby_id))
    return render_template('add_milestone.html', baby=baby)


if __name__ == '__main__':
    init_db()
    # Debug mode is disabled for security - use environment variable to enable if needed
    # Set FLASK_DEBUG=1 in environment for development debugging
    debug_mode = os.environ.get('FLASK_DEBUG', '0') == '1'
    app.run(debug=debug_mode, host='0.0.0.0', port=5000)
