### Why Fluent Build? ###
The reasons are numerous as to why this project was created. The main reason is that the popular .NET build scripts (i.e. Nant, MSBuild) are written in XML which is never fun to work with. The other alternatives require you to learn a new language/environment which is typically to much investment to do a simple build. To that end we wrote a simple fluent interface around builds to allow developers to write build scripts in the VS editor using the language that they already know.

### Do I have to build my build script then run the compiled build file? ###
No. That would be silly. The build runner takes EITHER a pre-compiled build dll or else a path to the folder containing your build scripts. If a folder is provided it will compile the build scripts and then run them.

### Why not use NAnt? ###
  * Build scripts are written in xml which is very chatty
  * Build scripts are interpreted. Errors come back at runtime instead of during editing
  * Intellisense is possible but requires adding a schema and configuration to each developer system
  * Getting started has a learning curve
  * Hard to see where properties are used
  * Hard to refactor

### Why not use MSBuild? ###
  * Build scripts are written in xml which is very chatty
  * Build scripts are interpreted. Errors come back at runtime instead of during editing
  * Intellisense is possible but requires adding a schema and configuration to each developer system
  * It is good at building a project but doing other tasks is usually challenging
  * No support for tokenization
  * Getting started has a learning curve higher than NAnt
  * Hard to see where properties are used
  * Hard to refactor

### Why not use PSake? ###
  * Build scripts are interpreted. Errors come back at runtime instead of during editing
  * Intellisense is possible if you use a tool like Power Console
  * It requires powershell to be installed on all systems running the build script
  * Need to be familiar with powershell syntax
  * Hard to see where properties are used
  * Hard to refactor

### Is Fluent Build compatible with Cruise Control.NET? ###
> Yes and no. You can run cruise control via an 

&lt;exec&gt;

 task as you would normally run from the command line. We attempted building a full fledged plugin but found that Cruise Control.NET was a pain to work with and configure. This goes against our believe that software should be simple, concise, and easy to use so work was canceled on the plugin to focus on other features. The code is still in source control so others are welcome to pick it up and contribute a cc.net plugin back to the project.

### Is Fluent Build compatible with TeamCity? ###
> Yes. There is a plugin for TeamCity that is mostly complete. The plugin does not yet support the reporting of customized data from third party runners (i.e. unit testing, code coverage, etc.). It currently will only report what was outputted to the command line. Once that is completed we will officially release the plugin. If you want the alpha plugin feel free to download it from source control