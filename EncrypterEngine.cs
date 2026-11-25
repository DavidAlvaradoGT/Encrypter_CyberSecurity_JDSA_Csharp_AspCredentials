using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encrypter
{
    public static class EncrypterEngine
    {
        public static readonly Dictionary<char, string> EncryptMap = new Dictionary<char, string>
        {
            // Characters to Enctrypt

            {'A', "F54EW45X" },
            {'B', "E7HVM5H6" },
            {'C', "521GDSL4" },
            {'D', "574FDER5" },
            {'E', "MBND57D9" },
            {'F', "OPTRE4B5" },
            {'G', "589F6SA2" },
            {'H', "GFKRI586" },
            {'I', "41E5DTDF" },
            {'J', "53F1D24G" },
            {'K', "T4F2344F" },
            {'L', "FAS4TFDY" },
            {'M', "F4G1DKPH" },
            {'N', "4105HLI9" },
            {'O', "VCWRQ54U" },
            {'P', "457CPO10" },
            {'Q', "KJGU4P45" },
            {'R', "FJE4IDPF" },
            {'S', "44GJE84F" },
            {'T', "PI09453E" },
            {'U', "MBVBXUE5" },
            {'V', "74569YRW" },
            {'W', "2F2GH4R2" },
            {'X', "TMVK5GTG" },
            {'Y', "FKLRRPOE" },
            {'Z', "14V434TE" },
            {'0', "MJUFO455" },
            {'1', "5477R7DT" },
            {'2', "FK435FR2" },
            {'3', "FKEIC355" },
            {'4', "ZASF422E" },
            {'5', "7ASC1496" },
            {'6', "NCD4534D" },
            {'7', "452FRTEF" },
            {'8', "SMVLOR54" },
            {'9', "NMEYR44D" },
            {'/', "MNYR452E" },
            {'.', "MONY478E" },
            {'-', "R4ER5RX8" },
            {'_', "E14FD4R1" },
            {'a', "54D5S6E5" },
            {'b', "EC54S5EW" },
            {'c', "E4D2S4EE" },
            {'d', "E4F4555E" },
            {'e', "DCGH4S2E" },
            {'f', "DEFF4251" },
            {'g', "OIRS4G2F" },
            {'h', "54G2SWDX" },
            {'i', "54E54ESD" },
            {'j', "CVG4GH5B" },
            {'k', "ECRY54DT" },
            {'l', "MVJFOW9R" },
            {'m', "4101FEW4" },
            {'n', "D145CFGE" },
            {'o', "MLPIE52W" },
            {'p', "578R2CSZ" },
            {'q', "BVNJIE55" },
            {'r', "5E5FW2E5" },
            {'s', "EV54E45W" },
            {'t', "5N7ER5W6" },
            {'u', "PO87EX35" },
            {'v', "MDHW5R8D" },
            {'w', "FCNHW5R7" },
            {'x', "17ES57EE" },
            {'y', "CNUUW0WX" },
            {'z', "DJSIER47" },
            {'*', "KJDDS61A" }

        };

        private static readonly Dictionary<string, char> DecryptMap =
            EncryptMap.ToDictionary(pswd => pswd.Value, pswd => pswd.Key);
        private const char Separator = '|';
        public static string Encrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var DA = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0) DA.Append(Separator);
                char txt = input[i];
                DA.Append(EncryptMap.TryGetValue(txt, out string code) ? code : txt.ToString());
            }
            return DA.ToString();
        }
        public static string Decrypt(string DesEncrypt)
        {
            if (string.IsNullOrEmpty(DesEncrypt)) return string.Empty;
            var DA = new StringBuilder();
            foreach (string token in DesEncrypt.Split(Separator))
            {
                DA.Append(DecryptMap.TryGetValue(token, out char txt) ? txt.ToString() : token);

            }
            return DA.ToString();
        }
    }
}
