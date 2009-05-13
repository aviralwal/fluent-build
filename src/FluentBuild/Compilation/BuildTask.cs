using System;
using System.Collections.Generic;
using System.Text;

namespace FluentBuild
{
    public class BuildTask
    {
        private readonly List<string> _references = new List<string>();
        private readonly List<string> _sources = new List<string>();
        internal readonly string compiler;
        private bool _includeDebugSymbols;
        private string _outputFileLocation;
        private Target _target;

        public BuildTask() : this("")
        {
        }

        protected internal BuildTask(string compiler)
        {
            _target = new Target(this);
            this.compiler = compiler;
        }

        public Target Target
        {
            get { return _target; }
            set { _target = value; }
        }

        protected internal string TargetType { get; set; }

        public BuildTask IncludeDebugSymbols
        {
            get
            {
                _includeDebugSymbols = true;
                return this;
            }
        }

        public BuildTask OutputFileTo(string outputFileLocation)
        {
            _outputFileLocation = outputFileLocation;
            return this;
        }

        public BuildTask OutputFileTo(BuildArtifact artifact)
        {
            return OutputFileTo(artifact.ToString());
        }

        public BuildTask AddRefences(params string[] fileNames)
        {
            _references.AddRange(fileNames);
            return this;
        }

        public BuildTask AddRefences(params BuildArtifact[] artifact)
        {
            foreach (BuildArtifact buildArtifact in artifact)
            {
                _references.Add(buildArtifact.ToString());
            }
            return this;
        }

        public void Execute()
        {
            string compilerWithoutExtentions = compiler.Substring(0, compiler.IndexOf("."));
            MessageLogger.Write(compilerWithoutExtentions, String.Format("Compiling {0} files to '{1}'", _sources.Count, _outputFileLocation));
            MessageLogger.WriteDebugMessage("Compile Using: " + @"c:\Windows\Microsoft.NET\Framework\" + FrameworkVersion.frameworkVersion + "\\" + compiler + " " + Args);
            Run.Executeable(@"c:\Windows\Microsoft.NET\Framework\" + FrameworkVersion.frameworkVersion + "\\" + compiler).WithArguments(Args).Execute(compilerWithoutExtentions);
            MessageLogger.WriteDebugMessage("Done Compiling");
        }

        internal string Args
        {
            get
            {
                var sources = new StringBuilder();
                foreach (string source in _sources)
                {
                    sources.Append(" \"" + source + "\"");
                }

                var references = new StringBuilder();
                foreach (string reference in _references)
                {
                    references.Append(" /reference:");
                    references.Append("\"" + reference + "\"");
                }

                string args = String.Format("/out:\"{1}\" /target:{2} {3} {0}", sources, _outputFileLocation, "library", references);
                if (_includeDebugSymbols)
                    args += " /debug";
                return args;
            }
        }

        public BuildTask AddSources(FileSet fileset)
        {
            _sources.AddRange(fileset.Files);
            return this;
        }
    }
}