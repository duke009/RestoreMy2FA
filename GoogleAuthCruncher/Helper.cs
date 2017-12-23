using System.IO;
using System.Net;

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
}