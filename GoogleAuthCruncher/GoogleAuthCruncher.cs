using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using QRCoder;

namespace GoogleAuthCruncher
{
    public class Cruncher
    {
        // TODO To config!
        private const string DbPathInArchive = "data\\data\\com.google.android.apps.authenticator2\\databases\\databases";
        public List<BitmapModel> CrunchTitaniumZip(string filePath)
        {
            using (var unarchiver = new Unarchivator())
            {
                var dbFilePath = Path.Combine(unarchiver.Unarchive(filePath), DbPathInArchive);
                return CrunchDbFileInternal(dbFilePath).ToList();
            }
        }

        public IEnumerable<BitmapModel> CrunchDbFile(string dbFilePath)
        {
            return CrunchDbFileInternal(dbFilePath);
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