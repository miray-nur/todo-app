using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Encrypt
{
    public class MD5HashingService : IEncryptionService
    {
        public byte[] Hash(string text)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                return md5.ComputeHash(bytes);
            }
        }
    }
}
