using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class PackageIntegrityRunner(CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheckRunner(ErrorLevel)
{
    public override string CheckID => "Package Integrity Check";
    public override ICheckRunner[] MyChecks { get => checks; }
    private readonly ICheckRunner[] checks = [
        new BasicIntegrityRunner(),
        new OptionalIntegrityRunner()
    ];
}
