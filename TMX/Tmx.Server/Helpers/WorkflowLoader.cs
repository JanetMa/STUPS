﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 7/24/2014
 * Time: 5:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Tmx.Server
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Tmx.Interfaces.Exceptions;
    using Tmx.Core.Types.Remoting;
    using Tmx.Interfaces.Remoting;
    using Tmx.Interfaces.Server;
    using Tmx.Server.Helpers;
    
    /// <summary>
    /// Description of WorkflowLoader.
    /// </summary>
    public class WorkflowLoader
    {
        const string taskElement_id = "id";
        const string taskElement_afterTask = "afterTask";
        const string taskElement_isActive = "isActive";
        const string taskElement_isCritical = "isCritical";
        const string taskElement_name = "name";
        const string taskElement_rule = "rule";
        const string taskElement_storyId = "storyId";
        const string taskElement_taskType = "taskType";
        const string taskElement_taskRuntimeType = "taskRuntimeType";
        const string taskElement_timeLimit = "timelimit";
        const string taskElement_retryCount = "retryCount";
        
        const string taskElement_action = "action";
        const string taskElement_beforeAction = "beforeAction";
        const string taskElement_afterAction = "afterAction";
        const string taskElement_code = "code";
        const string taskElement_parameters = "parameters";

        /// <exception cref="WorkflowLoadingException">Server failed to load the workflow. </exception>
        public virtual bool Load(string pathToWorkflowFile)
        {
            try {
                if (!System.IO.File.Exists(pathToWorkflowFile))
                    throw new WorkflowLoadingException("There is no such file '" + pathToWorkflowFile + "'.");
                ImportXdocument(XDocument.Load(pathToWorkflowFile));
            }
            catch (Exception eImportDocument) {
                // TODO: AOP
                Trace.TraceError("Load(string pathToWorkflowFile)");
                throw new WorkflowLoadingException(
                    "Unable to load an XML workflow from the file '" +
                    pathToWorkflowFile +
                    "'. " + 
                    eImportDocument.Message, eImportDocument);
            }
            return true;
        }
        
        public virtual void Reload(string pathToWorkflowFile)
        {
            // TODO: write code
        }
        
        public virtual Guid ImportXdocument(XContainer xDocument)
        {
            var workflowId = getWorkflowId(xDocument);
            
            setParametersPageName(workflowId, xDocument);
            
            var tasks = from task in xDocument.Descendants(WorkflowXmlConstants.TaskNode)
                        where task.Element(taskElement_isActive).Value == "1"
                        select task;
            var importedTasks = tasks.Select(tsk => getNewTestTask(tsk, workflowId));
            addTasksToCommonPool(importedTasks);
            return workflowId;
        }
        
        Guid getWorkflowId(XContainer xDocument)
        {
            var workflow = xDocument.Descendants(WorkflowXmlConstants.WorkflowNode).FirstOrDefault();
            if (null == workflow)
                throw new WorkflowLoadingException("There's no workflow element in the document");
            var nameAttribute = workflow.Attribute(WorkflowXmlConstants.NameAttribute);
            var workflowName = null != nameAttribute ? nameAttribute.Value : "unnamed workflow";
            return addWorkflow(workflowName, xDocument);
        }
        
        Guid addWorkflow(string name, XContainer xDocument)
        {
            var testLabName = xDocument.Descendants(WorkflowXmlConstants.TestLabNode).FirstOrDefault().Value;
            
            var testLab = string.IsNullOrEmpty(testLabName) ? 
                getFirstTestLab() :
                getOrCreateTestLab(testLabName);
            
            var workflow = new TestWorkflow(testLab) { Name = name };
            WorkflowCollection.AddWorkflow(workflow);
            return workflow.Id;
        }
        
        ITestLab getFirstTestLab()
        {
            return TestLabCollection.TestLabs.First();
        }
        
        ITestLab getOrCreateTestLab(string testLabName)
        {
            var testLab = TestLabCollection.TestLabs.FirstOrDefault(tl => tl.Name.ToLower() == testLabName.ToLower());
            if (null != testLab)
                return testLab;
            testLab = new TestLab { Name = testLabName };
            TestLabCollection.TestLabs.Add(testLab);
            return testLab;
        }
        
        void setParametersPageName(Guid workflowId, XContainer xDocument)
        {
            // 20150115
            // WorkflowCollection.Workflows.FirstOrDefault(wfl => wfl.Id == workflowId).ParametersPageName = xDocument.Descendants(WorkflowXmlConstants.ParametersPageNode).FirstOrDefault().Value;
            
//            var parameterspageName = xDocument.Descendants(WorkflowXmlConstants.ParametersPageNode).FirstOrDefault().Value;
//            Trace.TraceInformation("is null or empty? {0}", string.IsNullOrEmpty(parameterspageName));
//            Trace.TraceInformation("does start with dot? {0}", parameterspageName.Substring(0, 1) == ".");
//            Trace.TraceInformation("does file exist? {0}", File.Exists(new TmxServerRootPathProvider().GetRootPath() + "/workflows/" + parameterspageName + ".html"));
            
            var parameterspageName = xDocument.Descendants(WorkflowXmlConstants.ParametersPageNode).FirstOrDefault().Value;
            if (string.IsNullOrEmpty(parameterspageName) || parameterspageName.Substring(0, 1) == "." || !File.Exists(new TmxServerRootPathProvider().GetRootPath() + "/workflows/" + parameterspageName + ".html"))
                parameterspageName = UrlList.ViewTestWorkflowParameters_DefaultPage;
            WorkflowCollection.Workflows.FirstOrDefault(wfl => wfl.Id == workflowId).ParametersPageName = parameterspageName;
        }
        
        internal virtual void addTasksToCommonPool(IEnumerable<ITestTask> importedTasks)
        {
            TaskPool.Tasks.AddRange(importedTasks);
        }
        
        internal virtual ITestTask getNewTestTask(XContainer taskNode, Guid workflowId)
        {
            return new TestTask {
                Action = getActionCode(taskNode, taskElement_action),
                ActionParameters = getActionParameters(taskNode, taskElement_action),
                AfterAction = getActionCode(taskNode, taskElement_afterAction),
                AfterActionParameters = getActionParameters(taskNode, taskElement_afterAction),
                BeforeAction = getActionCode(taskNode, taskElement_beforeAction),
                BeforeActionParameters = getActionParameters(taskNode, taskElement_beforeAction),
                TaskFinished = false,
                // ExpectedResult
                Id = convertTestTaskElementValue(taskNode, taskElement_id),
                AfterTask = convertTestTaskElementValue(taskNode, taskElement_afterTask),
                IsActive = "1" == getTestTaskElementValue(taskNode, taskElement_isActive),
                IsCritical = "1" == getTestTaskElementValue(taskNode, taskElement_isCritical),
                Name = getTestTaskElementValue(taskNode, taskElement_name),
                // PreviousTaskId
                RetryCount = convertTestTaskElementValue(taskNode, taskElement_retryCount),
                Rule = getTestTaskElementValue(taskNode, taskElement_rule),
                TaskStatus = TestTaskStatuses.New,
                StoryId = getTestTaskElementValue(taskNode, taskElement_storyId),
                // TaskResult
                TaskType = getTestTaskType(getTestTaskElementValue(taskNode, taskElement_taskType)),
                TaskRuntimeType = getTaskRuntimeType(getTestTaskElementValue(taskNode, taskElement_taskRuntimeType)),
                TimeLimit = convertTestTaskElementValue(taskNode, taskElement_timeLimit),
                WorkflowId = workflowId
            };
        }
        
        internal virtual string getActionCode(XContainer taskNode, string elementName)
        {
            var actionNode = taskNode.Element(elementName);
            return getTestTaskElementValue(actionNode, taskElement_code);
        }
        
        internal virtual IDictionary<string, string> getActionParameters(XContainer taskNode, string elementName)
        {
            var resultDict = new Dictionary<string, string>();
            var nodeParameters = taskNode.Element(elementName);
            try {
                nodeParameters = nodeParameters.Element(taskElement_parameters);
                if (null == nodeParameters) return resultDict;
                foreach (var parameterNode in nodeParameters.Elements())
                    resultDict.Add(parameterNode.Name.LocalName, parameterNode.Value.ToString());
            }
            catch {}
            return resultDict;
        }
        
        internal virtual int convertTestTaskElementValue(XContainer taskNode, string elementName)
        {
            return Convert.ToInt32(string.Empty == getTestTaskElementValue(taskNode, elementName) ? "0" : getTestTaskElementValue(taskNode, elementName));
        }
        
        internal virtual string getTestTaskElementValue(XContainer taskNode, string elementName)
        {
            try {
                return taskNode.Element(elementName).Value ?? string.Empty;
            }
            catch {
                // TODO: AOP
                Trace.TraceInformation("getTestTaskElementValue(XContainer taskNode, string elementName)");
                // throw new WrongTaskDataException("failed to read the value of element '" + elementName + "'");
                return string.Empty;
            }
        }

        /// <exception cref="WrongTaskDataException">Failed to read the taskType element</exception>
        internal virtual TestTaskExecutionTypes getTestTaskType(string taskTypeStringValue)
        {
            switch (taskTypeStringValue.ToUpper()) {
                case "RDP":
                    return TestTaskExecutionTypes.RemoteApp;
                case "PSREMOTING":
                    return TestTaskExecutionTypes.PsRemoting;
                case "INLINE":
                case "":
                    return TestTaskExecutionTypes.Inline;
                case "SSH":
                    return TestTaskExecutionTypes.Ssh;
                default:
                    throw new WrongTaskDataException("Failed to read the taskType element");
            }
        }
        
        internal virtual TestTaskRuntimeTypes getTaskRuntimeType(string taskRuntimeTypeStringValue)
        {
            switch (taskRuntimeTypeStringValue.ToUpper()) {
                case "POWERSHELL":
                    return TestTaskRuntimeTypes.Powershell;
                case "NUNIT":
                    return TestTaskRuntimeTypes.Nunit;
                case "XUNIT":
                    return TestTaskRuntimeTypes.Xunit;
                case "MBUNIT":
                    return TestTaskRuntimeTypes.Mbunit;
                default:
                    // 20141211
                    // temporary
                    // TODO: change the behavior
                    return TestTaskRuntimeTypes.Powershell;
            }
        }
    }
}