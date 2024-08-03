using System;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks.PNGValidators;

internal class PNGHeaderValidationCheck(CheckStatus ErrorLevel = CheckStatus.Fatal) : BasePNGValidationCheck(0, [0x89, 0x50, 0x4E, 0x47], "Header", ErrorLevel)
{
    public override string CheckID => "Header Validation";
}
