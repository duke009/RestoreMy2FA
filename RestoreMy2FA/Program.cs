

namespace RestoreMy2FA
{
    class Program
    {
        static void Main(string[] args)
        {
            var cruncher = new GoogleAuthCruncher.GoogleAuthCruncher();
            var crunched = cruncher.CrunchDbFile("databases");

            var i = 0;
            foreach (var bitmap in crunched)
            {
                bitmap.Save($"{++i}.bmp");
            }
        }
    }
}
