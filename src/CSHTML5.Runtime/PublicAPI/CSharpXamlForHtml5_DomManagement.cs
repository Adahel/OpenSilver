﻿
//===============================================================================
//
//  IMPORTANT NOTICE, PLEASE READ CAREFULLY:
//
//  => This code is licensed under the GNU General Public License (GPL v3). A copy of the license is available at:
//        https://www.gnu.org/licenses/gpl.txt
//
//  => As stated in the license text linked above, "The GNU General Public License does not permit incorporating your program into proprietary programs". It also does not permit incorporating this code into non-GPL-licensed code (such as MIT-licensed code) in such a way that results in a non-GPL-licensed work (please refer to the license text for the precise terms).
//
//  => Licenses that permit proprietary use are available at:
//        http://www.cshtml5.com
//
//  => Copyright 2019 Userware/CSHTML5. This code is part of the CSHTML5 product (cshtml5.com).
//
//===============================================================================



#if !BRIDGE
using JSIL.Meta;
#else
using Bridge;
#endif

using CSHTML5.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if MIGRATION
using System.Windows;
#else
using Windows.UI.Xaml;
#endif

public static partial class CSharpXamlForHtml5
{
    /// <summary>
    /// This class allows the management of the Dom tree without using VerbatimExpressions
    /// </summary>
    public static partial class DomManagement
    {
        /// <summary>
        /// Checks if the UIElement is in the visual tree.
        /// </summary>
        /// <param name="control">The UIElement</param>
        /// <returns>True if the UIElement is currently in the visual tree, false otherwise.</returns>
        public static bool IsControlInVisualTree(UIElement control)
        {
            return INTERNAL_VisualTreeManager.IsElementInVisualTree(control);
        }

        //see if the comment that tells that it is the outermost element is comprehensible enough.
        /// <summary>
        /// Returns the DOM element corresponding to the UIElement.
        /// </summary>
        /// <param name="control">The UIElement</param>
        /// <returns>The DOM element corresponding to the UIElement. It is the DOM Element that contains all of the UIElement's DOM elements.</returns>
        [Obsolete]
        public static dynamic GetDomElementFromControl(UIElement control)
        {
            if (control.INTERNAL_OuterDomElement == null)
                throw new InvalidOperationException("Cannot get the DOM element because the control is not (yet?) in the visual tree. Consider waiting until the Loaded event before calling this piece of code. You can also call the 'IsControlInVisualTree' method to check if the control is in the visual tree.");

            if (CSharpXamlForHtml5.Environment.IsRunningInJavaScript)
                return control.INTERNAL_OuterDomElement;
            else
                return new CSharpXamlForHtml5.DomManagement.Types.DynamicDomElement((INTERNAL_HtmlDomElementReference)control.INTERNAL_OuterDomElement);
        }

        /// <summary>
        /// Sets the Html representation of the UIElement.
        /// </summary>
        /// <param name="control">The UIElement</param>
        /// <param name="htmlReprensentation">The string that defines the html representation of the UIElement.</param>
        public static void SetHtmlRepresentation(UIElement control, string htmlReprensentation)
        {
            control.INTERNAL_HtmlRepresentation = htmlReprensentation;
        }

#if PUBLIC_API_THAT_REQUIRES_SUPPORT_OF_DYNAMIC
        public static dynamic Document
        {
            get
            {
                return GetDocument();
            }
        }

#if !BRIDGE
        [JSReplacement("window.document")]
#else
        [Template("window.document")]
#endif
        static dynamic GetDocument()
        {
            if (_document == null)
                _document = new Types.Document();
            return _document;
        }

#if !BRIDGE
        [JSIgnore]
#else
        [External]
#endif
        static Types.Document _document;
#endif
    }
}
