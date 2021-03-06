﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 7/2/2013
 * Time: 3:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Tmx.Commands
{
    using System;
    using System.Management.Automation;
    using Tmx.Interfaces.TestStructure;
    
    /// <summary>
    /// Description of OpenTmxTestPlatformCommand.
    /// </summary>
    [Cmdlet(VerbsCommon.Open, "TmxTestPlatform")]
    public class OpenTmxTestPlatformCommand : PlatformCmdletBase
    {
        protected override void BeginProcessing()
        {
            CheckCmdletParameters();
            
            // temporary
            var platform = TmxHelper.GetTestPlatformById(this.Id);
            TestData.CurrentTestPlatform = platform;
            WriteObject(this, platform);
        }
    }
}
