﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 11/19/2012
 * Time: 12:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace TLAddinUnitTests
{
    using System;
    using MbUnit.Framework;
    using PSTestLib;
    using Moq;
    //using Autofac;
    //using Autofac.Builder;
    using TMX;
    using Meyn.TestLink;
    using CookComputing.XmlRpc;
    
    using MbUnit.Framework;
    
    /// <summary>
    /// Description of ConnectTLServerTestFixture.
    /// </summary>
    [TestFixture]
    //[Parallelizable]
    public class ConnectTLServerTestFixture
    {
        public ConnectTLServerTestFixture()
        {
        }
        
        [SetUp]
        public void SetUp()
        {
            UnitTestingHelper.PrepareUnitTestDataStore();
        }
        
        [TearDown]
        public void TearDown()
        {
        }
    }
}