using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.Checks;
using dev.mamallama.checkrunnerlib.CheckRunners;

namespace TSTestTool.TSPackage.CheckRunners;

internal class OptionalIntegrityRunner(CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheckRunner(ErrorLevel)
{
    public override string CheckID => "Optional Package Integrity";
    public override ICheckRunner[] MyChecks => checks;
    private readonly ICheckRunner[] checks = [
        new FileExistsCheck("Changelog.md", CheckStatus.Warning),
        new FileExistsCheck("LICENSE", CheckStatus.Warning),
    ];
}

