using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using dev.mamallama.checkrunnerlib.Checks;

namespace TSTestTool.TSPackage.Checks;

internal class JSONValidationCheck(string FileName, CheckStatus ErrorLevel = CheckStatus.Failed) : BaseCheck(ErrorLevel)
{
    protected string FileName = FileName;
    protected static Dictionary<string, bool> Fields = new(){
      {"name", false},
      {"description", false},
      {"version_number", false},
      {"dependencies", false},
      {"website_url", false},
    };
    public override string CheckID => "JSON Validation";

    public override CheckValidation RunCheck()
    {
        FileInfo info = new(FileName);
        List<CheckValidation> Validations = [];
        CheckStatus status = CheckStatus.Succeeded;

        if (!info.Exists)
            return new(CheckID, CheckStatus.Fatal, $"{FileName} not found");

        try
        {
            using (var fs = info.OpenRead())
            {
                using (JsonDocument document = JsonDocument.Parse(fs, new JsonDocumentOptions() { MaxDepth = 2 }))
                {
                    foreach (JsonProperty item in document.RootElement.EnumerateObject())
                    {
                        Console.WriteLine(item.Name);

                        if (Fields.TryGetValue(item.Name, out bool value))
                        {
                            //We've already seen this term
                            if (value)
                            {
                                Validations.Add(new("Duplicate Field", CheckStatus.Failed, item.Name));
                                status = CheckStatus.Failed;
                            }
                            else
                            {
                                Validations.Add(new("Required Field", CheckStatus.Succeeded, item.Name));
                                Fields[item.Name] = true;
                            }
                        }
                        else
                        {
                            if (item.Name == "installers")
                            {
                                Validations.Add(new("Unused Field", CheckStatus.Warning, item.Name));

                                if (status != CheckStatus.Failed)
                                    status = CheckStatus.Warning;
                            }
                            else
                            {
                                Validations.Add(new("Unknown Extra Field", CheckStatus.Warning, item.Name));

                                if (status != CheckStatus.Failed)
                                    status = CheckStatus.Warning;
                            }
                        }

                    }
                }
            }
        }
        catch (Exception e) when (e is UnauthorizedAccessException or DirectoryNotFoundException or IOException)
        {
            //failed to read the file
            return new CheckValidation(CheckID, CheckStatus.Fatal, "Unable to open manifest.json");
        }
        catch (Exception e) when (e is JsonException)
        {
            return new CheckValidation(CheckID, CheckStatus.Fatal, "Unable to parse JSON, manifest.json is malformed");
        }

        foreach (string item in Fields.Keys)
        {
            if (!Fields[item])
            {
                Validations.Add(new("Required Field Missing", CheckStatus.Failed, item));
                status = CheckStatus.Failed;
            }
        }

        return new(CheckID, status, "", [.. Validations]);

    }
}
