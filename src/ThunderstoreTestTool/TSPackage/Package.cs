using System;
using System.IO;
using TSTestTool.TSPackage.CheckRunners;
using TSTestTool.StringBuilderWrapper;
using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;
using System.Text;
using System.CodeDom.Compiler;

namespace TSTestTool.TSPackage;

internal class Package(DirectoryInfo Folder)
{
    public readonly PackageIntegrityRunner runner = new();

    protected static string[] Overrides = [
        "plugins",
        "patchers",
        "core",
        "config"
    ];
    public DirectoryInfo Folder { get; } = Folder;

    public void RunChecks(bool Print = true)
    {

        var dir = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(Folder.FullName);
        runner.RunChecks();
        Directory.SetCurrentDirectory(dir);

        Console.WriteLine("-----------------------------------------------");
        Console.Write("  Checking Package [");
        Console.Write("\x1B[35m");
        Console.Write(Folder.Name);
        Console.Write(GetColorCode_Ext.COLOR_RESET);
        Console.WriteLine(']');
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine();

        var sb = new StringBuilder();
        runner.BuildValidationString(sb, 0);

        Console.Write(sb);
    }
}

internal static class ExtMethod
{
    internal static void BuildValidationString(this BaseTSCheckRunner Runner, StringBuilder sb, int Indent)
    {

        if (Runner.State == CheckStatus.Pending)
            return;

        static string Tabs(int n)
        {
            return new string(' ', n * 2);
        }

        static void DumpBecause(BaseTSCheckRunner Runner, StringBuilder sb, int Indent)
        {
            foreach (var item in Runner.Because)
            {
                sb.Append(Tabs(Indent));
                sb.AppendLine(item);
            }
        }

        //Output the runner's name
        sb.Append(Tabs(Indent));
        sb.Append(Runner.CheckID);
        sb.Append(" [");
        //And state
        sb.Append(Runner.State.GetColorCode());
        sb.Append(Runner.State);
        sb.Append(GetColorCode_Ext.COLOR_RESET);
        sb.AppendLine("]");

        //If the runner is a leaf output it's _Because_ value underneath it
        if (Runner.MyChecks.Length == 0)
        {
            //Leaf runners
            DumpBecause(Runner, sb, Indent + 1);
        }
        else
        {
            //Branch runners

            //A runner that is fatal by its own pre-checks needs to output its because
            if (Runner.State == CheckStatus.Fatal && Runner.Because.Count > 0)
            {
                //Output runner reasons
                DumpBecause(Runner, sb, Indent + 1);
            }
            else
            {
                //Descend into non-fatal runners
                foreach (var runner in Runner.MyChecks)
                {
                    if (runner is BaseTSCheckRunner tsRunner)
                    {
                        tsRunner.BuildValidationString(sb, Indent + 1);
                    }
                }
            }

            sb.AppendLine();
        }

    }
}
