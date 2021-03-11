# RightTurn.Extensions.Configuration

Provides configuration extensions for [RightTurn](https://github.com/Jandini/RightTurn)

## QuickStart

###### Configuration from a file

Use `.WithConfigurationFile()` to load optional configuration from `appsettings.json` file.

```C#
static void Main() => new Turn()
    .WithConfigurationFile()
    .Take((provider) =>
    {

    });
```

###### Customize configuration

Load configuration from `appsettings.json` or if not exists load configuration from embedded configuration file `Program.appsettings.json`. 

```C#
.WithConfiguration(() =>
{
    if (File.Exists("appsettings.json"))
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .Build();
    else 
    {
        using var stream = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), typeof(Program).Namespace)
            .GetFileInfo("Program.appsettings.json").CreateReadStream();

        return new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
    }
})
```
