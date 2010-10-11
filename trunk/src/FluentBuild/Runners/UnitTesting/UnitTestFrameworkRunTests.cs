﻿using NUnit.Framework;

namespace FluentBuild.Runners.UnitTesting
{
    ///<summary />
    public class UnitTestFrameworkRunTests
    {
        ///<summary />
        public void Nunit_ShouldCreateNunitRunner()
        {
            var subject = new UnitTestFrameworkRun();
            Assert.That(subject.NUnit, Is.TypeOf(typeof (NUnitRunner)));
        }
    }
}