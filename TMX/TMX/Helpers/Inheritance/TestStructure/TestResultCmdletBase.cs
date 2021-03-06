﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 21/02/2012
 * Time: 08:55 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Tmx
{
    using System;
    using System.Management.Automation;
    using Tmx.Interfaces.TestStructure;
    
    /// <summary>
    /// Description of TestResultCmdletBase.
    /// </summary>
    public class TestResultCmdletBase : CommonCmdletBase
    {
        public TestResultCmdletBase()
        {
            // 20130330
            this.TestResultStatus = TestResultStatuses.NotTested;
        }
        
        #region Parameters
        [Parameter(Mandatory = false,
                   Position = 0)]
        [Alias("Name")]
        public new string TestResultName { get; set; }
        
        [Parameter(Mandatory = false,
                   ParameterSetName = "EnumLogic")]
        public TestResultStatuses TestResultStatus { get; set; }
        [Parameter(Mandatory = false,
                   ParameterSetName = "DualLogic")]
        public new SwitchParameter TestPassed { get; set; }
        
        [Parameter(Mandatory = false,
                   ParameterSetName = "DualLogic")]
        public new SwitchParameter KnownIssue { get; set; }
        
        [Parameter(Mandatory = false)]
        [AllowNull]
        [AllowEmptyString]
        public string Description { get; set; }
        
        [Parameter(Mandatory = false)]
        public TestResultOrigins TestOrigin { get; set; }
        
        [Parameter(Mandatory = false)]
        internal new string Name { get; set; }
        #endregion Parameters
        
        public void ConvertTestResultStatusToTraditionalTestResult()
        {
            if (TestResultStatuses.NotTested == TestResultStatus) return;
            switch (TestResultStatus) {
                case TestResultStatuses.Passed:
                    TestPassed = true;
                    break;
                case TestResultStatuses.Failed:
                    TestPassed = false;
                    break;
                case TestResultStatuses.NotTested:
                    // nothing to do
                    // the impossible combination
                    break;
                case TestResultStatuses.KnownIssue:
                    KnownIssue = true;
                    break;
                default:
                    //throw new Exception("Invalid value for TestResultStatuses");
                    break;
            }
        }
    }
}
