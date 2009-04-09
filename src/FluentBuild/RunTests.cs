using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FluentBuild
{
    [TestFixture]
    public class RunTests
    {
        [Test]
        public void Run_Should_Populate_Exe()
        {
            const string exe = "temp.exe";
            var executeable = Run.Executeable(exe);
            Assert.That(executeable._executeablePath, Is.EqualTo(exe));
        }

        [Test]
        public void Run_Should_Populate_Exe_When_Using_Build_Artifact()
        {
            var exe = new BuildArtifact("temp.exe");
            var executeable = Run.Executeable(exe);
            Assert.That(executeable._executeablePath, Is.EqualTo(exe.ToString()));
        }
    }
}