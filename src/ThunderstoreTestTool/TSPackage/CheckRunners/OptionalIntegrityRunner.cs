using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.Checks;
using dev.mamallama.checkrunnerlib.CheckRunners;

namespace TSTestTool.TSPackage.CheckRunners;

internal class OptionalIntegrityRunner : BaseTSCheckRunner
{
    public override string CheckID => "Optional Package Integrity";
    public override ICheckRunner[] MyChecks => checks;
    private readonly ICheckRunner[] checks = [
        new FileExistsCheck("Changelog.md"){ErrorLevel = CheckStatus.Warning},
        new FileExistsCheck("LICENSE"){ErrorLevel = CheckStatus.Warning},
    ];
}

