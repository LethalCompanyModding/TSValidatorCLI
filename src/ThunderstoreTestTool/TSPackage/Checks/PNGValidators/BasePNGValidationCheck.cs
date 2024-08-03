using System;
using System.IO;
using System.Reflection;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks.PNGValidators;

internal abstract class BasePNGValidationCheck(int Offset, byte[] matches, string Matching = "Buffer", CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheck(ErrorLevel)
{
    public byte[] Data = [];
    private readonly int Offset = Offset;
    private readonly string Matching = Matching;
    private readonly byte[] matches = matches;

    public override void RunChecks()
    {
        if (Data.Length < 24)
        {
            SetStateAndReason(CheckStatus.Fatal, "Buffer is malformed");
            return;
        }

        Span<byte> data = new(Data, Offset, 4);
        Span<byte> pattern = new(matches, 0, 4);

        if (data == pattern)
        {
            SetStateAndReason(CheckStatus.Failed, $"{Matching} does not match");
            return;
        }

        SetStateAndReason(CheckStatus.Failed, $"{Matching} validated");
    }
}