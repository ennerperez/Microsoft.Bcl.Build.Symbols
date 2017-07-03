//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var solution = "./Microsoft.Bcl.Build.Symbols.sln";
var nuspec = "./Microsoft.Bcl.Build.Symbols/Package.nuspec";
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var assemblyInfo = ParseAssemblyInfo("./Microsoft.Bcl.Build.Symbols/Properties/AssemblyInfo.cs");
var version = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", assemblyInfo.AssemblyFileVersion);

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("../Build/" + configuration);

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
    NuGetRestore(solution);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{

    if(IsRunningOnWindows())
    {
	  var settings = new MSBuildSettings()
	  .WithProperty("OutputPath", buildDir)
	  .WithProperty("PackageVersion", version)
	  .WithProperty("BuildSymbolsPackage", "false");
	  settings.SetConfiguration(configuration);
      // Use MSBuild
      MSBuild(solution, settings);
    }
    else
    {
	  var settings = new XBuildSettings()
	  .WithProperty("OutputPath", buildDir)
	  .WithProperty("PackageVersion", version)
	  .WithProperty("BuildSymbolsPackage", "false");
	  settings.SetConfiguration(configuration);
      // Use XBuild
      XBuild(solution, settings);
    }
});

Task("Build-NuGet-Packages")
    .IsDependentOn("Build")
    .Does(() =>
    {
	   var nuGetPackSettings = new NuGetPackSettings()
	   {
		OutputDirectory = "Build/" + configuration,
		IncludeReferencedProjects = false,
		Id = assemblyInfo.Title,
		Version = version,
		Authors = new [] {assemblyInfo.Company},
		Summary = assemblyInfo.Description,
		Copyright = assemblyInfo.Copyright,
		Properties = new Dictionary<string, string>()
		{
			{ "Configuration", configuration }
		}
	   };
	   NuGetPack(nuspec, nuGetPackSettings);			 
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("Build-NuGet-Packages");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);