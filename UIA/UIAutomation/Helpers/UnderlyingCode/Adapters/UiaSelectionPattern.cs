﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 12/6/2013
 * Time: 6:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace UIAutomation
{
    extern alias UIANET; extern alias UIACOM;// using System.Windows.Automation;
    using System;
    using classic = UIANET::System.Windows.Automation; using viacom = UIACOM::System.Windows.Automation; // using System.Windows.Automation;
    using System.Linq;
//    using System.Collections;
//    using System.Collections.Generic;

    public class UiaSelectionPattern : ISelectionPattern
    {
        private classic.SelectionPattern _selectionPattern;
        private IUiElement _element;
        
        public UiaSelectionPattern(IUiElement element, classic.SelectionPattern selectionPattern)
        {
            this._selectionPattern = selectionPattern;
            this._element = element;
            //this._useCache = useCache;
        }
        
        public UiaSelectionPattern(IUiElement element)
        {
            this._element = element;
        }
        
        public struct SelectionPatternInformation : ISelectionPatternInformation
        {
            private bool _useCache;
            private ISelectionPattern _selectionPattern;
            
            public SelectionPatternInformation(ISelectionPattern selectionPattern, bool useCache)
            {
                this._selectionPattern = selectionPattern;
                this._useCache = useCache;
            }
            
            public bool CanSelectMultiple {
                get {
                    return (bool)this._selectionPattern.GetParentElement().GetPatternPropertyValue(classic.SelectionPattern.CanSelectMultipleProperty, this._useCache);
                }
            }
            public bool IsSelectionRequired {
                get {
                    return (bool)this._selectionPattern.GetParentElement().GetPatternPropertyValue(classic.SelectionPattern.IsSelectionRequiredProperty, this._useCache);
                }
            }
            
            public IUiElement[] GetSelection()
            {
                // 20140302
                // AutomationElement[] nativeElements = (AutomationElement[])this._selectionPattern.GetParentElement().GetPatternPropertyValue(SelectionPattern.SelectionProperty, this._useCache);
                var nativeElements = (classic.AutomationElement[])this._selectionPattern.GetParentElement().GetPatternPropertyValue(classic.SelectionPattern.SelectionProperty, this._useCache);
                IUiEltCollection tempCollection = AutomationFactory.GetUiEltCollection(nativeElements);
                if (null == tempCollection || 0 == tempCollection.Count) {
                    return new UiElement[] {};
                } else {
                    return tempCollection.Cast<IUiElement>().ToArray<IUiElement>();
                }
            }
        }
        public static readonly classic.AutomationPattern Pattern = classic.SelectionPatternIdentifiers.Pattern;
        public static readonly classic.AutomationProperty SelectionProperty = classic.SelectionPatternIdentifiers.SelectionProperty;
        public static readonly classic.AutomationProperty CanSelectMultipleProperty = classic.SelectionPatternIdentifiers.CanSelectMultipleProperty;
        public static readonly classic.AutomationProperty IsSelectionRequiredProperty = classic.SelectionPatternIdentifiers.IsSelectionRequiredProperty;
        public static readonly classic.AutomationEvent InvalidatedEvent = classic.SelectionPatternIdentifiers.InvalidatedEvent;
        
        public virtual ISelectionPatternInformation Cached {
            get {
                return new UiaSelectionPattern.SelectionPatternInformation(this, true);
            }
        }
        
        public virtual ISelectionPatternInformation Current {
            get {
                return new UiaSelectionPattern.SelectionPatternInformation(this, false);
            }
        }
        
        public void SetParentElement(IUiElement element)
        {
            this._element = element;
        }
        
        public IUiElement GetParentElement()
        {
            return this._element;
        }
        
        public void SetSourcePattern(object pattern)
        {
            this._selectionPattern = pattern as classic.SelectionPattern;
        }
        
        public object GetSourcePattern()
        {
            return this._selectionPattern;
        }
    }
}

