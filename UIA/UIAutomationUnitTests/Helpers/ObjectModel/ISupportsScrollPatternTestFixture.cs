﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 12/22/2013
 * Time: 2:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace UIAutomationUnitTests.Helpers.ObjectModel
{
    using System.Windows.Automation;
    using UIAutomation;
    using MbUnit.Framework;using Xunit;using NUnit.Framework;
    using NSubstitute;
    
    /// <summary>
    /// Description of ISupportsScrollPatternTestFixture.
    /// </summary>
    [MbUnit.Framework.TestFixture][NUnit.Framework.TestFixture]
    public class ISupportsScrollPatternTestFixture
    {
        public ISupportsScrollPatternTestFixture()
        {
            FakeFactory.Init();
        }
        
        [MbUnit.Framework.SetUp][NUnit.Framework.SetUp]
        public void SetUp()
        {
            FakeFactory.Init();
        }
        
        [MbUnit.Framework.TearDown][NUnit.Framework.TearDown]
        public void TearDown()
        {
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_ImplementsCommonPattern()
        {
//            ISupportsInvokePattern invokableElement =
//                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
//                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsInvokePattern;
//            
//            MbUnit.Framework.Assert.IsNotNull(invokableElement as ISupportsInvokePattern);
            
            
            var highlightableElement =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsHighlighter;
            
            MbUnit.Framework.Assert.IsNotNull(highlightableElement as ISupportsHighlighter);
            Xunit.Assert.NotNull(highlightableElement as ISupportsHighlighter);
            
            var navigatableElement =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsNavigation;
            
            MbUnit.Framework.Assert.IsNotNull(navigatableElement as ISupportsNavigation);
            Xunit.Assert.NotNull(navigatableElement as ISupportsNavigation);
            
            var conversibleElement =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsConversion;
            
            MbUnit.Framework.Assert.IsNotNull(conversibleElement as ISupportsConversion);
            Xunit.Assert.NotNull(conversibleElement as ISupportsConversion);
            
            var refreshableElement =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsRefresh;
            
            MbUnit.Framework.Assert.IsNotNull(refreshableElement as ISupportsRefresh);
            Xunit.Assert.NotNull(refreshableElement as ISupportsRefresh);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_ImplementsPatternInQuestion()
        {
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsScrollPattern;
            
            MbUnit.Framework.Assert.IsNotNull(element as ISupportsScrollPattern);
            Xunit.Assert.NotNull(element as ISupportsScrollPattern);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_DoesNotImplementOtherPatterns()
        {
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsValuePattern;
            
            MbUnit.Framework.Assert.IsNull(element as ISupportsValuePattern);
            Xunit.Assert.Null(element as ISupportsValuePattern);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_Scroll()
        {
            // Arrange
            const bool expectedResult = true;
            bool result = false;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsScrollPattern;
            
            // Act
            element.Scroll(ScrollAmount.LargeIncrement, ScrollAmount.LargeIncrement);
            try {
                (element as IUiElement).GetCurrentPattern<IScrollPattern>(ScrollPattern.Pattern).Received(1).Scroll(ScrollAmount.LargeIncrement, ScrollAmount.LargeIncrement);
                result = true;
            }
            catch {}
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedResult, result);
            Xunit.Assert.Equal(expectedResult, result);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_ScrollHorizontal()
        {
            // Arrange
            const bool expectedResult = true;
            bool result = false;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsScrollPattern;
            
            // Act
            element.ScrollHorizontal(ScrollAmount.LargeIncrement);
            try {
                (element as IUiElement).GetCurrentPattern<IScrollPattern>(ScrollPattern.Pattern).Received(1).ScrollHorizontal(ScrollAmount.LargeIncrement);
                result = true;
            }
            catch {}
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedResult, result);
            Xunit.Assert.Equal(expectedResult, result);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_ScrollVertical()
        {
            // Arrange
            const bool expectedResult = true;
            bool result = false;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsScrollPattern;
            
            // Act
            element.ScrollVertical(ScrollAmount.LargeIncrement);
            try {
                (element as IUiElement).GetCurrentPattern<IScrollPattern>(ScrollPattern.Pattern).Received(1).ScrollVertical(ScrollAmount.LargeIncrement);
                result = true;
            }
            catch {}
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedResult, result);
            Xunit.Assert.Equal(expectedResult, result);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_SetScrollPercent()
        {
            // Arrange
            const bool expectedResult = true;
            bool result = false;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData()) }) as ISupportsScrollPattern;
            
            // Act
            element.SetScrollPercent(1, 1);
            try {
                (element as IUiElement).GetCurrentPattern<IScrollPattern>(ScrollPattern.Pattern).Received(1).SetScrollPercent(1, 1);
                result = true;
            }
            catch {}
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedResult, result);
            Xunit.Assert.Equal(expectedResult, result);
        }
        
                
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_HorizontallyScrollable()
        {
            // Arrange
            const bool expectedValue = true;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData() { ScrollPattern_HorizontallyScrollable = expectedValue }) }) as ISupportsScrollPattern;
            
            // Act
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedValue, element.HorizontallyScrollable);
            Xunit.Assert.Equal(expectedValue, element.HorizontallyScrollable);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_HorizontalScrollPercent()
        {
            // Arrange
            const double expectedValue = 12.1;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData() { ScrollPattern_HorizontalScrollPercent = expectedValue }) }) as ISupportsScrollPattern;
            
            // Act
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedValue, element.HorizontalScrollPercent);
            Xunit.Assert.Equal(expectedValue, element.HorizontalScrollPercent);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_HorizontalViewSize()
        {
            // Arrange
            const double expectedValue = 14.2;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData() { ScrollPattern_HorizontalViewSize = expectedValue }) }) as ISupportsScrollPattern;
            
            // Act
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedValue, element.HorizontalViewSize);
            Xunit.Assert.Equal(expectedValue, element.HorizontalViewSize);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_VerticallyScrollable()
        {
            // Arrange
            const bool expectedValue = false;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData() { ScrollPattern_VerticallyScrollable = expectedValue }) }) as ISupportsScrollPattern;
            
            // Act
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedValue, element.VerticallyScrollable);
            Xunit.Assert.Equal(expectedValue, element.VerticallyScrollable);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_VerticalScrollPercent()
        {
            // Arrange
            const double expectedValue = 16.3;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData() { ScrollPattern_VerticalScrollPercent = expectedValue }) }) as ISupportsScrollPattern;
            
            // Act
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedValue, element.VerticalScrollPercent);
            Xunit.Assert.Equal(expectedValue, element.VerticalScrollPercent);
        }
        
        [MbUnit.Framework.Test][NUnit.Framework.Test][Fact]
        public void Scroll_VerticalViewSize()
        {
            // Arrange
            const double expectedValue = 18.4;
            var element =
                FakeFactory.GetAutomationElementForMethodsOfObjectModel(
                    new IBasePattern[] { FakeFactory.GetScrollPattern(new PatternsData() { ScrollPattern_VerticalViewSize = expectedValue }) }) as ISupportsScrollPattern;
            
            // Act
            
            // Assert
            MbUnit.Framework.Assert.AreEqual(expectedValue, element.VerticalViewSize);
            Xunit.Assert.Equal(expectedValue, element.VerticalViewSize);
        }
    }
}
