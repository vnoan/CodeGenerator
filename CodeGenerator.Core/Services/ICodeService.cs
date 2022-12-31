namespace CodeGenerator.Core.Services;

public interface ICodeService
{
    public IEnumerable<string> GenerateCodeList(int count, int codeLength, bool numbers, bool repeatLetters);
}
