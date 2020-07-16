﻿

/*===================================================================================
* 
*   Copyright (c) Userware/OpenSilver.net
*      
*   This file is part of the OpenSilver Runtime (https://opensilver.net), which is
*   licensed under the MIT license: https://opensource.org/licenses/MIT
*   
*   As stated in the MIT license, "the above copyright notice and this permission
*   notice shall be included in all copies or substantial portions of the Software."
*  
\*====================================================================================*/


using CSHTML5.Native.Html.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if MIGRATION
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
#else
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
#endif

namespace CSHTML5.Native.Html.Input
{
#if MIGRATION
    public class HtmlCanvasPointerRoutedEventArgs : MouseButtonEventArgs
#else
    public class HtmlCanvasPointerRoutedEventArgs : PointerRoutedEventArgs
#endif
    {
        public readonly HtmlCanvas HtmlCanvas;

        // Note: there are multiple constructor overloads.
#if MIGRATION
        public HtmlCanvasPointerRoutedEventArgs(MouseEventArgs e, HtmlCanvas htmlCanvas)
#else
        public HtmlCanvasPointerRoutedEventArgs(PointerRoutedEventArgs e, HtmlCanvas htmlCanvas)
#endif
        {
            this.OriginalSource = e.OriginalSource;
            this.Handled = e.Handled;
            this.KeyModifiers = e.KeyModifiers;
            this.Pointer = e.Pointer;
            this._pointerAbsoluteX = e._pointerAbsoluteX;
            this._pointerAbsoluteY = e._pointerAbsoluteY;

            this.HtmlCanvas = htmlCanvas;
        }

        // Note: there are multiple constructor overloads.
        public HtmlCanvasPointerRoutedEventArgs(RightTappedRoutedEventArgs e, HtmlCanvas htmlCanvas)
        {
            this.OriginalSource = e.OriginalSource;
            this.Handled = e.Handled;
            this._pointerAbsoluteX = e._pointerAbsoluteX;
            this._pointerAbsoluteY = e._pointerAbsoluteY;

            this.HtmlCanvas = htmlCanvas;
        }


#if MIGRATION
        public Point GetPosition(HtmlCanvasElement relativeTo)
#else
        public PointerPoint GetCurrentPoint(HtmlCanvasElement relativeTo)
#endif
        {
            if (relativeTo == null)
            {
#if MIGRATION
                return base.GetPosition(null);
#else
                return base.GetCurrentPoint(null);
#endif
            }
            else
            {
#if MIGRATION
                Point pointerPoint = base.GetPosition(this.HtmlCanvas);
#else
                PointerPoint pointerPoint = base.GetCurrentPoint(this.HtmlCanvas);
#endif
                Stack<HtmlCanvasElement> elem = HtmlCanvas.SearchElement(this.HtmlCanvas, relativeTo);

#if MIGRATION
                double x = pointerPoint.X;
                double y = pointerPoint.Y;
#else
                double x = pointerPoint.Position.X;
                double y = pointerPoint.Position.Y;
#endif
                if (elem == null)
                    return pointerPoint;

                while (elem.Count > 0)
                {
                    var e = elem.Pop();
                    x -= e.X;
                    y -= e.Y;
                }


#if MIGRATION
                pointerPoint = new Point(x, y);
#else
                pointerPoint.Position = new Point(x, y);
#endif
                return pointerPoint;
            }
        }
    }
}
