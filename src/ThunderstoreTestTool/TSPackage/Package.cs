using System;
using System.IO;
using TSTestTool.TSPackage.CheckRunners;
using TSTestTool.StringBuilderWrapper;
using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.CheckRunners;

namespace TSTestTool.TSPackage;

internal class Package(DirectoryInfo Folder)
{
    private PackageIntegrityRunner? runner;

    protected static string[] Overrides = [
        "plugins",
        "patchers",
        "core",
        "config"
    ];
    public DirectoryInfo Folder { get; } = Folder;
    protected readonly ICheckRunnerRunner[] Runners = [];

    public void RunChecks(bool Print = true)
    {
        runner ??= new(this);

        var validation = runner.RunCheck();


        Console.WriteLine("-----------------------------------------------");
        Console.Write("  Checking Package [");
        Console.Write("\x1B[35m");
        Console.Write(Folder.Name);
        Console.Write(GetColorCode_Ext.COLOR_RESET);
        Console.WriteLine(']');
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine();

        PrintValidationToConsole(validation, 0);
    }

    protected static void PrintValidationToConsole(CheckValidation Validation, int Indent)
    {

        //Do not output pending or children of pending validations
        if (Validation.Passed == CheckStatus.Pending)
            return;

        string Tabs(int n)
        {
            return new string(' ', n * 2);
        }

        //Print the current validation at the current Indentation level
        using (StringBuilderDisposable sb = new(Tabs(Indent)))
        {
            sb.Append(Validation.CheckName);
            sb.Append(" [");
            sb.Append(Validation.Passed.GetColorCode());
            sb.Append(Validation.Passed);
            sb.Append(GetColorCode_Ext.COLOR_RESET);
            sb.AppendLine("]");
            Console.Write(sb);
        }

        if (Validation.InnerValidations is not null)
        {
            foreach (var item in Validation.InnerValidations)
            {
                PrintValidationToConsole(item, Indent + 1);
            }
        }
        else
        {
            using (StringBuilderDisposable sb = new(Tabs(Indent + 1)))
            {
                sb.Append(Validation.Because);
                sb.AppendLine();
                Console.Write(sb);
            }
        }

    }
}
