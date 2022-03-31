using LTHL.VIEW_MODELS.Common;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace LTHL.COMMON.Helpers
{
    public static class globals
    {
        #region Encryption Decryption
        //public static string Encrypt(string plainText)
        //{
        //    if (plainText == null) throw new ArgumentNullException("plainText");

        //    //encrypt data
        //    var data = Encoding.Unicode.GetBytes(plainText);
        //    byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);

        //    //return as base64 string
        //    return Convert.ToBase64String(encrypted);
        //}

        //public static string Decrypt(string cipher)
        //{
        //    try
        //    {
        //        if (cipher == null) throw new ArgumentNullException("cipher");

        //        //parse base64 string
        //        byte[] data = Convert.FromBase64String(cipher);

        //        //decrypt data
        //        byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
        //        return Encoding.Unicode.GetString(decrypted);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        #region Compute Hash
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GetMD5BytesHash(string password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = new MD5CryptoServiceProvider().ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        #endregion

        #region Hash with Salt

        public static string ComputeHashWithSalt(string password, string salt)
        {
            // Create a Hash With Salt   
            var hashedPassword = ComputeHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt));
            return hashedPassword;
        }
        public static string ComputeHash(byte[] bytesToHash, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }
        public static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        #endregion



        #region Encryption-Decryption
        public static string Encrypt(string clearText, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText, string EncryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion

        #region JWT-TOKEN
        public static string GetToken(AuthToken claimData, int validityTimeinMinute, AppSettings appSettings)
        {
            string token = null;
            try
            {
                var handeler = new JwtSecurityTokenHandler();
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(claimData.UserId.ToString(), "Token"),
                    new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier,claimData.UserId.ToString()),
                    new Claim(ClaimTypes.Name,claimData.UserName.ToString()),
                    new Claim("token" , appSettings.SecretKey),
                    });
                var keyByteArray = Encoding.ASCII.GetBytes(appSettings.SecretKey);
                var signinKey = new SymmetricSecurityKey(keyByteArray);
                var securityToken = handeler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Issuer = "Issuer",
                    Audience = "Audience",
                    SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
                    Subject = identity,
                    Expires = DateTime.Now.Add(TimeSpan.FromMinutes(validityTimeinMinute)),
                    NotBefore = DateTime.Now
                });
                return handeler.WriteToken(securityToken);
            }
            catch (Exception)
            {
                throw;
            }
            return token;
        }

        public static AuthToken ReadJWT_Token(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var jti = tokenS.Claims.First(claim => claim.Type == "token").Value;
                //var authtoken = JsonConvert.DeserializeObject<AuthToken>(jti);
                var authtoken = new AuthToken
                {
                    UserId = Convert.ToInt32(tokenS.Claims.FirstOrDefault(x => x.Type.Equals("nameid"))?.Value),
                    DeviceToken = tokenS.Claims.FirstOrDefault(x => x.Type.Equals("token"))?.Value
                };
                return authtoken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

    }
}
