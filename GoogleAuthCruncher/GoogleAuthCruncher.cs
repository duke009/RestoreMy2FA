using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using QRCoder;

namespace GoogleAuthCruncher
{
    public class GoogleAuthCruncher
    {
        public IEnumerable<Bitmap> CrunchTitaniumZip(string fileName)
        {
            var dbFilePath = UnArchive(fileName);
            var otpStrings = CrunchDbFileInternal(dbFilePath);
            return GetBitmaps(otpStrings);
        }

        public IEnumerable<Bitmap> CrunchDbFile(string dbFilePath)
        {
            var otpStrings = CrunchDbFileInternal(dbFilePath);
            return GetBitmaps(otpStrings);
        }

        private IEnumerable<string> CrunchDbFileInternal(string dbFilePath)
        {
            var dbReader = new GoogleAuthDatabaseReader(dbFilePath);
            return GetStrings(dbReader.selectQuery());
        }

        private IEnumerable<Bitmap> GetBitmaps(IEnumerable<string> otpStrings)
        {
            var qrGenerator = new QRCodeGenerator();

            foreach (var otpString in otpStrings)
            {
                var qrCodeData = qrGenerator.CreateQrCode(otpString, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                yield return qrCode.GetGraphic(20);
            }
        }

        private IEnumerable<string> GetStrings(IEnumerable<Account> accounts)
        {
            return accounts.Select(Helper.BuildOtpString);
        }

        private string UnArchive(object filename)
        {
            throw new System.NotImplementedException();
        }
    }
}