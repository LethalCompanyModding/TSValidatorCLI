using System.IO;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.Packages.Checks;

internal class FileExistsCheck(string FileName, CheckStatus ErrorLevel = CheckStatus.Failed) : BaseCheck(ErrorLevel)
{
    protected string FileName = FileName;
    public override string CheckID => "File Exists";

    public override CheckValidation RunCheck()
    {
        FileInfo info = new(FileName);

        if (info.Exists)
            return new(CheckID, CheckStatus.Succeeded, $"{FileName} was found");

        return new(CheckID, ErrorLevel, $"{FileName} not found");
    }
}
