using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace passwordOrganizer
{
  
    public class Encryption
    {
        private static readonly string  _AllChars = "ABCDEFGHIGKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz1234567890";
        private static readonly string _AltChars =  "KICDGTHRGNXLABQMOSUYPVZEWFknqfytwxcolsigevmghapdbuzr9246017583";
          public static string Encrypt(string password) {
            var sb = new StringBuilder();
            foreach (char c in password) {
                var charIdx  = _AllChars.IndexOf(c);
                sb.Append(_AltChars[charIdx]);
            }
            return sb.ToString();
        
        }

        public static string Decrypt(string password) {
            var sb = new StringBuilder();
            foreach (char c in password)
            {
                var charIdx = _AltChars.IndexOf(c);
                sb.Append(_AllChars[charIdx]);
            }
            return sb.ToString();
        }

    }
}
