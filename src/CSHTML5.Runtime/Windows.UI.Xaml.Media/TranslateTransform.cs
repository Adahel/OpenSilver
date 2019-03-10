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
    /// Translates (moves) an object in the two-dimensional x-y coordinate system.
    /// </summary>
    public sealed class TranslateTransform : Transform
    {
        /// <summary>
        /// Gets or sets the distance to translate along the x-axis.
        /// </summary>
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        /// <summary>
        /// Identifies the X dependency property
        /// </summary>
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(TranslateTransform), new PropertyMetadata(0d)//, X_Changed));
            {
                GetCSSEquivalent = (instance) =>
                {
                    if (((TranslateTransform)instance).INTERNAL_parent != null)
                    {
                        return new CSSEquivalent()
                        {
                            DomElement = ((TranslateTransform)instance).INTERNAL_parent.INTERNAL_OuterDomElement,
                            Value = (inst, value) =>
                            {
                                return value + "px";
                            },
                            Name = new List<string> { "translateX" }, //Note: the css use would be: transform = "scaleX(2)" but the velocity call must use: scaleX : 2
                            UIElement = ((TranslateTransform)instance).INTERNAL_parent,
                            ApplyAlsoWhenThereIsAControlTemplate = true,
                            OnlyUseVelocity = true
                        };
                    }
                    return null;
                }
            });

        private static void X_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var translateTransform = (TranslateTransform)d;
            double newX = (double)e.NewValue;
            translateTransform.ApplyTranslateTransform(newX, translateTransform.Y);
        }


        /// <summary>
        /// Gets or sets the distance to translate (move) an object along the y-axis.
        /// </summary>
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        /// <summary>
        /// Identifies the Y dependency property
        /// </summary>
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(TranslateTransform), new PropertyMetadata(0d)//, Y_Changed));
            {
                GetCSSEquivalent = (instance) =>
                {
                    if (((TranslateTransform)instance).INTERNAL_parent != null)
                    {
                        return new CSSEquivalent()
                        {
                            DomElement = ((TranslateTransform)instance).INTERNAL_parent.INTERNAL_OuterDomElement,
                            Value = (inst, value) =>
                            {
                                return value + "px";
                            },
                            Name = new List<string> { "translateY" }, //Note: the css use would be: transform = "scaleX(2)" but the velocity call must use: scaleX : 2
                            UIElement = ((TranslateTransform)instance).INTERNAL_parent,
                            ApplyAlsoWhenThereIsAControlTemplate = true,
                            OnlyUseVelocity = true
                        };
                    }
                    return null;
                }
            });

        //private static void Y_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var translateTransform = (TranslateTransform)d;
        //    double newY = (double)e.NewValue;
        //    translateTransform.ApplyTranslateTransform(translateTransform.X, newY);
        //}

        internal void ApplyTranslateTransform(double x, double y)
        {
            if (this.INTERNAL_parent != null && INTERNAL_VisualTreeManager.IsElementInVisualTree(this.INTERNAL_parent))
            {
                object parentDom = this.INTERNAL_parent.INTERNAL_OuterDomElement;
                CSSEquivalent translateXcssEquivalent = XProperty.GetTypeMetaData(typeof(TranslateTransform)).GetCSSEquivalent(this);
                INTERNAL_HtmlDomManager.SetDomElementStylePropertyUsingVelocity(translateXcssEquivalent.DomElement, translateXcssEquivalent.Name, translateXcssEquivalent.Value(this, x));

                CSSEquivalent translateYcssEquivalent = YProperty.GetTypeMetaData(typeof(TranslateTransform)).GetCSSEquivalent(this);
                INTERNAL_HtmlDomManager.SetDomElementStylePropertyUsingVelocity(translateYcssEquivalent.DomElement, translateYcssEquivalent.Name, translateYcssEquivalent.Value(this, y));

                //dynamic domStyle = INTERNAL_HtmlDomManager.GetFrameworkElementOuterStyleForModification(this.INTERNAL_parent);

                //string value = "translate(" + x + "px, " + y + "px)"; //todo: make sure that the conversion from double to string is culture-invariant so that it uses dots instead of commas for the decimal separator.

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

        internal override void INTERNAL_ApplyTransform()
        {
            this.ApplyTranslateTransform(this.X, this.Y);
        }

        internal override void INTERNAL_UnapplyTransform()
        {
            if (this.INTERNAL_parent != null && INTERNAL_VisualTreeManager.IsElementInVisualTree(this.INTERNAL_parent))
            {
                object parentDom = this.INTERNAL_parent.INTERNAL_OuterDomElement;
                CSSEquivalent scaleXcssEquivalent = XProperty.GetTypeMetaData(typeof(TranslateTransform)).GetCSSEquivalent(this);
                INTERNAL_HtmlDomManager.SetDomElementStylePropertyUsingVelocity(scaleXcssEquivalent.DomElement, scaleXcssEquivalent.Name, scaleXcssEquivalent.Value(this, 0));

                CSSEquivalent scaleYcssEquivalent = YProperty.GetTypeMetaData(typeof(TranslateTransform)).GetCSSEquivalent(this);
                INTERNAL_HtmlDomManager.SetDomElementStylePropertyUsingVelocity(scaleYcssEquivalent.DomElement, scaleYcssEquivalent.Name, scaleYcssEquivalent.Value(this, 0));
            }
        }

        protected override Point INTERNAL_TransformPoint(Point point)
        {
            return new Point(point.X + X, point.Y + Y);
        }
    }
}
