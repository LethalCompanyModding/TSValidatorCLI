using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class ManifestValidatorRunner(Package BasePackage) : BaseTSCheckRunner(BasePackage)
{
    public override string CheckGroupID => "Manifest Validator";
    protected override ICheck[] MyChecks { get => checks; }
    private readonly ICheck[] checks = [
        new FileExistsCheck("manifest.json", CheckStatus.Failed),
        new JSONValidationCheck("manifest.json", CheckStatus.Failed),
    ];
}
