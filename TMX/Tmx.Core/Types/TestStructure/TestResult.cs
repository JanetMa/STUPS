﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 12/19/2012
 * Time: 2:53 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Tmx.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;
    using System.Xml.Serialization;
    using Tmx.Interfaces.TestStructure;
    // using Tmx.Core;
    
    /// <summary>
    /// Description of TestResult.
    /// </summary>
    public class TestResult : ITestResult
    {
        public TestResult() {
            UniqueId = Guid.NewGuid();
            Details = new List<ITestResultDetail>();
        }
        
        public TestResult(
           string testScenarioId,
           string testSuiteId)
        {
            UniqueId = Guid.NewGuid();
            this.Details = new List<ITestResultDetail>();
            this.enStatus = TestResultStatuses.NotTested;
            
            // scenarioId and suiteId
            this.ScenarioId = testScenarioId;
            this.SuiteId = testSuiteId;
            
            this.SetNow();
        }
        
        [XmlAttribute]
        public virtual Guid UniqueId { get; set; }
        [XmlAttribute]
        public virtual string Name { get; set; }
        [XmlAttribute]
        public virtual string Id { get; set; }
        [XmlAttribute]
        public virtual string Description { get; set; }
        // [XmlInclude(typeof(List<ITestResultDetail>))]
        [XmlElement("TestResultDetails", typeof(ITestResultDetail))]
        public virtual List<ITestResultDetail> Details { get; protected internal set; }
        // public virtual List<ITestResultDetail> Details { get; set; }
        
        [XmlAttribute]
        public virtual string ScriptName { get; protected internal set; }
        public virtual void SetScriptName(string scriptName)
        {
            ScriptName = scriptName;
        }
        [XmlAttribute]
        public virtual int LineNumber { get; protected internal set; }
        public virtual void SetLineNumber(int lineNumber)
        {
            LineNumber = lineNumber;
        }
        [XmlAttribute]
        public virtual int Position { get; protected internal set; }
        public virtual void SetPosition(int position)
        {
            Position = position;
        }
        [XmlIgnore]
        // 20150116
        // public virtual ErrorRecord Error { get; protected internal set; }
        // public virtual void SetError(ErrorRecord error)
        public virtual Exception Error { get; protected internal set; }
        public virtual void SetError(Exception error)
        {
            Error = error;
        }
        [XmlAttribute]
        public virtual string Code { get; set; }
        
        [XmlIgnore]
        public virtual List<object> Parameters { get; protected internal set; }

        string status;
        [XmlAttribute]
        public virtual string Status { get { return this.status; } }
        TestResultStatuses _enStatus;
        [XmlAttribute]
        public virtual TestResultStatuses enStatus
        { 
            get { return _enStatus; }
            set{
                _enStatus = value;
                
                switch (value) {
                    case TestResultStatuses.Passed:
                        status = TestData.TestStatePassed;
                        break;
                    case TestResultStatuses.Failed:
                        status = TestData.TestStateFailed;
                        break;
                    case TestResultStatuses.NotTested:
                        status = TestData.TestStateNotTested;
                        break;
                    case TestResultStatuses.KnownIssue:
                        status = TestData.TestStateKnownIssue;
                        break;
                    default:
                        throw new Exception("Invalid value for TestResultStatuses");
                }
            }
        }
        
        [XmlAttribute]
        public virtual double TimeSpent { get; protected internal set; }
        public virtual void SetTimeSpent(double timeSpent)
        {
            TimeSpent = timeSpent;
        }
        
        [XmlAttribute]
        public virtual DateTime Timestamp { get; protected internal set; }
        public virtual void SetNow()
        {
            Timestamp = DateTime.Now;
        }
        // TODO: DI
        public virtual void SetTimestamp(DateTime timestamp)
        {
            Timestamp = timestamp;
        }
        
        [XmlIgnore]
        public virtual string SuiteId { get; set; }
        [XmlIgnore]
        public virtual Guid SuiteUniqueId { get; set; }
        [XmlIgnore]
        public virtual string ScenarioId { get; set; }
        [XmlIgnore]
        public virtual Guid ScenarioUniqueId { get; set; }
        [XmlAttribute] // ??
        public virtual bool Generated { get; protected internal set; }
        public virtual void SetGenerated()
        {
            Generated = true;
        }
        
        [XmlAttribute]
        public virtual string Screenshot { get; protected internal set; }
        public virtual void SetScreenshot(string path)
        {
            Screenshot = path;
        }
        
        [XmlAttribute]
        public virtual TestResultOrigins Origin { get; protected internal set; }
        public virtual void SetOrigin(TestResultOrigins origin)
        {
            if (TestResultOrigins.Logical == this.Origin) {
                
                // don't change the origin - it already was logical
            } else {
                
                Origin = origin;
            }
        }
        
//       public virtual TestResultOrigins Origin
//       {
//           get { return Origin; }
//           set {
//                if (TestResultOrigins.Logical != value)
//                    Origin = value;
//           }
//       }
        
        // 20140703
        // refactoring
//        public virtual object[] ListDetailNames(TestResultStatusCmdletBase cmdlet)
//        {
//            //ArrayList detailsList =
//            //    new ArrayList();
//            
//            // 20130402
//            ITestResultDetail[] detailsList = null;
//            
//            cmdlet.WriteVerbose(cmdlet, "trying to enumerate details");
//            
//            // 20140208
//            // if (null == this.Details || 0 >= this.Details.Count) return detailsList;
//            if (null == this.Details || 0 == this.Details.Count) return detailsList;
//            // if (null != this.Details && 0 < this.Details.Count) {
//
//            // 20130402
//            //if (null == cmdlet.TestResultStatus) {
//            if (TestResultStatuses.NotTested == cmdlet.TestResultStatus) {
//                    
//                var testResultDetailsNonFiltered = 
//                    from detail in this.Details
//                    select detail;
//                    
//                try {
//                    detailsList = testResultDetailsNonFiltered.ToArray();
//                }
//                catch {}
//                    
//            } else {
//                    
//                var testResultDetailFiltered =
//                    from detail in this.Details
//                    where detail.DetailStatus == TestResultStatuses.Failed || detail.DetailStatus == TestResultStatuses.KnownIssue
//                    select detail;
//                    
//                try {
//                    detailsList = testResultDetailFiltered.ToArray();
//                }
//                catch {}
//                    
//            }
//                
////                foreach (TestResultDetail detail in this.Details) {
////                    
////                    detailsList.Add(detail.Name);
////                }
//            
//            return detailsList;
//        }
        
        public virtual object[] ListDetailNames(TestResultStatuses status)
        {
            ITestResultDetail[] detailsList = null;
            
            if (null == this.Details || 0 == this.Details.Count) return detailsList;
            
            if (TestResultStatuses.NotTested == status) {
                    
                var testResultDetailsNonFiltered = 
                    from detail in this.Details
                    select detail;
                    
                try {
                    detailsList = testResultDetailsNonFiltered.ToArray();
                }
                catch {}
                    
            } else {
                    
                var testResultDetailFiltered =
                    from detail in this.Details
                    where detail.DetailStatus == TestResultStatuses.Failed || detail.DetailStatus == TestResultStatuses.KnownIssue
                    select detail;
                    
                try {
                    detailsList = testResultDetailFiltered.ToArray();
                }
                catch {}
                    
            }
                
//                foreach (TestResultDetail detail in this.Details) {
//                    
//                    detailsList.Add(detail.Name);
//                }
            
            return detailsList;
        }
        
        [XmlAttribute]
        public virtual string PlatformId { get; set; }
        [XmlAttribute]
        public virtual Guid PlatformUniqueId { get; set; }
    }
}
