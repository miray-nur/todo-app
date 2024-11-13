using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Encrypt
{
    public class SHA256HashingService : IEncryptionService
    {
        public byte[] Hash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                return sha256.ComputeHash(bytes);
            }
        }
    }
}
