using System.Collections.Generic;
using System.Drawing;
using System.IO;
using QRCoder;

namespace GoogleAuthCruncher
{
    public class Cruncher
    {
        public IEnumerable<BitmapModel> CrunchTitaniumZip(string filePath)
        {
            using (var unarchiver = new Unarchivator())
            {
                var dbFilePath = GetDbFilePath(unarchiver.Unarchive(filePath));
                return CrunchDbFileInternal(dbFilePath);
            }
        }

        public IEnumerable<BitmapModel> CrunchDbFile(string dbFilePath)
        {
            return CrunchDbFileInternal(dbFilePath);
        }

        private string GetDbFilePath(string tempdir)
        {
            return Path.Combine(tempdir, "");
        }

        private IEnumerable<BitmapModel> CrunchDbFileInternal(string dbFilePath)
        {
            foreach (var account in GetAccounts(dbFilePath))
            {
                yield return new BitmapModel(){OriginalName = account.OriginalName, Bitmap = GetBitmap(account) };
            }
        }

        private IEnumerable<Account> GetAccounts(string dbFilePath)
        {
            var dbReader = new GoogleAuthDatabaseReader(dbFilePath);
            return dbReader.GetAccounts();
        }

        private Bitmap GetBitmap(Account account)
        {
            var otpString = Helper.BuildOtpString(account);

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(otpString, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);

            return qrCode.GetGraphic(20);
        }
    }
}