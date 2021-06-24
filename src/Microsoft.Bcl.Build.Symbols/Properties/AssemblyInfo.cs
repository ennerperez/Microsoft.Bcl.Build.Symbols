using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Microsoft.Bcl.Build.Symbols")]
[assembly: AssemblyDescription("Provides compile time symbols for .NET Framework.")]
[assembly: AssemblyCompany("Enner Pérez")]
[assembly: AssemblyProduct("Microsoft.Bcl.Build.Symbols")]
[assembly: AssemblyCopyright("Copyright © Enner Pérez")]
[assembly: AssemblyTrademark("Microsoft")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
#if !NETSTANDARD_10 && !NETSTANDARD_11
[assembly: Guid("A619D960-0942-47BC-B908-F940ECBFA1E1")]
#endif
[assembly: AssemblyVersion("1.4.0.*")]
#pragma warning disable CS7035 // The specified version string does not conform to the recommended format - major.minor.build.revision
[assembly: AssemblyFileVersion("1.4.0.*")]
#pragma warning restore CS7035 // The specified version string does not conform to the recommended format - major.minor.build.revision
[assembly: NeutralResourcesLanguage("en")]