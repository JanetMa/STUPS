﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 4/23/2013
 * Time: 5:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace TestUtils
{
    using System;
    using System.Management.Automation;
    using TestUtils.Commands;
    
    /// <summary>
    /// Description of NewTuZipArchiveCommand.
    /// </summary>
    class NewZipArchiveCommand : Win32Command
    {
        internal NewZipArchiveCommand(CommonCmdletBase cmdlet) : base (cmdlet)
        {
        }
        
        internal override void Execute()
        {
            var cmdlet = (NewTuZipArchiveCommand)Cmdlet;
            
            ArchivingHelper.AddFilesToArchive(cmdlet);
        }
    }
}
