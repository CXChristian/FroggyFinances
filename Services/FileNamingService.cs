using System;

namespace expense_transactions.Services;

public class FileNamingService
{
    public string MakeUniqueFileName(string fileName)
    {
        var baseFileName = Path.GetFileNameWithoutExtension(fileName);
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var uniqueFileName = $"{baseFileName}_{timestamp}.imported";
        return uniqueFileName;
    }
}
