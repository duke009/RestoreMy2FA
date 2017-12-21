using System.IO;
using System.Linq;

namespace RestoreMy2FA
{
    class Program
    {
        static void Main(string[] args)
        {
            // otpauth://totp/Google%3Amyemail%40gmail.com?secret=7gmdmzctmhpm7i6nrmbom6u5gny7o6la&issuer=Google
            var cruncher = new GoogleAuthCruncher.GoogleAuthCruncher();
            var crunched = cruncher.CrunchDbFile("databases");

            foreach (var bitmapModel in crunched)
            {
                var fileName = Path.GetInvalidFileNameChars().Aggregate(bitmapModel.OriginalName, (current, badChar) => current.Replace(badChar.ToString(), "."));
                bitmapModel.Bitmap.Save($"{fileName}.bmp");
            }
        }
    }
}
