namespace TSTestTool.TSPackage.Checks.PNGValidators;

internal class PNGWidthValidationCheck() : BasePNGValidationCheck(0x10, [0x00, 0x00, 0x01, 0x00], "Width")
{
    public override string CheckID => "Width Validation";
}
