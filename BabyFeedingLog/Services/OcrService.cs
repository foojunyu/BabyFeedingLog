using Tesseract;

namespace BabyFeedingLog.Services;

public class OcrService
{
    private readonly string _tessDataPath;

    public OcrService()
    {
        // Get the tessdata path relative to the application
        string appPath = AppDomain.CurrentDomain.BaseDirectory;
        _tessDataPath = Path.Combine(appPath, "tessdata");
        
        // If not found, search in parent directories (for development/debug scenarios)
        if (!Directory.Exists(_tessDataPath))
        {
            var currentDir = new DirectoryInfo(appPath);
            while (currentDir != null && currentDir.Parent != null)
            {
                var testPath = Path.Combine(currentDir.FullName, "tessdata");
                if (Directory.Exists(testPath))
                {
                    _tessDataPath = testPath;
                    break;
                }
                currentDir = currentDir.Parent;
            }
        }
    }

    public string ExtractTextFromImage(string imagePath, bool preprocessImage = true)
    {
        try
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image file not found", imagePath);
            }

            if (!Directory.Exists(_tessDataPath))
            {
                throw new DirectoryNotFoundException($"Tesseract data directory not found at: {_tessDataPath}");
            }

            // Load and optionally preprocess the image for better OCR accuracy
            using var originalImage = Image.FromFile(imagePath);
            using var processedImage = preprocessImage ? PreprocessImage(originalImage) : new Bitmap(originalImage);
            
            // Save processed image to temp file for Tesseract
            string tempFile = Path.Combine(Path.GetTempPath(), $"ocr_processed_{Guid.NewGuid()}.png");
            processedImage.Save(tempFile, System.Drawing.Imaging.ImageFormat.Png);
            
            try
            {
                using var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default);
                
                // Configure engine for better accuracy
                engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,:-/ ");
                engine.SetVariable("tessedit_pageseg_mode", "6"); // Assume uniform block of text
                
                using var img = Pix.LoadFromFile(tempFile);
                using var page = engine.Process(img);
                
                string text = page.GetText();
                float confidence = page.GetMeanConfidence();
                
                // Log confidence for debugging
                System.Diagnostics.Debug.WriteLine($"OCR Confidence: {confidence * 100:F2}%");
                
                return text?.Trim() ?? string.Empty;
            }
            finally
            {
                // Clean up temp file
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error extracting text from image: {ex.Message}", ex);
        }
    }

    public string ExtractTextFromImage(Image image, bool preprocessImage = true)
    {
        try
        {
            // Save image to temporary file with optional preprocessing
            using var processedImage = preprocessImage ? PreprocessImage(image) : new Bitmap(image);
            string tempFile = Path.Combine(Path.GetTempPath(), $"ocr_temp_{Guid.NewGuid()}.png");
            processedImage.Save(tempFile, System.Drawing.Imaging.ImageFormat.Png);

            try
            {
                return ExtractTextFromImage(tempFile, false); // Already preprocessed
            }
            finally
            {
                // Clean up temp file
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error extracting text from image: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Preprocesses the image to improve OCR accuracy
    /// </summary>
    private Bitmap PreprocessImage(Image originalImage)
    {
        // Create a new bitmap with the same dimensions
        var bitmap = new Bitmap(originalImage.Width, originalImage.Height);
        
        using (var g = Graphics.FromImage(bitmap))
        {
            // Use high quality rendering
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            
            g.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
        }
        
        // Convert to grayscale and increase contrast
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                var pixel = bitmap.GetPixel(x, y);
                
                // Convert to grayscale
                int gray = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                
                // Increase contrast using threshold
                gray = gray > 128 ? 255 : 0;
                
                var newColor = Color.FromArgb(gray, gray, gray);
                bitmap.SetPixel(x, y, newColor);
            }
        }
        
        return bitmap;
    }

    /// <summary>
    /// Gets the confidence level of the last OCR operation (0-1)
    /// </summary>
    public float GetLastConfidence(string imagePath)
    {
        try
        {
            if (!File.Exists(imagePath) || !Directory.Exists(_tessDataPath))
            {
                return 0f;
            }

            using var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default);
            using var img = Pix.LoadFromFile(imagePath);
            using var page = engine.Process(img);
            
            return page.GetMeanConfidence();
        }
        catch
        {
            return 0f;
        }
    }
}
