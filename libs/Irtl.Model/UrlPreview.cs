namespace Irtl.Bff.WebPreview.Model;

public class UrlPreview
{
    public string Title { get; set; }

    public string? Summary { get; set; }

    /// <summary>
    /// Relative path to the image in the backing storage
    /// </summary>
    public string? ImagePath { get; set; }
    
    /// <summary>
    /// Relative path to the icon for the website in the backing storage
    /// </summary>
    public string? IconPath { get; set; }
}