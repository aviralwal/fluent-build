# Convention Thoughts #

I do want to add some convention support in once we have the baseline interface built and functioning. Once people start to use it then patterns should emerge.

## Tokenization ##
```
file.ReplaceTokens.OutputFileTo(finalFile);

or

file.Copy.ReplaceTokens.To(finalFile);

```
This may use reflection to map all parameters to variables in the code.

## Build Templates ##
```
inherit Conventions.BaseDirContainsSeperateSrcAndTestFolder

```

```

Build.SourceLocatedIn("").UnitTestsLocatedIn("").IntegrationTestsLocatedIn("").Execute;

```

## Build Factory ##
```
FluentBuild.BuildFactory.Create()
                        .Application.SourceLocation("").AddDependancy("").OutputTo("")
                        .Tests.TestFilesIn("").AddDependancy("").UnitTestRunner.NUnit
                        .Execute();
```