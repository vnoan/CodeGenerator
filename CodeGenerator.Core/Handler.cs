//using CodeGenerator.Core.Repositories;
//using CodeGenerator.Core.Services;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CodeGenerator.Core;

//public class Handler
//{
//    private readonly ILogger<Handler> _logger;
//    private readonly ICodeService _codeService;
//    private readonly ICodeRepository _codeRepository;
//    private readonly IConfiguration _configuration;

//    public Handler(ILogger<Handler> logger, ICodeService codeService, ICodeRepository codeRepository, IConfiguration configuration)
//    {
//        _logger = logger;
//        _codeService = codeService;
//        _codeRepository = codeRepository;
//        _configuration = configuration;
//    }

//    public void Handle(int lineCount)
//    {
//        bool uniqueness = bool.Parse(_configuration["Uniqueness"]);
//        int codeLength = int.Parse(_configuration["CodeLength"]);
//        int blockSize = int.Parse(_configuration["BlockSize"]);
//        int bufferSize = blockSize * sizeof(char) * codeLength;

//        _logger.LogInformation(
//            $"Generating {lineCount} codes of {codeLength} letters "
//            + (uniqueness ? "with uniqueness between them" : "accepting duplicated codes")
//            );

//        var codes = new ConcurrentBag<string>();
//        var codesByPrefix = new ConcurrentDictionary<string, ConcurrentBag<string>>();
//        var newLineCount = 0;
//        while (newLineCount < lineCount)
//        {
//            var timesLeft = lineCount - newLineCount;
//            var timesToExecute = timesLeft < blockSize ? timesLeft : blockSize;

//            _logger.LogInformation($"{timesLeft} codes left");

//            Parallel.For(0, timesToExecute, _ =>
//            {
//                var code = _codeService.GenerateCode(codeLength);
//                codes.Add(code);

//                if (uniqueness)
//                {
//                    var prefix = code[0..1];
//                    codesByPrefix.AddOrUpdate(
//                        prefix,
//                        new ConcurrentBag<string> { code },
//                        (key, value) =>
//                        {
//                            value.Add(code);
//                            return value;
//                        });
//                }
//            });

//            var codeList = codes.ToList();
//            if (uniqueness)
//            {
//                codeList = RemoveDuplicated(codesByPrefix, codeList).ToList();
//                codesByPrefix.Clear();
//            }

//            _codeRepository.Save(codeList, bufferSize);

//            newLineCount += codeList.Count;
//            codes.Clear();
//        }
//    }

//    private IEnumerable<string> RemoveDuplicated(ConcurrentDictionary<string, ConcurrentBag<string>> dict, List<string> codeList)
//    {
//        var codesToRemove = new List<string>();
//        foreach (var keyValue in dict)
//        {
//            codesToRemove.AddRange(GetDuplicated(keyValue.Key, keyValue.Value));
//        }
//        dict.Clear();
//        codeList.RemoveAll(c => codesToRemove.Contains(c));
//        return codeList.Distinct().ToList();
//    }

//    private IEnumerable<string> GetDuplicated(string key, IEnumerable<string> valueList)
//    {
//        var savedCodes = _codeRepository.GetAuxCodes(key);
//        var auxCodesToSave = new ConcurrentBag<string>();
//        var auxCodesToRemove = new ConcurrentBag<string>();
//        Parallel.ForEach(valueList, value =>
//        {
//            if (savedCodes.Contains(value))
//                auxCodesToRemove.Add(value);
//            else
//                auxCodesToSave.Add(value);
//        });
//        _codeRepository.SaveAux(auxCodesToSave, key);
//        return auxCodesToRemove;
//    }
//}
