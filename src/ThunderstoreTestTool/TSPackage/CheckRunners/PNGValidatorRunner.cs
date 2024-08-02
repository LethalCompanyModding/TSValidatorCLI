using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class PNGValidatorRunner(Package BasePackage) : BaseTSCheckRunner(BasePackage)
{
    public override string CheckGroupID => "PNG Validator";
    protected override ICheck[] MyChecks { get => checks; }
    private readonly ICheck[] checks = [
        new FileExistsCheck("icon.png", CheckStatus.Failed),
        new PNGValidationCheck("icon.png", CheckStatus.Failed)
    ];
}