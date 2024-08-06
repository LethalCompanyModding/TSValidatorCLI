using System.IO;
using TSTestTool.TSPackage.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;
using dev.mamallama.checkrunnerlib.CheckRunners;

namespace TSTestTool.TSPackage.Checks;


internal abstract class BaseTSCheck : BaseTSCheckRunner
{
    public override ICheckRunner[] MyChecks => [];
}
