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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHTML5.Internal;

public static partial class CSharpXamlForHtml5
{
    public static partial class DomManagement
    {
        public static partial class Types
        {
#if !BRIDGE
            [JSIgnore]
#else
            [External]
#endif
            public class DynamicDomElementChildrenCollection
            {
                INTERNAL_HtmlDomElementReference _parentDomElementRef;

                internal DynamicDomElementChildrenCollection(INTERNAL_HtmlDomElementReference parentDomElementRef)
                {
                    _parentDomElementRef = parentDomElementRef;
                }

                public DynamicDomElement this[int index]
                {
                    get
                    {
                        return new DynamicDomElement(INTERNAL_HtmlDomManager.GetChildDomElementAt(_parentDomElementRef, index));
                    }
                }
            }

        }
    }
}
