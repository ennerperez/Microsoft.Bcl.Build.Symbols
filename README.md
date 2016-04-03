![logo](http://go.microsoft.com/fwlink/?LinkID=288859)

# Microsoft.Bcl.Build.Symbols
Provides build symbol definitions so that projects referencing specific Microsoft Framework can use it in code as compile definition.

[![Build status](https://ci.appveyor.com/api/projects/status/uciwrypxuvsbeuqa?svg=true)](https://ci.appveyor.com/project/ennerperez/microsoft-bcl-build-symbols)

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

## License
[The MIT License (MIT)](LICENSE)