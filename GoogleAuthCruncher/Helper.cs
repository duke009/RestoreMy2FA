using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace GoogleAuthCruncher
{
    internal class Helper
    {
        public static string BuildOtpString(Account account)
        {
            return $"otpauth://totp/{WebUtility.HtmlEncode(account.Email)}?secret={account.Secret}&issuer={account.Issuer}";
        }

        public static string GetTemporaryDirectory()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
    public class DirectoryHelper
    {
        public static void WipeAndDelete(string directoryPath)
        {
            WipeData(directoryPath);
            Directory.Delete(directoryPath, true);
        }

        private static void WipeData(string dir)
        {
            foreach (var subDir in Directory.GetDirectories(dir))
            {
                WipeData(subDir);
            }

            foreach (var file in Directory.GetFiles(dir))
            {
                WipeFile(file, 3);
            }
        }

        //https://www.codeproject.com/Articles/22736/Securely-Delete-a-File-using-NET
        private static void WipeFile(string filename, int timesToWrite)
        {
            try
            {
                if (File.Exists(filename))
                {
                    // Set the files attributes to normal in case it's read-only.

                    File.SetAttributes(filename, FileAttributes.Normal);

                    // Calculate the total number of sectors in the file.
                    double sectors = Math.Ceiling(new FileInfo(filename).Length / 512.0);

                    // Create a dummy-buffer the size of a sector.

                    byte[] dummyBuffer = new byte[512];

                    // Create a cryptographic Random Number Generator.
                    // This is what I use to create the garbage data.

                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                    // Open a FileStream to the file.
                    FileStream inputStream = new FileStream(filename, FileMode.Open);
                    for (int currentPass = 0; currentPass < timesToWrite; currentPass++)
                    {
                        // Go to the beginning of the stream

                        inputStream.Position = 0;

                        // Loop all sectors
                        for (int sectorsWritten = 0; sectorsWritten < sectors; sectorsWritten++)
                        {
                            // Fill the dummy-buffer with random data
                            rng.GetBytes(dummyBuffer);

                            // Write it to the stream
                            inputStream.Write(dummyBuffer, 0, dummyBuffer.Length);
                        }
                    }

                    // Truncate the file to 0 bytes.
                    // This will hide the original file-length if you try to recover the file.

                    inputStream.SetLength(0);

                    // Close the stream.
                    inputStream.Close();

                    // As an extra precaution I change the dates of the file so the
                    // original dates are hidden if you try to recover the file.

                    DateTime dt = new DateTime(2037, 1, 1, 0, 0, 0);
                    File.SetCreationTime(filename, dt);
                    File.SetLastAccessTime(filename, dt);
                    File.SetLastWriteTime(filename, dt);
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}