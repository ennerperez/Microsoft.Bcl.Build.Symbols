//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define solution.
var solution = "./Microsoft.Bcl.Build.Symbols.sln";

// Define AssemblyInfo source.
var assemblyInfoVersion = ParseAssemblyInfo("./Microsoft.Bcl.Build.Symbols/Properties/AssemblyInfo.cs");

// Define version.
var elapsedSpan = new TimeSpan(DateTime.Now.Ticks - new DateTime(2001, 1, 1).Ticks);
var assemblyVersion = assemblyInfoVersion.AssemblyVersion.Replace("*", elapsedSpan.Ticks.ToString().Substring(4, 4));
var version = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", assemblyVersion);

// Define directories.
var outputDirectory = "Build/" + configuration;
var buildDir = Directory("../" + outputDirectory);

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
	   foreach (var folder in new System.IO.FileInfo(solution).Directory.GetDirectories())
		  foreach (var file in folder.GetFiles("*.nuspec"))
		  {
			 var assemblyInfo = ParseAssemblyInfo(folder + "/Properties/AssemblyInfo.cs");
			 var nuGetPackSettings = new NuGetPackSettings()
			 {
			 OutputDirectory = outputDirectory,
			 IncludeReferencedProjects = true,
			 Id = assemblyInfo.Title.Replace(" ", "."),
			 Title = assemblyInfo.Title,
			 Version = version,
			 Authors = new [] {assemblyInfo.Company},
			 Summary = assemblyInfo.Description,
			 Copyright = assemblyInfo.Copyright,
			 Properties = new Dictionary<string, string>()
				{
				    { "Configuration", configuration }
				}
			 };   
			 NuGetPack(file.FullName, nuGetPackSettings);
		  }	   
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