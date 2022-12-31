using System;
using System.Text;

namespace TreasuryChallenge.Services
{
    public class CodeService : ICodeService
    {
        private string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string GenerateCode(int length)
        {
            ValidateLength(length);

            Random random = new Random();
            var stringBuilder = new StringBuilder(_alphabet);

            var code = "";
            while (code.Length < length)
            {
                var index = random.Next(stringBuilder.Length - 1);
                code += stringBuilder[index];
                stringBuilder.Remove(index, 1);
            }

            return code;
        }

        private void ValidateLength(int length)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException("Length should be bigger than 1");
            else if (length > _alphabet.Length)
                throw new ArgumentOutOfRangeException($"Length should be smaller than {_alphabet.Length}");
        }
    }
}
