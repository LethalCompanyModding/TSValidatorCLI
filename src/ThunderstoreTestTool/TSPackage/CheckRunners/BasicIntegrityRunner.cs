using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;
using TSTestTool.TSPackage.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class BasicIntegrityRunner : BaseTSCheckRunner
{
    public override string CheckID => "Basic Package Integrity";
    public override ICheckRunner[] MyChecks => checks;
    private readonly ICheckRunner[] checks = [
        new FileExistsCheck("README.md"){ErrorLevel = CheckStatus.Pending},
        new JSONValidationCheckRunner("manifest.json"),
        new PNGValidatorRunner("icon.png"),
    ];
}
