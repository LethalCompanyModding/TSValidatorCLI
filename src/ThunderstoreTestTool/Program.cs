using System;
using System.IO;
using TSTestTool.Packages;
using TSTestTool.StringBuilderWrapper;

namespace TSTestTool;

internal class Program
{
    /// <summary>
    /// <para> 
    /// TSTestTool, the Thunderstore package tester
    /// Written by Robyn
    /// </para>
    /// </summary>
    /// <param name="f">The folder that contains your TS Package files</param>
    static void Main(DirectoryInfo f)
    {
        f ??= new(Directory.GetCurrentDirectory());

        //Console.WriteLine($"Using directory {f.FullName}");

        Package package = new(f);

        //Set the working directory to the given directory
        var wDir = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(f.FullName);

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
        finally
        {
            //Reset the working directory
            Directory.SetCurrentDirectory(wDir);
        }


    }
}
