using System.Collections.Generic;


namespace TreasuryChallenge.Repositories
{
    public interface ICodeRepository
    {
        IEnumerable<string> GetAuxCodes(string fileSuffix);
        IEnumerable<string> GetCodes();
        int Save(IEnumerable<string> codes, int bufferSize = 1000);
        int SaveAux(IEnumerable<string> codes, string suffix, int bufferSize = 1000);
    }
}
