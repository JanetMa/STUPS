﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 17/02/2012
 * Time: 07:01 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Tmx.Interfaces.TestStructure
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using System.Xml.Serialization;
    
    /// <summary>
    /// Description of ITestResult.
    /// </summary>
    public interface ITestResult
    {
        [XmlAttribute]
        Guid UniqueId { get; set; }
        [XmlAttribute]
        string Name { get; set; }
        [XmlAttribute]
        string Id { get; set; }
        [XmlElement("TestResultDetails", typeof(ITestResultDetail))]
        List<ITestResultDetail> Details { get; }
        [XmlAttribute]
        string Status { get; }
        [XmlAttribute]
        TestResultStatuses enStatus { get; set; }
        
        [XmlAttribute]
        string ScriptName { get; }
        void SetScriptName(string scriptName);
        [XmlAttribute]
        int LineNumber { get; }
        void SetLineNumber(int lineNumber);
        [XmlAttribute]
        int Position { get; }
        void SetPosition(int position);
        [XmlIgnore]
        ErrorRecord Error { get; }
        void SetError(ErrorRecord error);
        [XmlAttribute]
        string Code { get; set; }
        
        [XmlAttribute]
        string Description { get; set; }
        [XmlIgnore]
        List<object> Parameters { get; }
        
        [XmlIgnore]
        string ScenarioId { get; set; }
        [XmlIgnore]
        string SuiteId { get; set; }
        [XmlIgnore]
        Guid SuiteUniqueId { get; set; }
        [XmlIgnore]
        Guid ScenarioUniqueId { get; set; }
        
        [XmlAttribute]
        double TimeSpent { get; }
        void SetTimeSpent(double timeSpent);
        
        [XmlAttribute]
        DateTime Timestamp { get; }
        void SetNow();
        
        [XmlAttribute]
        bool Generated { get; }
        void SetGenerated();
        
        [XmlAttribute]
        string Screenshot { get; }
        void SetScreenshot(string path);
        
        [XmlAttribute]
        TestResultOrigins Origin { get; }
        void SetOrigin(TestResultOrigins origin);
        
        object[] ListDetailNames(TestResultStatuses status);
        
        [XmlAttribute]
        string PlatformId { get; set; }
        [XmlAttribute]
        Guid PlatformUniqueId { get; set; }
    }
}
