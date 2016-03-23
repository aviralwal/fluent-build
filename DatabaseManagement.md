**PLEASE NOTE:** This is a beta feature. There are lots of database migration tools and we recommend using them instead of this. It remains only for backwards compatibility and may be removed (or replaced) in future versions.

Managing databases and database versioning can be a pain to do manually. In fluent-build there is some rudimentary support for automating a lot of this work.

```
FluentBuild.Database.MsSqlDatabase.CreateOrUpgradeDatabase()
                .ConnectionString("server=.\\sqlExpress;initial catalog=dispatch;Integrated Security=SSPI;")
                .CreateScript(@"C:\project\database\create\CreateScript.sql")
                .UpdateScripts(@"C:\project\database\update\")
                .Execute();
```

The above code will check if the "dispatch" database exists and attempt to create it if it does not exist (NOTE: you will need permissions to create the database). Once it has created the database it will run the CreateScript specified and then create a version table so that subsequent runs know the current version of the database.

The default table name for the version table is "Version" but can be overridden using .VersionTable("") anywhere after CreateOrUpgradeDatabase()

The build will then scan through all files in the UpdateScripts folder specified and run the appropriate scripts to update the database. Files in this folder must be preceded by the version number and an underscore (i.e. 0001\_CreateLocations.sql). After a file is parsed and ran the version number in the database will be upgraded to the version number of the successful script.

Currently only MsSql databases are supported. Of course, we are open to contributions to expand this to other database systems.