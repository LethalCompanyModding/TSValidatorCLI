using System;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks.PNGValidators;

internal class PNGWidthValidationCheck(CheckStatus ErrorLevel = CheckStatus.Fatal) : BasePNGValidationCheck(0x10, [0x00, 0x00, 0x01, 0x00], "Header", ErrorLevel)
{
    public override string CheckID => "Width Validation";
}
