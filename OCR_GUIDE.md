# OCR Feature Usage Guide

## New OCR Import Tab

The Baby Feeding Log application now includes an **OCR Import** tab that allows you to extract text from images and automatically populate form fields.

### How to Use:

1. **Select an Image**
   - Click the "Select Image" button
   - Choose an image file (JPG, PNG, BMP, GIF, or TIFF)
   - The image will display in the preview area

2. **Extract Text**
   - Click the "Extract Text" button
   - The OCR engine (Tesseract) will analyze the image
   - Extracted text appears in the text area on the right

3. **Parse to Forms**
   - Click "Parse to Feeding" to auto-populate feeding form fields
   - Click "Parse to Growth" to auto-populate growth measurement fields
   - The app will automatically switch to the appropriate tab with pre-filled data

### What the OCR Can Recognize:

#### Feeding Data:
- **Amounts**: "120 ml", "4 oz"
- **Types**: "breast", "bottle", "solid", "food"
- Automatically selects the correct unit and type

#### Growth Data:
- **Weight**: "5.5 kg", "12 lbs", "12 pounds"
- **Height**: "55 cm", "22 inches", "height 60 cm", "length 55 cm"
- **Head Circumference**: "head 38 cm", "head 15 inches"

### Example Use Cases:

1. **Medical Records**: Take a photo of doctor visit notes and extract measurements
2. **Handwritten Logs**: Photograph your handwritten feeding logs
3. **Text Messages**: Screenshot text messages with feeding information
4. **Digital Documents**: Import data from PDFs or images of documents

### UI Layout:

```
┌─────────────────────────────────────────────────────────────────────┐
│ OCR Import Tab                                                       │
├─────────────────────────────────────────────────────────────────────┤
│                                                                      │
│ [Select Image] [Extract Text] [Parse to Feeding] [Parse to Growth] │
│                                                                      │
│ Image Preview:          │  Extracted Text:                          │
│ ┌─────────────────────┐ │  ┌──────────────────────────────────────┐│
│ │                     │ │  │ Baby Feeding Record                  ││
│ │                     │ │  │                                       ││
│ │   [Image shown      │ │  │ Date: December 27, 2024              ││
│ │    here]            │ │  │ Time: 10:30 AM                       ││
│ │                     │ │  │                                       ││
│ │                     │ │  │ Type: Bottle                          ││
│ │                     │ │  │ Amount: 120 ml                        ││
│ └─────────────────────┘ │  │                                       ││
│                          │  └──────────────────────────────────────┘│
│                                                                      │
│ Status: Text extracted successfully! (123 characters)               │
└─────────────────────────────────────────────────────────────────────┘
```

### Tips for Best Results:

1. **Image Quality**: Use clear, well-lit images with good contrast
2. **Text Size**: Ensure text is large enough to be readable
3. **Language**: Currently supports English text recognition
4. **Review Data**: Always review auto-populated data before saving
5. **Notes Preserved**: Original extracted text is saved in the notes field
6. **High Resolution**: Higher resolution images (300 DPI or more) work best
7. **Straight Text**: Avoid rotated or tilted text when possible
8. **Clean Background**: Text on plain backgrounds is easier to read

### Accuracy Improvements:

The OCR system now includes:
- **Automatic Image Preprocessing**: Converts images to grayscale and increases contrast
- **Dual-Pass Extraction**: Tries with preprocessing first, then without if needed
- **Confidence Scoring**: Shows OCR confidence percentage (green = high, orange = medium, red = low)
- **Optimized Settings**: Configured for medical records and feeding logs
- **Character Whitelist**: Focuses on alphanumeric characters and common punctuation

### Technical Details:

- **OCR Engine**: Tesseract 5.2.0
- **Supported Languages**: English (eng.traineddata)
- **Pattern Matching**: Smart regex-based parsing for common units
- **Performance**: Compiled regex patterns for fast processing
- **Memory Management**: Proper image disposal to prevent memory leaks
- **Image Processing**: Grayscale conversion and contrast enhancement

### Troubleshooting:

- **"No text detected"**: Try improving image quality or contrast, ensure text is clearly visible
- **Low confidence scores**: Use higher resolution images with better lighting
- **Incorrect parsing**: Review and manually adjust the populated fields
- **Missing tessdata error**: Ensure tessdata/eng.traineddata file exists in the application directory
- **Inaccurate results**: Try taking a new photo with better lighting and focus

