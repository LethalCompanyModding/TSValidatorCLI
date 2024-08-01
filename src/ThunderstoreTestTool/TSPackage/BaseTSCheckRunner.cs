using dev.mamallama.checkrunnerlib.CheckRunners;

namespace TSTestTool.Packages;

internal abstract class BaseTSCheckRunner(Package BasePackage) : BaseCheckRunner
{
    protected Package BasePackage = BasePackage;
}
