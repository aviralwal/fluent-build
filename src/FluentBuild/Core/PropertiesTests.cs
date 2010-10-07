﻿using System;
using FluentBuild.ApplicationProperties;
using NUnit.Framework;

namespace FluentBuild.Core
{
    ///<summary />
    public class PropertiesTests
    {
        ///<summary />
        public void TeamCityShouldReturnProperObject()
        {
            Assert.That(Properties.TeamCity, Is.TypeOf<TeamCityProperties>());
        }

        ///<summary />
        public void CruiseControlShouldReturnProperObject()
        {
            Assert.That(Properties.CruiseControl, Is.TypeOf<CruiseControlProperties>());
        }

        ///<summary />
        public void CommandLineShouldReturnProperObject()
        {
            Assert.That(Properties.CommandLineProperties, Is.TypeOf<CommandLineProperties>());
        }

        ///<summary />
        public void CurrentDirectoryShouldBeProper()
        {
            Assert.That(Properties.CurrentDirectory, Is.EqualTo(Environment.CurrentDirectory));
        }
    }
}