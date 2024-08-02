using System.IO;
using System.Collections.Generic;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks;

internal class PNGValidationCheck(string FileName, CheckStatus ErrorLevel = CheckStatus.Failed) : BaseCheck(ErrorLevel)
{
    protected string FileName = FileName;
    public override string CheckID => "PNG Validation";

    public override CheckValidation RunCheck()
    {
        FileInfo info = new(FileName);
        List<CheckValidation> Validations = [];

        if (!info.Exists)
            return new(CheckID, CheckStatus.Fatal, $"{FileName} not found");

        byte[] buffer = new byte[24];

        using (var fs = info.OpenRead())
        {
            fs.ReadExactly(buffer, 0, 24);
        }

        //Check for magic PNG bytes
        if (buffer[1] == 'P' && buffer[2] == 'N' && buffer[3] == 'G')
            Validations.Add(new("Valid PNG File", CheckStatus.Succeeded, "Valid header magic found"));
        else
            Validations.Add(new("Valid PNG File", CheckStatus.Failed, "Header magic does not match PNG"));

        //Check width and height
        //Width and Height should always read 00 00 01 00 (256)
        if (buffer[0x10] == 0 && buffer[0x11] == 0 && buffer[0x12] == 1 && buffer[0x13] == 0)
            Validations.Add(new("Correct Dimensions", CheckStatus.Succeeded, "Width is 256"));
        else
            Validations.Add(new("Correct Dimensions", CheckStatus.Failed, "Width not 256"));

        if (buffer[0x14] == 0 && buffer[0x15] == 0 && buffer[0x16] == 1 && buffer[0x17] == 0)
            Validations.Add(new("Correct Dimensions", CheckStatus.Succeeded, "Height is 256"));
        else
            Validations.Add(new("Correct Dimensions", CheckStatus.Failed, "Height not 256"));

        return new(CheckID, CheckStatus.Warning, "", [.. Validations]);

    }
}
