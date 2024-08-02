using System;
using System.Text.Json;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks;

internal class JSONFieldValidationCheck(string Field, bool InverseCheck = false, CheckStatus ErrorLevel = CheckStatus.Failed) : BaseTSCheck(ErrorLevel)
{
    public JsonElement rootElement;
    protected string Field = Field;
    public override string CheckID => $"Field Validation: {Field}";

    public override void RunChecks()
    {
        try
        {
            foreach (JsonProperty item in rootElement.EnumerateObject())
            {
                Console.WriteLine(item.Name);
                if (Field == item.Name)
                {
                    if (InverseCheck)
                        SetStateAndReason(CheckStatus.Warning, "Unsupported field");
                    else
                        SetStateAndReason(CheckStatus.Succeeded, "Field Validated");

                    return;
                }
            }

        }
        catch (Exception e) when (e is JsonException)
        {
            SetStateAndReason(CheckStatus.Fatal, "Unable to parse root element");
            return;
        }

        SetStateAndReason(InverseCheck ?
        CheckStatus.Succeeded : CheckStatus.Failed,
        "Field does not exist in document");
    }
}
