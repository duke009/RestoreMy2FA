using System.Net;

namespace GoogleAuthCruncher
{
    public class Helper
    {
        public static string BuildOtpString(Account account)
        {
            return $"otpauth://totp/{WebUtility.HtmlEncode(account.Email)}?secret={account.Secret}&issuer={account.Issuer}";
        }
    }
}