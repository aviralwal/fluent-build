using FluentBuild.Runners.Zip;
using NUnit.Framework;

namespace FluentBuild.Core
{
    ///<summary />
    public class RunTests
    {
        ///<summary />
        public void Run_Should_Populate_Exe()
        {
            const string exe = "temp.exe";
            var executeable = Run.Executeable(exe);
            Assert.That(executeable.ExecuteablePath, Is.EqualTo(exe));
        }

        ///<summary />
        public void Run_Should_Populate_Exe_When_Using_Build_Artifact()
        {
            var exe = new BuildArtifact("temp.exe");
            var executeable = Run.Executeable(exe);
            Assert.That(executeable.ExecuteablePath, Is.EqualTo(exe.ToString()));
        }

        ///<summary />
        public void UnitTestFramework_ShouldCreateObject()
        {
            var framework = Run.UnitTestFramework;
            Assert.That(framework, Is.Not.Null);
        }

        ///<summary />
        public void Zip_ShouldCreateObject()
        {
            var zip = Run.Zip;
            Assert.That(zip, Is.TypeOf<Zip>());
        }

    }
}