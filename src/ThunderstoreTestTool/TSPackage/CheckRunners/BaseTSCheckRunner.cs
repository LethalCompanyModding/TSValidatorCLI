using dev.mamallama.checkrunnerlib.CheckRunners;

namespace TSTestTool.TSPackage.CheckRunners;

internal abstract class BaseTSCheckRunner(Package BasePackage) : BaseCheckRunner
{
    protected Package BasePackage = BasePackage;
}
