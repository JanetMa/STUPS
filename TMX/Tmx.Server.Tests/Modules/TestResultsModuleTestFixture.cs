﻿/*
 * Created by SharpDevelop.
 * User: Alexander
 * Date: 7/13/2014
 * Time: 2:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Tmx.Server.Tests.Modules
{
    using System;
    using Nancy;
    using Nancy.Testing;
    using MbUnit.Framework;
    using NUnit.Framework;
	using TMX.Interfaces.TestStructure;
    using Xunit;
    using Tmx;
    
    /// <summary>
    /// Description of TestResultsModuleTestFixture.
    /// </summary>
    [MbUnit.Framework.TestFixture][NUnit.Framework.TestFixture]
    public class TestResultsModuleTestFixture
    {
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Should_react_on_posting_a_test_suite()
        {
            var browser = new Browser(new DefaultNancyBootstrapper());
            
            var response = browser.Post("/Results/suites/");
            
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Should_create_a_test_suite()
        {
            var browser = new Browser(new DefaultNancyBootstrapper());
            var testSuiteNameExpected = "test suite name";
            var testSuiteIdExpected = "111";
            var testSuite = new TMX.TestSuite(testSuiteNameExpected, testSuiteIdExpected);
//            var response = browser.Post("/Results/suites/", (with) => {
//                                        	with.JsonBody<ITestSuite>(testSuite);
//                                    }
//                                   );
        }
    }
}
