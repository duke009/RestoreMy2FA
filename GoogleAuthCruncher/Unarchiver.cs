using System;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace GoogleAuthCruncher
{
    public class Unarchiver : IDisposable
    {
        private readonly string tempDir;

        public Unarchiver()
        {
            tempDir = Helper.GetTemporaryDirectory();
        }

        public string Unarchive(string filePath)
        {
            try
            {
                ExtractTgz(filePath, tempDir);
                return tempDir;
            }
            catch(Exception ex)
            {
                throw new GoogleAuthUnarchiverException("Cannot Unarchive", ex);
            }
        }

        private void ExtractTgz(string gzArchiveName, string destFolder)
        {
            var inStream = File.OpenRead(gzArchiveName);
            var gzipStream = new GZipInputStream(inStream);

            var tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
            tarArchive.ExtractContents(destFolder);
            tarArchive.Close();

            gzipStream.Close();
            inStream.Close();
        }

        public void Dispose()
        {
            // TODO Wipe files data to prevent restoration
            DirectoryHelper.WipeAndDelete(tempDir);
        }
    }
}