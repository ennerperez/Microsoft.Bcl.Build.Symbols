﻿using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Microsoft.Bcl.Build.Symbols")]
[assembly: AssemblyDescription("Provides compile time symbols for .NET Framework.")]
#if DEBUG
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("RELEASE")]
#endif
[assembly: AssemblyCompany("Enner Pérez")]
[assembly: AssemblyProduct("Microsoft.Bcl.Build.Symbols")]
[assembly: AssemblyCopyright("Copyright © Enner Pérez")]
[assembly: AssemblyTrademark("Microsoft")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("A619D960-0942-47BC-B908-F940ECBFA1E1")]
[assembly: AssemblyVersion("1.1.*")]
[assembly: AssemblyFileVersion("1.1.0")]
[assembly: NeutralResourcesLanguage("en")]