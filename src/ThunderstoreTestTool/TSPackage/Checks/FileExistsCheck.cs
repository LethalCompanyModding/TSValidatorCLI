using System.IO;
using TSTestTool.TSPackage.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks;

internal class FileExistsCheck(string FileName) : BaseTSCheck
{
    protected string FileName = FileName;
    public override string CheckID => "File Exists";

    public override void RunChecks()
    {
        FileInfo info = new(FileName);

        if (info.Exists)
        {
            Because.Add($"{FileName} found");
            UpdateState(CheckStatus.Succeeded);
            return;
        }

        Because.Add($"{FileName} not found");
        UpdateState(CheckStatus.Failed);
    }
}
