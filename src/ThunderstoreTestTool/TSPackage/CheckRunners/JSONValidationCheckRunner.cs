using System;
using System.IO;
using System.Text.Json;
using dev.mamallama.checkrunnerlib.Checks;
using dev.mamallama.checkrunnerlib.CheckRunners;
using TSTestTool.TSPackage.Checks;

namespace TSTestTool.TSPackage.CheckRunners;

internal class JSONValidationCheckRunner(string FileName, CheckStatus ErrorLevel = CheckStatus.Fatal) : BaseTSCheckRunner(ErrorLevel)
{
    protected string FileName = FileName;
    public override string CheckID => "JSON Validation";

    public override ICheckRunner[] MyChecks => checks;
    private readonly BaseTSCheck[] checks = [
        new FileExistsCheck("manifest.json", CheckStatus.Failed),
        new JSONFieldValidationCheck("name"),
        new JSONFieldValidationCheck("description"),
        new JSONFieldValidationCheck("version_number"),
        new JSONFieldValidationCheck("dependencies"),
        new JSONFieldValidationCheck("website_url"),
        new JSONFieldValidationCheck("installers", InverseCheck: true),
    ];

    public override void RunChecks()
    {

        FileInfo info = new(FileName);

        if (!info.Exists)
        {
            SetStateAndReason(CheckStatus.Fatal, $"{FileName} not found");
            return;
        }

        try
        {
            using var fs = info.OpenRead();
            using JsonDocument document = JsonDocument.Parse(fs, new JsonDocumentOptions() { MaxDepth = 2 });

            //set the root element for each JSON item
            foreach (var item in checks)
            {
                if (item is JSONFieldValidationCheck json)
                {
                    json.rootElement = document.RootElement;
                }
            }

            //Run checks and update my state
            base.RunChecks();

        }
        catch (Exception e) when (e is UnauthorizedAccessException or DirectoryNotFoundException or IOException)
        {
            //failed to read the file
            SetStateAndReason(CheckStatus.Fatal, "Unable to open manifest.json");
            return;
        }
        catch (Exception e) when (e is JsonException)
        {
            SetStateAndReason(CheckStatus.Fatal, "Unable to parse JSON, manifest.json is malformed");
            return;
        }
    }
}
