using System.Collections.Generic;
using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

/// <summary>
/// 
/// </summary>
/// <param name="ErrorLevel"></param> <summary>
/// The highest ErrorLevel this Runner can become
/// </summary>
internal abstract class BaseTSCheckRunner(CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseCheckRunnerLimited(ErrorLevel)
{
    public List<string> Because { get; } = [];
}
