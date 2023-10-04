using System.Text.RegularExpressions;

namespace Irtl.Bff.Storage;

public static class KnownStoragePaths
{
    public const string FavIcons = "favicon";

    public static string GetFavIconFileNameWithoutExtension(Uri favIconUri)
    {
        var key = RemoveInvalidFilePathCharacters(favIconUri.Host, "_");

        if (!TryGetFileName(favIconUri, out string fileName))
        {
            return null;
        }
        
        return Path.Combine(FavIcons, key + "");
    }
    
    static bool TryGetFileName(Uri uri, out string? fileName)
    {
        fileName = default;

        if (uri.Segments.Length <= 0)
            return false;

        // If no segments are available, or the last segment is a directory, return an empty string or null
        string lastSegment = uri.Segments[^1]; // Last segment of the URI path

        if (lastSegment.EndsWith('/'))
            return false;

        var ext = Path.GetFileNameWithoutExtension(lastSegment);

        if (string.IsNullOrEmpty(ext))
        {
            return false;
        }

        fileName = lastSegment;
        return true;
    }
    
    public static string RemoveInvalidFilePathCharacters(string filename, string replaceChar)
    {
        string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
        var r = new Regex($"[{Regex.Escape(regexSearch)}]", RegexOptions.Compiled);
        return r.Replace(filename, replaceChar);
    }
}