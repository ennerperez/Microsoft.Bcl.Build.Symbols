//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var assemblyInfo = ParseAssemblyInfo("./Microsoft.Bcl.Build.Symbols/Properties/AssemblyInfo.cs");
var version = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", assemblyInfo.AssemblyFileVersion);

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./Microsoft.Bcl.Build.Symbols/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./Microsoft.Bcl.Build.Symbols.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{

    if(IsRunningOnWindows())
    {
	  var settings = new MSBuildSettings()
	  .WithProperty("PackageVersion", version)
	  .WithProperty("BuildSymbolsPackage", "false");
	  settings.SetConfiguration(configuration);
      // Use MSBuild
      MSBuild("./Microsoft.Bcl.Build.Symbols.sln", settings);
    }
    else
    {
	  var settings = new XBuildSettings()
	  .WithProperty("PackageVersion", version)
	  .WithProperty("BuildSymbolsPackage", "false");
	  settings.SetConfiguration(configuration);
      // Use XBuild
      XBuild("./Microsoft.Bcl.Build.Symbols.sln", settings);
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);