﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 10/14/2014
 * Time: 2:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Tmx.Server //.ObjectModel.ServerControl
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Nancy.Bootstrapper;
    
    /// <summary>
    /// Description of ModulesLoader.
    /// </summary>
    // TODO: to template method
    public class ModulesLoader
    {
        string _path;
        
        public ModulesLoader(string path)
        {
            _path = path;
        }
        
        public void Load()
        {
            if (!Directory.Exists(_path)) return;
            try {
                var dir = new DirectoryInfo(_path);
                var files = dir.GetFiles(@"Nancy.ViewEngines*.dll");
                if (null == files || !files.Any()) return;
                foreach (var probablyAssembly in files) {
                    try {
                        var assembly = Assembly.LoadFrom(probablyAssembly.FullName);
                        AppDomainAssemblyTypeScanner.AddAssembliesToScan(assembly.FullName);
                    }
                    catch {}
                }
                AppDomainAssemblyTypeScanner.UpdateTypes();
            }
            catch {}
        }
    }
}
