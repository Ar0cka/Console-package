## Console Package

# Installation
To install the package, open Unity → Package Manager → Add package from git URL and paste the following link:
```bash
https://github.com/Ar0cka/Console-package.git?path=ConsolePackage#v0.2.1
```

After installation, go to the Samples tab in the Package Manager and import the provided assets.

# Usage
Console Interface

On the Console object inside the Canvas/ConsoleInterface, you can set a custom path for saving logs.
If no path is specified, logs will be stored in your MyDocuments folder by default.

The console can be opened and closed by pressing the ~ (tilde) key.

# Logging
To log messages into the console, use the ConsoleLogger with one of the supported log types:
- Log
- Warning
- Error
- Command

Example:
```C#
ConsoleLogger.Log("Hello from console!");
ConsoleLogger.Warning("This is a warning");
ConsoleLogger.Error("Something went wrong");
```

# Commands

To register all available console commands, you need to provide your assemblies to the ConsoleRegister.
This should be done on startup (or at the end of initialization if you are using a bootstrap system):
```C#
CommandRegister.Register(typeof(YourClassInit).Assembly);
```
To create a custom console command, simply add the attribute [ConsoleCommand("your_command_name")] above the method you want to expose:
```C#
[ConsoleCommand("say_hello")]
public static void SayHello()
{
    ConsoleLogger.Log("Hello from command!");
}
```

## Future Plans:
Visual improvements
Adding an Editor version of the console
Adding server interaction
Improving the console file system

## Possible improvements:
Porting the console to pure C# with a custom renderer

