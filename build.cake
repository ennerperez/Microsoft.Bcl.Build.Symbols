#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./build") + Directory(configuration);

// Define solutions.
var solutions = new Dictionary<string, string> {
     { "./src/Microsoft.Bcl.Build.Symbols.sln", "Any" },
};

// Define AssemblyInfo source.
var assemblyInfoVersion = ParseAssemblyInfo("./src/Microsoft.Bcl.Build.Symbols/Properties/AssemblyInfo.cs");

// Define version.
var ticks = DateTime.Now.ToString("ddHHmmss");
var assemblyVersion = assemblyInfoVersion.AssemblyVersion.Replace(".*", "." + ticks.Substring(ticks.Length-8,8));
var version = EnvironmentVariable("APPVEYOR_BUILD_VERSION") ?? Argument("version", assemblyVersion);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
    //CleanDirectories("./**/bin");
    //CleanDirectories("./**/obj");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    foreach (var solution in solutions)
    {
        NuGetRestore(solution.Key);
    }
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    foreach (var file in solutions)
    {
        if (IsRunningOnWindows())
        {
            var settings = new MSBuildSettings()
            .WithProperty("PackageVersion", version)
            .WithProperty("BuildSymbolsPackage", "false")
            .WithProperty("ToolVersion","MSBuildToolVersion.VS2019");
            settings.SetConfiguration(configuration);
            // Use MSBuild
            MSBuild(file.Key, settings);
        }
        else
        {
            var settings = new XBuildSettings()
            .WithProperty("PackageVersion", version)
            .WithProperty("BuildSymbolsPackage", "false");
            settings.SetConfiguration(configuration);
            // Use XBuild
            XBuild(file.Key, settings);
        }
    }
   
});

Task("Build-NuGet-Packages")
    .IsDependentOn("Build")
    .Does(() =>
    {
        foreach (var solution in solutions)
        {
            foreach (var folder in new System.IO.FileInfo(solution.Key).Directory.GetDirectories())
            {
                foreach (var file in folder.GetFiles("*.nuspec"))
                {
	    			var path = file.Directory;
                    var assemblyInfo = ParseAssemblyInfo(path + "/Properties/AssemblyInfo.cs");
                    var nuGetPackSettings = new NuGetPackSettings()
                    {
                        OutputDirectory = buildDir,
                        IncludeReferencedProjects = false,
                        Id = assemblyInfo.Title.Replace(" ", "."),
                        Title = assemblyInfo.Title,
                        Version = version,
                        Authors = new[] { assemblyInfoVersion.Company },
                        Summary = assemblyInfo.Description,
                        Copyright = assemblyInfoVersion.Copyright,
                        Properties = new Dictionary<string, string>()
                        {{ "Configuration", configuration }}
                    };
                    NuGetPack(file.FullName, nuGetPackSettings);
                }
            }
        }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./src/**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests")
	.IsDependentOn("Build-NuGet-Packages");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);