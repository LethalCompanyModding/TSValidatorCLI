using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.Packages.CheckRunners;

internal class BasicIntegrityRunner(Package BasePackage) : BaseTSCheckRunner(BasePackage)
{
    public override string CheckGroupID => "Basic Package Integrity";
    protected override ICheck[] MyChecks { get => checks; }
    private readonly ICheck[] checks = [
        new ManifestValidatorRunner(BasePackage),
        new PNGValidatorRunner(BasePackage),
    ];
}
