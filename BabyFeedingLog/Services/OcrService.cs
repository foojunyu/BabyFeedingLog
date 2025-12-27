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
        
        // If not found, try the project directory
        if (!Directory.Exists(_tessDataPath))
        {
            string projectPath = Path.GetFullPath(Path.Combine(appPath, @"..\..\..\"));
            _tessDataPath = Path.Combine(projectPath, "tessdata");
        }
    }

    public string ExtractTextFromImage(string imagePath)
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

            using var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default);
            using var img = Pix.LoadFromFile(imagePath);
            using var page = engine.Process(img);
            
            string text = page.GetText();
            return text?.Trim() ?? string.Empty;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error extracting text from image: {ex.Message}", ex);
        }
    }

    public string ExtractTextFromImage(Image image)
    {
        try
        {
            // Save image to temporary file
            string tempFile = Path.Combine(Path.GetTempPath(), $"ocr_temp_{Guid.NewGuid()}.png");
            image.Save(tempFile, System.Drawing.Imaging.ImageFormat.Png);

            try
            {
                return ExtractTextFromImage(tempFile);
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
}
