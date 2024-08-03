using System;
using System.IO;
using TSTestTool.TSPackage.CheckRunners;
using TSTestTool.StringBuilderWrapper;
using dev.mamallama.checkrunnerlib.CheckRunners;
using dev.mamallama.checkrunnerlib.Checks;
using System.Text;

namespace TSTestTool.TSPackage;

internal class Package(DirectoryInfo Folder)
{
    private readonly PackageIntegrityRunner runner = new();

    protected static string[] Overrides = [
        "plugins",
        "patchers",
        "core",
        "config"
    ];
    public DirectoryInfo Folder { get; } = Folder;

    public void RunChecks(bool Print = true)
    {

        runner.RunChecks();

        Console.WriteLine("-----------------------------------------------");
        Console.Write("  Checking Package [");
        Console.Write("\x1B[35m");
        Console.Write(Folder.Name);
        Console.Write(GetColorCode_Ext.COLOR_RESET);
        Console.WriteLine(']');
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine();

        var sb = new StringBuilder();
        runner.GetValidationString(sb, 0);

        Console.Write(sb);
    }
}

internal static class ExtMethod
{
    internal static void GetValidationString(this BaseTSCheckRunner Runner, StringBuilder sb, int Indent)
    {

        string Tabs(int n)
        {
            return new string(' ', n * 2);
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
            //Leaf
            sb.Append(Tabs(Indent + 1));
            sb.AppendLine(Runner.Because);
        }
        else
        {
            //Branch
            foreach (var runner in Runner.MyChecks)
            {
                if (runner is BaseTSCheckRunner tsRunner)
                {
                    tsRunner.GetValidationString(sb, Indent + 1);
                }
            }
            sb.AppendLine();
        }

    }
}
