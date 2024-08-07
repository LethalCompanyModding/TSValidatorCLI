using System;
using System.Text.Json;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks;

internal class JSONFieldValidationCheck(string Field, bool InverseCheck = false) : BaseTSCheck
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
                //Console.WriteLine(item.Name);

                if (Field == item.Name)
                {
                    if (InverseCheck)
                    {
                        Because.Add("Unsupported field");
                        UpdateState(CheckStatus.Warning);

                    }
                    else
                    {
                        Because.Add("Field Validated");
                        UpdateState(CheckStatus.Succeeded);
                    }

                    return;
                }
            }

        }
        catch (JsonException)
        {
            Because.Add("Unable to parse root element");
            UpdateState(CheckStatus.Fatal);
            return;
        }

        if (InverseCheck)
        {
            Because.Add("Unsupported field not found");
            UpdateState(CheckStatus.Succeeded);

        }
        else
        {
            Because.Add("Required field not found");
            UpdateState(CheckStatus.Failed);
        }
    }
}
