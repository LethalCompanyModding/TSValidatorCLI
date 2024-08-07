using System;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks.PNGValidators;

internal class PNGHeaderValidationCheck() : BasePNGValidationCheck(0, [0x89, 0x50, 0x4E, 0x47], "Header")
{
    public override string CheckID => "Header Validation";
}
