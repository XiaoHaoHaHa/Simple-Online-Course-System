using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace CoreLib
{
	public static class Sha256Hash
	{
        public static string PwdSHA256(string pwdStr, string salt)
        {
            var result = string.Empty;
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                //將字串編碼轉成 UTF8 bytes[]
                var bytes = Encoding.UTF8.GetBytes(pwdStr + salt.ToUpper());
                //取得雜湊值bytes[]
                var hash = sha256.ComputeHash(bytes);
                //byte[] to base64 string
                result = Convert.ToBase64String(hash);
            }
            return result;
        }
    }
}
