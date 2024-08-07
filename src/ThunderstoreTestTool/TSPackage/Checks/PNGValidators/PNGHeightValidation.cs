using System;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks.PNGValidators;

internal class PNGHeightValidationCheck() : BasePNGValidationCheck(0x14, [0x00, 0x00, 0x01, 0x00], "Height")
{
    public override string CheckID => "Height Validation";
}
