using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TreasuryChallenge.Repositories
{
    public class CodeRepository : ICodeRepository
    {
        private readonly ILogger<CodeRepository> _logger;

        public CodeRepository(ILogger<CodeRepository> logger)
        {
            _logger = logger;
        }
        public int Save(IEnumerable<string> codes, int bufferSize = 1000)
        {
            return SaveFile(codes, "codes.txt", bufferSize);
        }

        public int SaveAux(IEnumerable<string> codes, string suffix, int bufferSize = 1000)
        {
            return SaveFile(codes, $"codes_{suffix}.txt", bufferSize);
        }

        private int SaveFile(IEnumerable<string> codes, string fileName, int bufferSize = 1000)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8, bufferSize))
            {
                foreach (var code in codes)
                {
                    writer.WriteLine(code);
                }
            }
            return codes.Count();
        }

        public IEnumerable<string> GetCodes()
        {
            return GetFromFile("codes.txt");
        }

        public IEnumerable<string> GetAuxCodes(string fileSuffix)
        {
            return GetFromFile($"codes_{fileSuffix}.txt");
        }

        private IEnumerable<string> GetFromFile(string fileName)
        {
            var codes = new List<string>();
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        codes.Add(reader.ReadLine());
                    }
                }
            }
            else
            {
                _logger.LogWarning("File not found. Returning empty list");
            }
            return codes;
        }
    }
}
