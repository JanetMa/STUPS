﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 12/19/2012
 * Time: 2:46 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace TMX
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
	using TMX.Interfaces.TestStructure;
    
    /// <summary>
    /// Description of TestSuite.
    /// </summary>
    public class TestSuite : ITestSuite
    {
        public TestSuite()
        {
            TestScenarios = new List<ITestScenario>();
            this.Statistics = new TestStat();
            this.enStatus = TestSuiteStatuses.NotTested;
            // 20140713
            this.Id = TestData.GetTestSuiteId();
            // 20140716
            addDefaultPlatform();
            // 20140713
            // addAutogeneratedTestScenario(Id);
            this.SetNow();
        }
        
        public TestSuite(string testSuiteName, string testSuiteId)
        {
            TestScenarios = new List<ITestScenario> ();
            this.Statistics = new TestStat();
            this.enStatus = TestSuiteStatuses.NotTested;
            this.Name = testSuiteName;
            this.Id = testSuiteId != string.Empty ? testSuiteId : TestData.GetTestSuiteId();
            // 20140716
            addDefaultPlatform();
            // 20140713
            // addAutogeneratedTestScenario(Id);
            this.SetNow();
        }
        
		void addAutogeneratedTestScenario(string testSuiteId)
		{
			// 20140716
			// TestScenarios.Add(new TestScenario("autogenerated", "1", testSuiteId));
			TestScenarios.Add(new TestScenario(TestData.Autogenerated, "1", testSuiteId));
		}
		
		void addDefaultPlatform()
		{
			if (TestData.TestPlatforms.All(tp => tp.Name != TestData.DefaultPlatformName))
				TestData.TestPlatforms.Add(new TestPlatform(TestData.DefaultPlatformName, TestData.GetTestPlatformId()));
			PlatformId = TestData.GetTestPlatform(TestData.DefaultPlatformName, TestData.TestPlatforms.First(tp => tp.Name == TestData.DefaultPlatformName).Id).Id;
		}
		
        //public virtual int DbId { get; protected set; }
        public virtual int DbId { get; set; }
        public string Name { get; protected internal set; }
        public string Id { get; protected internal set; }
        public virtual List<ITestScenario> TestScenarios { get; protected internal set; }
        public virtual string Description { get; set; }

        string _status;
        public virtual string Status { get { return this._status; } }
        TestSuiteStatuses _enStatus;
        protected internal TestSuiteStatuses enStatus 
        { 
            get { return _enStatus; }
            set{
				_enStatus = value;

                switch (value) {
                    case TestSuiteStatuses.Passed:
						_status = TestData.TestStatePassed;
                        break;
                    case TestSuiteStatuses.Failed:
						_status = TestData.TestStateFailed;
                        break;
                    case TestSuiteStatuses.NotTested:
						_status = TestData.TestStateNotTested;
                        break;
                    case TestSuiteStatuses.KnownIssue:
						_status = TestData.TestStateKnownIssue;
                        break;
                    default:
                        throw new Exception("Invalid value for TestSuiteStatuses");
                }
            }
        }
        
        public TestStat Statistics { get; set; }
        
        public virtual DateTime Timestamp { get; protected internal set; }
        public void SetNow()
        {
			Timestamp = System.DateTime.Now;
        }
        public virtual double TimeSpent { get; protected internal set; }
        public virtual void SetTimeSpent(double timeSpent)
        {
			TimeSpent = timeSpent;
        }
        
        public virtual string Tags { get; set; }
        public virtual string PlatformId { get; set; }
        
        public virtual ScriptBlock[] BeforeScenario { get; set; }
        public virtual ScriptBlock[] AfterScenario { get; set; }
        public virtual object[] BeforeScenarioParameters { get; set; }
        public virtual object[] AfterScenarioParameters { get; set; }
    }
}
