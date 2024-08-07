#if DEBUG

using System.IO;
using System.Diagnostics;
using TSTestTool.TSPackage;
using dev.mamallama.checkrunnerlib.Checks;
using System;

namespace TSTestTool.Debugging;

internal static class Debugger
{
    internal static void RunAllTests()
    {
        string UnitTests = "UnitTests";

        Package BadIcon_1 = new(new(Path.Combine(UnitTests, "BadIconFile")));
        Package BadIcon_2 = new(new(Path.Combine(UnitTests, "BadIconDimensions")));
        Package BadManifest_1 = new(new(Path.Combine(UnitTests, "BadManifestFile")));
        Package BadManifest_2 = new(new(Path.Combine(UnitTests, "BadManifestMalformed")));
        Package GoodPackage = new(new(Path.Combine(UnitTests, "GoodPackage")));

        BadIcon_1.RunChecks();
        BadIcon_2.RunChecks();
        BadManifest_1.RunChecks();
        BadManifest_2.RunChecks();
        GoodPackage.RunChecks();

        Debug.Assert(BadIcon_1.runner.MyChecks[0].MyChecks[2].MyChecks[1].State == CheckStatus.Failed, "PNG Validator failed to find a bad PNG header file in BadIcon_1");
        Debug.Assert(BadIcon_2.runner.MyChecks[0].MyChecks[2].MyChecks[2].State == CheckStatus.Failed, "PNG Validator failed to find a bad PNG width in BadIcon_2");
        Debug.Assert(BadIcon_2.runner.MyChecks[0].MyChecks[2].MyChecks[3].State == CheckStatus.Failed, "PNG Validator failed to find a bad PNG height in BadIcon_2");
        Debug.Assert(BadManifest_1.runner.MyChecks[0].MyChecks[1].State == CheckStatus.Fatal, "Manifest Validator failed to find a bad Manifest file in BadManifest_1");
        Debug.Assert(BadManifest_2.runner.MyChecks[0].MyChecks[1].State == CheckStatus.Fatal, "Manifest Validator failed to find a malformed Manifest file in BadManifest_2");
        Debug.Assert(GoodPackage.runner.MyChecks[0].State == CheckStatus.Succeeded, "Package Validator failed to validate a valid package in GoodPackage");

        Console.WriteLine();
        Console.WriteLine("ALL UNIT TESTS PASSED");
    }
}

#endif
