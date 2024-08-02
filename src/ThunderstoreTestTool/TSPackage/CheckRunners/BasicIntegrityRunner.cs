using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class BasicIntegrityRunner(CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheckRunner(ErrorLevel)
{
    public override string CheckID => "Basic Package Integrity";
    public override ICheckRunner[] MyChecks { get => checks; }
    private readonly ICheckRunner[] checks = [
        new ManifestValidatorRunner(),
        new PNGValidatorRunner(),
    ];
}
