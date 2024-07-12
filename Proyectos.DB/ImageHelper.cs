using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

public class ImageHelper
{
    public static byte[] GetPlaceholderImage()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using (var stream = assembly.GetManifestResourceStream("udp-logo.png"))
        {
            if (stream == null) return null;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}