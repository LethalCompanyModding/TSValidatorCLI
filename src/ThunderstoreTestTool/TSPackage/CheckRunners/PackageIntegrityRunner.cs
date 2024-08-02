using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class PackageIntegrityRunner(Package BasePackage) : BaseTSCheckRunner(BasePackage)
{
    public override string CheckGroupID => "Package Integrity Check";
    protected override ICheck[] MyChecks { get => checks; }
    private readonly ICheck[] checks = [
        new BasicIntegrityRunner(BasePackage),
        new OptionalIntegrityRunner(BasePackage)
    ];
}
