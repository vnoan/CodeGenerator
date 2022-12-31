using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Core.Services;

public class CodeService : ICodeService
{
    private readonly ILogger<ICodeService> _logger;
    private readonly IConfiguration _configuration;

    public CodeService(ILogger<ICodeService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    public IEnumerable<string> GenerateCodeList(int count, int codeLength, bool withNumbers, bool repeatLetters)
    {
        var codes = new ConcurrentBag<string>();
        var alphabet = GetAlphabet(withNumbers);

        ValidateParams(count, codeLength, alphabet.Length);
        
        Parallel.For(0, count, _ =>
        {
            var code = GenerateCode(alphabet, codeLength, repeatLetters);
            codes.Add(code);
        });

        var  codeList = codes.Distinct().ToList();
        while(codeList.Count < count)
        {
            _logger.LogInformation($"{count - codeList.Count} left");
            var code = GenerateCode(alphabet, codeLength, repeatLetters);
            if(!codeList.Contains(code))
            {
                codeList.Add(code);
            }
        }

        return codeList;
    }

    private string GenerateCode(StringBuilder alphabet, int codeLength, bool repeatLetters)
    {
        Random random = new Random();
        var code = "";
        while (code.Length < codeLength)
        {
            var index = random.Next(alphabet.Length - 1);
            code += alphabet[index];
            if (repeatLetters)
            {
                alphabet.Remove(index, 1);
            }
        }

        return code;
    }

    private void ValidateParams(int count, int length, int alphabetSize)
    {
        int countThreshold = _configuration.GetValue<int>("CodeSettings:MaxCount");
        if (count < 1)
            throw new ArgumentOutOfRangeException("Count should be bigger than 1");
        else if (count > countThreshold)
            throw new ArgumentOutOfRangeException($"Count should be smaller than {countThreshold}");

        int codeLength = _configuration.GetValue<int>("CodeSettings:MaxCodeLength"); ;
        if (length < 1)
            throw new ArgumentOutOfRangeException("Length should be bigger than 1");
        else if (length > codeLength)
            throw new ArgumentOutOfRangeException($"Length should be smaller than {codeLength}");
    }

    private StringBuilder GetAlphabet(bool withNumbers)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numbers = "1234567890";

        var stringBuilder = new StringBuilder(alphabet);

        if (withNumbers)
        {
            stringBuilder = stringBuilder.Append(numbers);
        }
        return stringBuilder;
    }
}
