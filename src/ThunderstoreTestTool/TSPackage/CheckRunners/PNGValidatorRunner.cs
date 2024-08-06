using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;
using TSTestTool.TSPackage.Checks.PNGValidators;
using System.IO;

namespace TSTestTool.TSPackage.CheckRunners;

internal class PNGValidatorRunner(string FileName) : BaseTSCheckRunner
{
    public override string CheckID => "PNG Validator";
    public override ICheckRunner[] MyChecks => checks;
    private readonly BaseTSCheck[] checks = [
        new FileExistsCheck("icon.png"),
        new PNGHeaderValidationCheck(),
        new PNGWidthValidationCheck(),
        new PNGHeightValidationCheck(),
    ];

    public override void RunChecks()
    {

        FileInfo info = new(FileName);

        if (!info.Exists)
        {
            Because.Add($"{FileName} not found");
            UpdateState(CheckStatus.Fatal);
            return;
        }

        byte[] buffer = new byte[24];

        using (var fs = info.OpenRead())
        {
            fs.ReadExactly(buffer, 0, 24);
        }

        foreach (var item in checks)
        {
            if (item is BasePNGValidationCheck png)
            {
                png.Data = buffer;
            }
        }

        base.RunChecks();
    }
}
