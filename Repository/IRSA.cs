using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Repository
{
    public interface IRSA
    {
         public string Decrypt(string encryptedText);
          public string GetPublicKey();
    }
}