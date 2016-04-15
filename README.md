![logo](http://go.microsoft.com/fwlink/?LinkID=288859)

# Microsoft.Bcl.Build.Symbols
Provides build symbol definitions so that projects referencing specific Microsoft .NET Framework can use it in code as compile definition.

[![Build status](https://ci.appveyor.com/api/projects/status/uciwrypxuvsbeuqa?svg=true)](https://ci.appveyor.com/project/ennerperez/microsoft-bcl-build-symbols)
[![NuGet](http://img.shields.io/nuget/v/Microsoft.Bcl.Build.Symbols.svg)](https://www.nuget.org/packages/Microsoft.Bcl.Build.Symbols/)

---------------------------------------

See the [changelog](CHANGELOG.md) for changes.

## Usage

Implementing Join(string delimiter, IEnumerable strings) Prior to .NET 4.0

```
// string Join(this IEnumerable<string> strings, string delimiter)
// was not introduced until 4.0. So provide our own.
#if ! NETFX_40 && NETFX_35
public static string Join( string delimiter, IEnumerable<string> strings)
{
    return string.Join(delimiter, strings.ToArray());
}
#endif
```

## Implementing in your application

**Add the library to your project**

Add the [NuGet Package](https://www.nuget.org/packages/Microsoft.Bcl.Build.Symbols/). Right click on your project and click 'Manage NuGet Packages...'. Search for 'Bcl.Build.Symbols' and click on install. Once installed the library will be included in your project references. (Or install it through the package manager console: PM> Install-Package Microsoft.Bcl.Build.Symbols), then restart your solution.

## License
[The MIT License (MIT)](LICENSE)