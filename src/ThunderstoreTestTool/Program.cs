using System;
using System.IO;
using TSTestTool.TSPackage;
using TSTestTool.StringBuilderWrapper;

namespace TSTestTool;

internal class Program
{

    internal static bool ReturnFromDebugging = false;

    /// <summary>
    /// <para> 
    /// TSTestTool, the Thunderstore package tester
    /// Written by Robyn
    /// </para>
    /// </summary>
    /// <param name="f">The folder that contains your TS Package files</param>
    /// <param name="RunUnitTests">[DEBUG BUILDS ONLY] Pass this switch to run full unit testing</param>
    static void Main(DirectoryInfo f, bool RunUnitTests)
    {

#if DEBUG
        if (RunUnitTests)
        {
            Console.Clear();
            Console.WriteLine("Built in debug mode, running tests");
            Debugging.Debugger.RunAllTests();
            ReturnFromDebugging = true;
        }
#endif

        if (ReturnFromDebugging)
            return;


        f ??= new(Directory.GetCurrentDirectory());

        //Console.WriteLine($"Using directory {f.FullName}");

        Package package = new(f);

        try
        {
            package.RunChecks();
        }
        catch (Exception e)
        {
            using StringBuilderDisposable sb = new("Runners failed to complete all tasks\n");
            sb.Append("  ");
            sb.AppendLine(e.GetType().ToString());

            sb.Append("  ");
            sb.AppendLine(e.Message);
            Console.WriteLine(sb);
        }


    }
}
