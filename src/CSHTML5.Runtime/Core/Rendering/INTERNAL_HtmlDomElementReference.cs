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

namespace CSHTML5.Internal
{
    // Note: this class is intented to be used by the Simulator only, not when compiled to JavaScript.
#if !BRIDGE
    [JSIgnore]
#else
    [External]
#endif
    public class INTERNAL_HtmlDomElementReference
    {
        public INTERNAL_HtmlDomElementReference(string uniqueIdentifier, INTERNAL_HtmlDomElementReference parent)
        {
            this.UniqueIdentifier = uniqueIdentifier;
            this.Parent = parent;
        }

        public string UniqueIdentifier { get; private set; }

        private INTERNAL_HtmlDomElementReference _parent;
        public INTERNAL_HtmlDomElementReference Parent
        {
            get { return _parent; }
            internal set
            {
                if (_parent != null)
                    _parent.FirstChild = null;
                _parent = value;
                if (_parent != null)
                    _parent.FirstChild = this; //what happens when we have multiple children? (is it even possible?)
            }
        }

        public INTERNAL_HtmlDomElementReference FirstChild { get; internal set; }
    }
}
