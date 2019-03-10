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
#if !MIGRATION
using Windows.Foundation;
#endif

#if MIGRATION
namespace System.Windows.Media
#else
namespace Windows.UI.Xaml.Media
#endif
{
    /// <summary>
    /// Represents a two-dimensional skew.
    /// </summary>
    public sealed class SkewTransform : Transform
    {

        /// <summary>
        /// Gets or sets the x-axis skew angle, which is measured in degrees counterclockwise
        /// from the y-axis.
        /// </summary>
        public double AngleX
        {
            get { return (double)GetValue(AngleXProperty); }
            set { SetValue(AngleXProperty, value); }
        }

        /// <summary>
        /// Identifies the AngleX dependency property.
        /// </summary>
        public static readonly DependencyProperty AngleXProperty =
            DependencyProperty.Register("AngleX", typeof(double), typeof(SkewTransform), new PropertyMetadata(0d)
            {
                GetCSSEquivalent = (instance) =>
                {
                    if (((SkewTransform)instance).INTERNAL_parent != null)
                    {
                        return new CSSEquivalent()
                        {
                            DomElement = ((SkewTransform)instance).INTERNAL_parent.INTERNAL_OuterDomElement,
                            Value = (inst, value) =>
                            {
                                return value + "deg";
                            },
                            Name = new List<string> { "skewX" }, //Note: the css use would be: transform = "scaleX(2)" but the velocity call must use: scaleX : 2
                            UIElement = ((SkewTransform)instance).INTERNAL_parent,
                            ApplyAlsoWhenThereIsAControlTemplate = true,
                            OnlyUseVelocity = true
                        };
                    }
                    return null;
                }
            });


        /// <summary>
        /// Gets or sets the y-axis skew angle, which is measured in degrees counterclockwise
        /// from the x-axis.
        /// </summary>
        public double AngleY
        {
            get { return (double)GetValue(AngleYProperty); }
            set { SetValue(AngleYProperty, value); }
        }

        /// <summary>
        /// Identifies the AngleY dependency property.
        /// </summary>
        public static readonly DependencyProperty AngleYProperty =
            DependencyProperty.Register("AngleY", typeof(double), typeof(SkewTransform), new PropertyMetadata(0d)
            {
                GetCSSEquivalent = (instance) =>
                {
                    if (((SkewTransform)instance).INTERNAL_parent != null)
                    {
                        return new CSSEquivalent()
                        {
                            DomElement = ((SkewTransform)instance).INTERNAL_parent.INTERNAL_OuterDomElement,
                            Value = (inst, value) =>
                            {
                                return value + "deg";
                            },
                            Name = new List<string> { "skewY" }, //Note: the css use would be: transform = "scaleX(2)" but the velocity call must use: scaleX : 2
                            UIElement = ((SkewTransform)instance).INTERNAL_parent,
                            ApplyAlsoWhenThereIsAControlTemplate = true,
                            OnlyUseVelocity = true
                        };
                    }
                    return null;
                }
            });


        internal void ApplySkewTransform(double angleX, double angleY)
        {
            if (this.INTERNAL_parent != null && INTERNAL_VisualTreeManager.IsElementInVisualTree(this.INTERNAL_parent))
            {
                object parentDom = this.INTERNAL_parent.INTERNAL_OuterDomElement;
                CSSEquivalent angleXcssEquivalent = AngleXProperty.GetTypeMetaData(typeof(SkewTransform)).GetCSSEquivalent(this);
                INTERNAL_HtmlDomManager.SetDomElementStylePropertyUsingVelocity(angleXcssEquivalent.DomElement, angleXcssEquivalent.Name, angleXcssEquivalent.Value(this, angleX));
                CSSEquivalent angleYcssEquivalent = AngleYProperty.GetTypeMetaData(typeof(SkewTransform)).GetCSSEquivalent(this);
                INTERNAL_HtmlDomManager.SetDomElementStylePropertyUsingVelocity(angleYcssEquivalent.DomElement, angleYcssEquivalent.Name, angleYcssEquivalent.Value(this, angleY));

                //dynamic domStyle = INTERNAL_HtmlDomManager.GetFrameworkElementOuterStyleForModification(this.INTERNAL_parent);

                //string value = "skew(" + angleX + "deg, " + angleY + "deg)"; //todo: make sure that the conversion from double to string is culture-invariant so that it uses dots instead of commas for the decimal separator.
                //try
                //{
                //    domStyle.transform = value;
                //}
                //catch
                //{
                //}
                //try
                //{
                //    domStyle.msTransform = value;
                //}
                //catch
                //{
                //}
                //try // Prevents crash in the simulator that uses IE.
                //{
                //    domStyle.WebkitTransform = value;
                //}
                //catch
                //{
                //}
            }
        }


        // NOTE: CenterX and CenterY are currently not supported because in CSS there is only the "transformOrigin" property, which is used for the "UIElement.RenderTransformOrigin" property.


        internal override void INTERNAL_ApplyTransform()
        {
            this.ApplySkewTransform(this.AngleX, this.AngleY);
        }

        internal override void INTERNAL_UnapplyTransform()
        {
            if (this.INTERNAL_parent != null && INTERNAL_VisualTreeManager.IsElementInVisualTree(this.INTERNAL_parent))
            {
                object parentDom = this.INTERNAL_parent.INTERNAL_OuterDomElement;
                CSSEquivalent angleXcssEquivalent = AngleXProperty.GetTypeMetaData(typeof(SkewTransform)).GetCSSEquivalent(this);
                INTERNAL_HtmlDomManager.SetDomElementStylePropertyUsingVelocity(angleXcssEquivalent.DomElement, angleXcssEquivalent.Name, angleXcssEquivalent.Value(this, 0));
                CSSEquivalent angleYcssEquivalent = AngleYProperty.GetTypeMetaData(typeof(SkewTransform)).GetCSSEquivalent(this);
                INTERNAL_HtmlDomManager.SetDomElementStylePropertyUsingVelocity(angleYcssEquivalent.DomElement, angleYcssEquivalent.Name, angleYcssEquivalent.Value(this, 0));
            }
        }

        protected override Point INTERNAL_TransformPoint(Point point)
        {
            throw new NotImplementedException("Please contact support@cshtml5.com");
        }
    }
}
