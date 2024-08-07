using System.Collections.Generic;
using dev.mamallama.checkrunnerlib.CheckRunners;

namespace TSTestTool.TSPackage.CheckRunners;

internal abstract class BaseTSCheckRunner : BaseCheckRunnerLimited
{
    public List<string> Because { get; } = [];
}
