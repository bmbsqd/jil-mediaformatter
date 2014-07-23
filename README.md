Jil MediaTypeFormatter
=========

Replacement of JsonMediaTypeFormatter using [JIL](https://github.com/kevin-montrose/Jil)


## Usage ##

First install the [nuget package](https://www.nuget.org/packages/Bmbsqd.JilMediaTypeFormatter/)  


Remove old JSON formatter
```csharp
config.Formatters.Remove( config.Formatters.JsonFormatter );
```

Add new JIL formatter 
```csharp
config.Formatters.Add( new JilMediaTypeFormatter() );
```

## Why? ##
JIL is fast

## Gotchas ##
[`config.Formatters.JsonFormatter`](http://msdn.microsoft.com/en-us/library/system.net.http.formatting.mediatypeformattercollection.jsonformatter(v=vs.118).aspx) is going to be `null` after you install the JIL formatter.