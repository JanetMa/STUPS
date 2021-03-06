﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 11/22/2014
 * Time: 2:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Tmx.Server
{
    using System;
    using System.Linq;
    using Nancy;
    using Nancy.TinyIoc;
    using Tmx.Core;
    using Tmx.Core.Types.Remoting;
    using Tmx.Interfaces.Remoting;
    
    /// <summary>
    /// Description of TestRunInitializer.
    /// </summary>
    public class TestRunInitializer
    {
        public ITestRun CreateTestRun(ITestRunCommand testRunCommand, DynamicDictionary formData)
        {
            if (string.IsNullOrEmpty(testRunCommand.TestRunName))
                testRunCommand.TestRunName = testRunCommand.WorkflowName + " " + DateTime.Now;
            var testRun = new TestRun { Name = testRunCommand.TestRunName, Status = testRunCommand.Status };
            foreach (var key in formData) {
                addOrUpdateDataItem(testRun, formData, key);
//                testRun.Data.AddOrUpdateDataItem(
//                    new CommonDataItem {
//                        Key = key,
//                        Value = formData[key] ?? string.Empty
//                    });
            }
            setWorkflow(testRunCommand, testRun);
            setStartUpParameters(testRun);
            setCommonData(testRun, formData);
            setCreatedTime(testRun);
            return testRun;
        }
        
        void setWorkflow(ITestRunCommand testRunCommand, TestRun testRun)
        {
            (testRun as TestRun).SetWorkflow(WorkflowCollection.Workflows.First(wfl => wfl.Name == testRunCommand.WorkflowName));
            TestLabCollection.TestLabs.First(testLab => testLab.Id == testRun.TestLabId).Status = TestLabStatuses.Busy;
        }
        
        void setStartUpParameters(ITestRun testRun)
        {
            if (TestRunStartTypes.Immediately == testRun.StartType)
                testRun.Status = TestRunQueue.TestRuns.Any(tr => tr.TestLabId == testRun.TestLabId && tr.IsQueued()) ? TestRunStatuses.Pending : TestRunStatuses.Running;
            if (testRun.IsActive())
                testRun.SetStartTime();
        }
        
        void setCommonData(ITestRun testRun, DynamicDictionary formData)
        {
            if (null == formData || 0 >= formData.Count)
                return;
            foreach (var key in formData) {
                addOrUpdateDataItem(testRun, formData, key);
//                testRun.Data.AddOrUpdateDataItem(
//                    new CommonDataItem {
//                        Key = key,
//                        Value = formData[key] ?? string.Empty
//                    });
            }
        }
        
        void setCreatedTime(ITestRun testRun)
        {
            testRun.CreatedTime = DateTime.Now;
        }
        
        void addOrUpdateDataItem(ITestRun testRun, DynamicDictionary formData, string key)
        {
            testRun.Data.AddOrUpdateDataItem(
                new CommonDataItem {
                    Key = key,
                    Value = formData[key] ?? string.Empty
                });
        }
    }
}
