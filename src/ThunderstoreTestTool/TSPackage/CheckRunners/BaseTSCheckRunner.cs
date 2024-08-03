using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

/// <summary>
/// 
/// </summary>
/// <param name="ErrorLevel"></param> <summary>
/// The highest ErrorLevel this Runner can become
/// </summary>
internal abstract class BaseTSCheckRunner(CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseCheckRunner
{
    protected CheckStatus ErrorLevel = ErrorLevel;
    public string? Because { get; private set; }
    protected override void UpdateState(CheckStatus IncState)
    {
        if ((int)IncState > (int)ErrorLevel)
            IncState = ErrorLevel;

        base.UpdateState(IncState);
    }
    protected virtual void SetStateAndReason(CheckStatus IncState, string Reason)
    {
        Because = Reason;
        UpdateState(IncState);
    }
}
