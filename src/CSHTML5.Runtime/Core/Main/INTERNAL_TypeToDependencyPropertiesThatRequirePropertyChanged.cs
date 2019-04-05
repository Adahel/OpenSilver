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

namespace CSHTML5.Internal
{
    internal static class INTERNAL_TypeToDependencyPropertiesThatRequirePropertyChanged
    {
        //---------------------------------------------------------------------------
        // When a UI element is added to the Visual Tree, we call "PropertyChanged"
        // on all its set properties so that the control can refresh itself.
        // However, when a property is not set, we don't call PropertyChanged.
        // Unless the property is in the list here (below).
        // To be listed here, just set "CallPropertyChangedWhenLoadedIntoVisualTree"
        // to "Always" in the "TypeMetadata" of the DependencyProperty.
        //---------------------------------------------------------------------------
        public static Dictionary<Type, List<DependencyProperty>> TypeToDependencyPropertiesThatRequirePropertyChanged = new Dictionary<Type, List<DependencyProperty>>(); //todo: use a HashSet instead of a List?

        public static void Add(Type ownerType, DependencyProperty dependencyProperty)
        {
            GetCollectionForType(ownerType).Add(dependencyProperty);
        }

        static List<DependencyProperty> GetCollectionForType(Type ownerType)
        {
            List<DependencyProperty> dependencyProperties;
            if (TypeToDependencyPropertiesThatRequirePropertyChanged.ContainsKey(ownerType))
            {
                dependencyProperties = TypeToDependencyPropertiesThatRequirePropertyChanged[ownerType];
                if (dependencyProperties == null)
                {
                    dependencyProperties = new List<DependencyProperty>();
                    TypeToDependencyPropertiesThatRequirePropertyChanged[ownerType] = dependencyProperties;
                }
            }
            else
            {
                dependencyProperties = new List<DependencyProperty>();
                TypeToDependencyPropertiesThatRequirePropertyChanged.Add(ownerType, dependencyProperties);
            }
            return dependencyProperties;
        }
    }
}
