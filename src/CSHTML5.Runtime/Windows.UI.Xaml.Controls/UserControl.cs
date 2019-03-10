﻿
//===============================================================================
//
//  IMPORTANT NOTICE, PLEASE READ CAREFULLY:
//
//  ● This code is dual-licensed (GPLv3 + Commercial). Commercial licenses can be obtained from: http://cshtml5.com
//
//  ● You are NOT allowed to:
//       – Use this code in a proprietary or closed-source project (unless you have obtained a commercial license)
//       – Mix this code with non-GPL-licensed code (such as MIT-licensed code), or distribute it under a different license
//       – Remove or modify this notice
//
//  ● Copyright 2019 Userware/CSHTML5. This code is part of the CSHTML5 product.
//
//===============================================================================


using CSHTML5.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
#if !FOR_DESIGN_TIME && !MIGRATION
using Windows.UI.Xaml.Markup;
#endif

#if MIGRATION
namespace System.Windows.Controls
#else
namespace Windows.UI.Xaml.Controls
#endif
{
    /// <summary>
    /// Provides the base class for defining a new control that encapsulates related
    /// existing controls and provides its own logic.
    /// </summary>
    [ContentProperty("Content")]
    public class UserControl : Control, INameScope
    {
        public UserControl()
        {
            IsTabStop = false; //we want to avoid stopping on this element's div when pressing tab.
        }

        /// <summary>
        /// Gets or sets the content that is contained within a user control.
        /// </summary>
        public UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        /// <summary>
        /// Identifies the Content dependency property
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(UIElement), typeof(UserControl), new PropertyMetadata(null, Content_Changed));

        static void Content_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement parent = (UIElement)d;
            UIElement oldChild = (UIElement)e.OldValue;
            UIElement newChild = (UIElement)e.NewValue;
            INTERNAL_VisualTreeManager.DetachVisualChildIfNotNull(oldChild, parent);
            INTERNAL_VisualTreeManager.AttachVisualChildIfNotAlreadyAttached(newChild, parent);
        }

        //protected virtual void InitializeComponent()
        //{
        //}

        #region ---------- INameScope implementation ----------

        Dictionary<string, object> _nameScopeDictionary = new Dictionary<string,object>();

        /// <summary>
        /// Finds the UIElement with the specified name.
        /// </summary>
        /// <param name="name">The name to look for.</param>
        /// <returns>The object with the specified name if any; otherwise null.</returns>
        public new object FindName(string name)
        {
            if (_nameScopeDictionary.ContainsKey(name))
                return _nameScopeDictionary[name];
            else
                return null;
        }

        public void RegisterName(string name, object scopedElement)
        {
            if (_nameScopeDictionary.ContainsKey(name) && _nameScopeDictionary[name] != scopedElement)
                throw new ArgumentException(string.Format("Cannot register duplicate name '{0}' in this scope.", name));

            _nameScopeDictionary[name] = scopedElement;
        }

        public void UnregisterName(string name)
        {
            if (!_nameScopeDictionary.ContainsKey(name))
                throw new ArgumentException(string.Format("Name '{0}' was not found.", name));

            _nameScopeDictionary.Remove(name);
        }

        #endregion
    }
}
