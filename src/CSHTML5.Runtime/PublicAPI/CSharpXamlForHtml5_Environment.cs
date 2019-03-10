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


using CSHTML5;
using CSHTML5.Internal;
#if !BRIDGE
using JSIL.Meta;
#else
using Bridge;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !MIGRATION
using Windows.UI.Xaml;
#endif

public static partial class CSharpXamlForHtml5
{
    /// <summary>
    /// A class that allows to know the current Environnement (between Running in C# or in Javascript)
    /// </summary>
    public static partial class Environment
    {
        /// <summary>
        /// Gets a boolean saying if we are currently running in Javascript.
        /// </summary>
        public static bool IsRunningInJavaScript
        {
            get
            {
                return !INTERNAL_InteropImplementation.IsRunningInTheSimulator();
            }
        }
    }

}
