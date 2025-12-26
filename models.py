"""
Baby Feeding and Progress Monitoring Application
Database models for tracking infant data
"""
from datetime import datetime
from flask_sqlalchemy import SQLAlchemy

db = SQLAlchemy()


class Baby(db.Model):
    """Model for storing baby information"""
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(100), nullable=False)
    date_of_birth = db.Column(db.Date, nullable=False)
    gender = db.Column(db.String(10))
    created_at = db.Column(db.DateTime, default=datetime.utcnow)
    
    # Relationships
    feedings = db.relationship('Feeding', backref='baby', lazy=True, cascade='all, delete-orphan')
    sleep_logs = db.relationship('SleepLog', backref='baby', lazy=True, cascade='all, delete-orphan')
    diaper_changes = db.relationship('DiaperChange', backref='baby', lazy=True, cascade='all, delete-orphan')
    growth_records = db.relationship('GrowthRecord', backref='baby', lazy=True, cascade='all, delete-orphan')
    milestones = db.relationship('Milestone', backref='baby', lazy=True, cascade='all, delete-orphan')


class Feeding(db.Model):
    """Model for tracking feeding sessions"""
    id = db.Column(db.Integer, primary_key=True)
    baby_id = db.Column(db.Integer, db.ForeignKey('baby.id'), nullable=False)
    timestamp = db.Column(db.DateTime, nullable=False, default=datetime.utcnow)
    feeding_type = db.Column(db.String(20), nullable=False)  # breast, bottle, solid
    amount = db.Column(db.Float)  # in ml or oz
    duration = db.Column(db.Integer)  # in minutes
    notes = db.Column(db.Text)


class SleepLog(db.Model):
    """Model for tracking sleep sessions"""
    id = db.Column(db.Integer, primary_key=True)
    baby_id = db.Column(db.Integer, db.ForeignKey('baby.id'), nullable=False)
    start_time = db.Column(db.DateTime, nullable=False)
    end_time = db.Column(db.DateTime)
    duration = db.Column(db.Integer)  # in minutes
    notes = db.Column(db.Text)


class DiaperChange(db.Model):
    """Model for tracking diaper changes"""
    id = db.Column(db.Integer, primary_key=True)
    baby_id = db.Column(db.Integer, db.ForeignKey('baby.id'), nullable=False)
    timestamp = db.Column(db.DateTime, nullable=False, default=datetime.utcnow)
    change_type = db.Column(db.String(20), nullable=False)  # wet, dirty, both
    notes = db.Column(db.Text)


class GrowthRecord(db.Model):
    """Model for tracking baby growth metrics"""
    id = db.Column(db.Integer, primary_key=True)
    baby_id = db.Column(db.Integer, db.ForeignKey('baby.id'), nullable=False)
    date = db.Column(db.Date, nullable=False)
    weight = db.Column(db.Float)  # in kg
    height = db.Column(db.Float)  # in cm
    head_circumference = db.Column(db.Float)  # in cm
    notes = db.Column(db.Text)


class Milestone(db.Model):
    """Model for tracking developmental milestones"""
    id = db.Column(db.Integer, primary_key=True)
    baby_id = db.Column(db.Integer, db.ForeignKey('baby.id'), nullable=False)
    date = db.Column(db.Date, nullable=False)
    milestone_type = db.Column(db.String(50), nullable=False)  # motor, cognitive, social, language
    description = db.Column(db.Text, nullable=False)
    achieved = db.Column(db.Boolean, default=True)
