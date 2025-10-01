using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.TesseractFolder
{
    public static class TesseractHelper
    {
        public static async Task CopyTessdataFilesAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "RecipeNest.Resources.Raw.pol.traineddata";

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) throw new Exception("Resource not found");

            var destPath = Path.Combine(FileSystem.AppDataDirectory, "tessdata");
            Directory.CreateDirectory(destPath);

            var filePath = Path.Combine(destPath, "pol.traineddata");
            using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await stream.CopyToAsync(fileStream);
        }
    }
}
