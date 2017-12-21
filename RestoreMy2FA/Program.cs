namespace RestoreMy2FA
{
    class Program
    {
        static void Main(string[] args)
        {
            // otpauth://totp/Google%3Amyemail%40gmail.com?secret=7gmdmzctmhpm7i6nrmbom6u5gny7o6la&issuer=Google
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
