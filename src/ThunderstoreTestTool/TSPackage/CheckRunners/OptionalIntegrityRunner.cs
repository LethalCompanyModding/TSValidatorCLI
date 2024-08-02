using TSTestTool.TSPackage.Checks;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class OptionalIntegrityRunner(Package BasePackage) : BasicIntegrityRunner(BasePackage)
{
    public override string CheckGroupID => "Optional Package Integrity";
    protected override ICheck[] MyChecks => checks;
    private readonly ICheck[] checks = [
        new FileExistsCheck("Changelog.md"),
        new FileExistsCheck("LICENSE"),
    ];

    protected override void UpdateState(CheckValidation[] Validations)
    {
        //strict checking
        foreach (var item in Validations)
        {
            if (item.Passed != CheckStatus.Succeeded)
            {
                State = CheckStatus.Warning;
                return;
            }
        }

        State = CheckStatus.Succeeded;
    }
}

