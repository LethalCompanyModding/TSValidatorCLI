using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.Checks;
using dev.mamallama.checkrunnerlib.CheckRunners;

namespace TSTestTool.TSPackage.CheckRunners;

internal class ManifestValidatorRunner(CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheckRunner(ErrorLevel)
{
    public override string CheckID => "Manifest Validator";
    public override ICheckRunner[] MyChecks { get => checks; }
    private readonly ICheckRunner[] checks = [
        new FileExistsCheck("manifest.json", CheckStatus.Failed),
        new JSONValidationCheck("manifest.json", CheckStatus.Failed),
    ];
}
