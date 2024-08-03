using System.IO;
using TSTestTool.TSPackage.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks;

internal class FileExistsCheck(string FileName, CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheck(ErrorLevel)
{
    protected string FileName = FileName;
    public override string CheckID => "File Exists";

    public override void RunChecks()
    {
        FileInfo info = new(FileName);

        if (info.Exists)
        {
            SetStateAndReason(CheckStatus.Succeeded, $"{FileName} found");
            return;
        }

        SetStateAndReason(CheckStatus.Failed, $"{FileName} not found");
    }
}
