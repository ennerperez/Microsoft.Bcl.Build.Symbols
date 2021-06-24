using System.Diagnostics;

namespace Microsoft.Bcl.Build.Symbols
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var framework = "world!";
            var csversion = "";
            var platform = "";

/* ******** */
/* PLATFORM */
/* ******** */

#if PORTABLE
            platform = "PCL";
#elif MONO
            platform = "Mono";
#elif IOS
            platform = "iOS";
#elif ANDROID
            platform = "Android";
#elif XAMARIN
            platform = "Xamarin";
#endif

/* ***************** */
/* FRAMEWORK VERSION */
/* ***************** */

#if NETFX_20
            framework = ".NET Framework 2.0";
#elif NETFX_30
            framework = ".NET Framework 3.0";
#elif NETFX_35
            framework = ".NET Framework 3.5";
#elif NETFX_40
            framework = ".NET Framework 4.0";
#elif NETFX_45
            framework = ".NET Framework 4.5";
#elif NETFX_451
            framework = ".NET Framework 4.5.1";
#elif NETFX_452
            framework = ".NET Framework 4.5.2";
#elif NETFX_46
            framework = ".NET Framework 4.6";
#elif NETFX_461
            framework = ".NET Framework 4.6.1";
#elif NETFX_462
            framework = ".NET Framework 4.6.2";
#elif NETFX_47
            framework = ".NET Framework 4.7";
#elif NETFX_471
            framework = ".NET Framework 4.7.1";
#elif NETFX_472
            framework = ".NET Framework 4.7.2";
#elif NETFX_48
            framework = ".NET Framework 4.8";
#elif NETSTANDARD_10
            framework = ".NET Standard 1.0";
#elif NETSTANDARD_11
            framework = ".NET Standard 1.1";
#elif NETSTANDARD_12
            framework = ".NET Standard 1.2";
#elif NETSTANDARD_13
            framework = ".NET Standard 1.3";
#elif NETSTANDARD_14
            framework = ".NET Standard 1.4";
#elif NETSTANDARD_15
            framework = ".NET Standard 1.5";
#elif NETSTANDARD_16
            framework = ".NET Standard 1.6";
#elif NETSTANDARD_17
            framework = ".NET Standard 1.7";
#elif NETSTANDARD_20
            framework = ".NET Standard 2.0";
#elif NETSTANDARD_21
            framework = ".NET Standard 2.1";
#elif NETCORE_10
            framework = ".NET Core 1.0";
#elif NETCORE_11
            framework = ".NET Core 1.1";
#elif NETCORE_20
            framework = ".NET Core 2.0";
#elif NETCORE_21
            framework = ".NET Core 2.1";
#elif NETCORE_22
            framework = ".NET Core 2.2";
#elif NETCORE_30
            framework = ".NET Core 3.0";
#elif NETCORE_31
            framework = ".NET Core 3.1";
#elif NET_50
            framework = ".NET 5.0";
#endif

/* ************** */
/* CSHARP VERSION */
/* ************** */

#if CSHARP_20
            csversion = "2.0";
#elif CSHARP_30
            csversion = "3.0";
#elif CSHARP_40
            csversion = "4.0";
#elif CSHARP_50
            csversion = "5.0";
#elif CSHARP_60
            csversion = "6.0";
#elif CSHARP_70
            csversion = "7.0";
#elif CSHARP_80
            csversion = "8.0";
#elif CSHARP_90
            csversion = "9.0";
#endif

            Debug.WriteLine($"Hello {framework} {platform} with C# {csversion}");
        }
    }
}