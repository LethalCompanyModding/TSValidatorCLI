using System;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks.PNGValidators;

internal class PNGHeightValidationCheck(CheckStatus ErrorLevel = CheckStatus.Fatal) : BasePNGValidationCheck(0x14, [0x00, 0x00, 0x01, 0x00], "Height", ErrorLevel)
{
    public override string CheckID => "Height Validation";
}
