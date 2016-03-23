# Introduction #

Fluent build allows you to write a build script right in your favorite IDE using a .NET language. This allows you to easily edit, view, refactor, reuse, and even debug your build scripts.

# First Steps #

  1. Add a new project to your solution that will contain your build script(s)
  1. Add a reference to FluentBuild.dll to the project you just created
  1. Add a class named Default (fluent build looks for a class with this name but it can be overriden)
  1. Set the Default class to inherit from FluentBuild.Core.BuildFile

# Setup #

Any application will need to setup are the folders, files, and tasks for the build to succeed. Typically you will want to create variables representing the directories involved. This is best accomplished using the BuildFolder class which simplifies dealing with folders (although you can use plain old strings if you want).

```
directory_base = new Directory(Properties.CurrentDirectory);
directory_compile = directory_base.SubFolder("compile");
directory_release = directory_base.SubFolder("release");
directory_tools = directory_base.SubFolder("tools");
```

Often you will also need to refer to files that are either used in the compilation of the assembly or as the names for outputs. This can be achieved by using the File() method on a BuildFolder which creates a new BuildArtifact:

```
thirdparty_nunit = directory_compile.File("nunit.framework.dll");
thirdparty_rhino = directory_compile.File("rhino.mocks.dll");
```

Alternatively a BuildArtifact can be created manually:

```
var output = new File("c:\\temp\\output.dll");
```

The next thing we need to do is write methods that we want to run and then let the build runner know to run them.

# Letting FluentBuild Know What To Run #

Tasks must be registered so that the build runner (fb.exe) knows what methods to run and in what order to run them. This is accomplished in the constructor of your BuildFile by calling AddTask(methodToRun). e.g.:

```
public class Default : Core.BuildFile
{
    public Default()
    {
         AddTask(Clean);
         AddTask(CompileSources);
         AddTask(CompileTests);
         AddTask(RunTests);
         AddTask(Package);
    }
}
```

Here we have added the methods that we want the build runner to execute in the order that we want them executed in. When the runner executes the task the method name will be output to the window along with any output that task generates.

# Directory Tasks #
The common tasks we want to do are delete and create directories for build outputs. This can be done right from the BuildFolder objects:

```
private void Clean()
{
    directory_compile.Delete(OnError.Continue).Create();
    directory_release.Delete(OnError.Continue).Create();
}
```

Here we have told the runtime to Delete the "compile" and "release" directories and then create them.

By default if FluentBuild can not perform a task it will throw an error and stop the build. To override this behavior some methods will reveal an override that allows you to specify the behavior of what to do on an error.

NOTE: The default failure behavior can be changed by setting the Defaults.OnError property.

# FileSets & Compilation #
NOTE: Most methods will take a string path or else a Directory/File object. Use whichever works best for you.

At a minimum compliation requires a compiler, source files, a target type and an output file name.

Source files are built using a FileSet. A FileSet is an object that allows you to include and exclude files.

```
FileSet sourceFiles = new FileSet()
			.Include("c:\\projects\\mysample\\*.cs")
			.Exclude("c:\\projects\\mysample\\AssemblyInfo.cs");
			.Include("c:\\projects\\CommonAssemblyInfo.cs");
```

In the above example we include all **.cs files, exclude the AssemblyInfo.cs file and then include a seprate CommonAssemblyInfo.cs**

Often it is required to Include all files matching a filter in the root and all subfolders.
Instead of manually specifying subfolders the shorthand of  can be used to indicate to recurse all folders:

```
FileSet sourceFiles = new FileSet().Include("c:\\projects\\mysample\\**\*.cs")
```

Once source files are established compiling is the next step:

```
Task.Build.Csc.Target.Library(compiler=>compiler.AddSources(sourceFiles).Target.Library.OutputFileTo("c:\\temp\\mysample.dll"));
```

Currently Build supports three build engines "UsingCsc" for the C# compiler and "UsingVbc" for the VB Compiler. If you need to run a different executable to compile your application then use Task.Run.Executable() to perform those actions.

Once a build engine is selected you can do many build tasks. The common ones are to AddSources (which takes a fileset), specify an output file, and select the type
of project you want to build. The targets are Executable (i.e. a console application), Library (i.e. a dll), Module (not commonly used), and WindowsExecutable.

If your project references a DLL then they will need to be included via the AddReferences method:

```
Task.Build.UsingCsc.Target.Library(compiler=>compiler.AddSources(sourceFiles).AddRefences(thirdparty_rhino, thirdparty_nunit).OutputFileTo("c:\\temp\\mysample.dll"));
```


# Doing More #
FluentBuild has some extra syntax that enables you to do common build tasks. The most common of these is running a unit test framework.
At the time of running the only currently supported framework is Nunit and can be invoked like so:

```
Task.Run.UnitTestFramework.NUnit(nunit=>nunit.FileToTest(assembly_FluentBuild.ToString()));
```

Zip support has also been added:

```
Task.Run.Zip.Compress(zip=>zip.SourceFolder("c:\\temp").UsingCompressionLevel.Six.To("myproject.zip"));
```

You can run any external file if need be as well:

```
Task.Run.Executeable(exe=>exe.ExecutablePath("c:\\sample.exe").WithArguments("/c:test"));
```

# Changing Defaults #
Currently, FluentBuild has only two defaults. The first is the FrameworkVersion which defaults to .NET 4.0. In the future the highest installed framework will be detected and set as the default. The other default is OnError which tells FluentBuild to either fail or continue if an error occurs during certain events. Some of these events include deleting/renaming/creating/moving files or folders and running executables.

```
Defaults.FrameworkVersion = FrameworkVersion.NET3_5;
Defaults.OnError = OnError.Continue;
```

# Building It #

Once you have a build file created you will need to run it using fb.exe.
```
fb.exe c:\projects\mysample\mysample.build 
```
This will compile the mysample.build folder and then run the build. If you want to avoid the compilation step or want to just distribute a build dll you can use the dll directly:
```
fb.exe c:\projects\mysample\mysample.build.dll
```
By default fb.exe will find and run a class named "Default". If you want to run a different class specify it with the -C flag:
```
fb.exe c:\projects\mysample\mysample.build -c:PublishRelease 
```
This will find and run the PublishRelease class.

It is also possible to pass in custom properties to fb.exe that can be accessed in your build script via Properties.CommandLineProperties.
```
fb.exe c:\projects\mysample\mysample.build -p:name=value -p:name2=value2
```