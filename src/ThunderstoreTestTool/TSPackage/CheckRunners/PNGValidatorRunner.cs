using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class PNGValidatorRunner(CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheckRunner(ErrorLevel)
{
    public override string CheckID => "PNG Validator";
    public override ICheckRunner[] MyChecks { get => checks; }
    private readonly ICheckRunner[] checks = [
        new FileExistsCheck("icon.png", CheckStatus.Failed),
        new PNGValidationCheck("icon.png", CheckStatus.Failed)
    ];
}
