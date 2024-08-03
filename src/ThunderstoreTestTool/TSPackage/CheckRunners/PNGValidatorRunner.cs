using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;
using TSTestTool.TSPackage.Checks.PNGValidators;
using System.IO;

namespace TSTestTool.TSPackage.CheckRunners;

internal class PNGValidatorRunner(string FileName, CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheckRunner(ErrorLevel)
{
    public override string CheckID => "PNG Validator";
    public override ICheckRunner[] MyChecks { get => checks; }
    private readonly BaseTSCheck[] checks = [
        new FileExistsCheck("icon.png", ErrorLevel),
        new PNGHeaderValidationCheck(ErrorLevel),
        new PNGWidthValidationCheck(ErrorLevel),
        new PNGHeightValidationCheck(ErrorLevel),
    ];

    public override void RunChecks()
    {

        FileInfo info = new(FileName);

        if (!info.Exists)
        {
            SetStateAndReason(CheckStatus.Fatal, $"Unable to open {FileName}");
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
