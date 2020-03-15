![logo](src/.editoricon.png)

# Microsoft.Bcl.Build.Symbols

Provides build symbols definitions so that projects referencing specific Microsoft .NET Framework can use it in code as compile definition.

[![Build status](https://ci.appveyor.com/api/projects/status/uciwrypxuvsbeuqa?svg=true)](https://ci.appveyor.com/project/ennerperez/microsoft-bcl-build-symbols)
[![NuGet](http://img.shields.io/nuget/v/Microsoft.Bcl.Build.Symbols.svg)](https://www.nuget.org/packages/Microsoft.Bcl.Build.Symbols/)

---------------------------------------

See the [changelog](CHANGELOG.md) for changes.

## More information about it

- [http://stackoverflow.com/questions/3436526/detect-target-framework-version-at-compile-time](http://stackoverflow.com/questions/3436526/detect-target-framework-version-at-compile-time)
- [https://msdn.microsoft.com/en-us/library/ms171464.aspx](https://msdn.microsoft.com/en-us/library/ms171464.aspx)

## Roadmap

- [x] .NET Core 1.0-3.1
- [x] .NET Standards 1.0-2.1
- [x] .NET Framework 4.8
- [x] .NET Framework 4.7.1-2
- [x] .NET Framework 4.6.1-2
- [x] .NET Framework 2.0 to 4.5.2
- [x] .NET Portable
- [x] .NET Profiles
- [x] Mono
- [x] Xamarin [iOS / Android]

## Table of contents

* [Implementing](#implementing)
* [Quick start](#quick-start)
* [Bugs and feature requests](#bugs-and-feature-requests)
* [Documentation](#documentation)
* [License](#license)

### Implementing

**Add the library to your project**

Add the [NuGet Package](https://www.nuget.org/packages/Microsoft.Bcl.Build.Symbols/). Right click on your project and click 'Manage NuGet Packages...'. Search for 'Bcl.Build.Symbols' and click on install. Once installed the library will be included in your project references. (Or install it through the package manager console: PM> Install-Package Microsoft.Bcl.Build.Symbols).

### Quick start

Implementing Join(string delimiter, IEnumerable strings) Prior to .NET 4.0

```
// string Join(this IEnumerable<string> strings, string delimiter)
// was not introduced until 4.0. So provide our own.
#if !NETFX_40 && NETFX_35
public static string Join( string delimiter, IEnumerable<string> strings)
{
    return string.Join(delimiter, strings.ToArray());
}
#endif
```

```
[Conditional("PORTABLE")]
public static string Join( string delimiter, IEnumerable<string> strings)
{
    return string.Join(delimiter, strings.ToArray());
}
```

### License

Code released under [The MIT License](LICENSE)