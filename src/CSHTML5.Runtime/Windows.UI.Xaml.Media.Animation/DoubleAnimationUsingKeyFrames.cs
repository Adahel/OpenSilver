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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
#if MIGRATION
using System.Windows.Controls;
#else 
using Windows.UI.Xaml.Controls;
#endif

#if MIGRATION
namespace System.Windows.Media.Animation
#else
namespace Windows.UI.Xaml.Media.Animation
#endif
{
    /// <summary>
    /// Animates the value of a Double property along a set of key frames.
    /// </summary>
    [ContentProperty("KeyFrames")]
#if WORKINPROGRESS
    public sealed class DoubleAnimationUsingKeyFrames : AnimationTimeline
#else
    public sealed class DoubleAnimationUsingKeyFrames : Timeline
#endif
    {
        string _targetName;
        PropertyPath _targetProperty;
        DependencyObject _target;

        private IterationParameters _parameters;

        private DoubleKeyFrameCollection _keyFrames;

        private int _appliedKeyFramesCount;
        private TimeSpan _ellapsedTime;

        private INTERNAL_ResolvedKeyFramesEntries _resolvedKeyFrames;

        private DoubleKeyFrame _currentKeyFrame;
        private DoubleAnimation _currentAnimation;
        //     The collection of DoubleKeyFrame objects that define the animation. The default
        //     is an empty collection.
        /// <summary>
        /// Gets the collection of DoubleKeyFrame objects that define the animation.
        /// </summary>
        public DoubleKeyFrameCollection KeyFrames
        {
            get
            {
                if (_keyFrames == null)
                {
                    _keyFrames = new DoubleKeyFrameCollection();
                }
                return _keyFrames;
            }
            set
            {
                _keyFrames = value;
            }
        }

        /// <summary>
        /// Returns the largest time span specified key time from all of the key frames.
        /// If there are not time span key times a time span of one second is returned
        /// to match the default natural duration of the From/To/By animations.
        /// </summary>
        private TimeSpan LargestTimeSpanKeyTime
        {
            get
            {
                if (_keyFrames == null || _keyFrames.Count == 0)
                {
                    return TimeSpan.FromTicks(0);
                }
                if (_resolvedKeyFrames != null)
                {
                    return _keyFrames[_resolvedKeyFrames.GetNextKeyFrameIndex(_keyFrames.Count - 1)].KeyTime.TimeSpan;
                }
                else
                {
                    throw new Exception("DoubleAnimationUsingKeyFrames has not been setup yet.");
                }
            }
        }

        private void InitializeKeyFramesSet()
        {
            _resolvedKeyFrames = new INTERNAL_ResolvedKeyFramesEntries(_keyFrames);
            _appliedKeyFramesCount = 0;
            _ellapsedTime = new TimeSpan();
        }

        internal override void Apply(IterationParameters parameters, bool isLastLoop)
        {
            StopCurrentAnimation(parameters.Target, revertToFormerValue: false);

            _parameters = parameters;
            DependencyObject target;
            PropertyPath propertyPath;
            DependencyObject targetBeforePath;
            GetPropertyPathAndTargetBeforePath(parameters.Target, out targetBeforePath, out propertyPath);
            DependencyObject parentElement = targetBeforePath; //this will be the parent of the clonable element (if any).
            foreach (Tuple<DependencyObject, DependencyProperty, int?> element in GoThroughElementsToAccessProperty(propertyPath, targetBeforePath))
            {
                DependencyObject depObject = element.Item1;
                DependencyProperty depProp = element.Item2;
                int? index = element.Item3;
                if (depObject is ICloneOnAnimation)
                {

                    if (!((ICloneOnAnimation)depObject).IsAlreadyAClone())
                    {
                        object clone = ((ICloneOnAnimation)depObject).Clone();
                        if (index != null)
                        {
#if BRIDGE
                            parentElement.GetType().GetProperty("Item").SetValue(parentElement, clone, new object[] { index });
#else
                            //JSIL does not support SetValue(object, object, object[])
#endif
                        }
                        else
                        {
                            parentElement.SetValue(depProp, clone);
                        }
                    }
                    break;
                }
                else
                {
                    parentElement = depObject;
                }
            }

            GetTargetElementAndPropertyInfo(parameters.Target, out target, out propertyPath);
            //DependencyObject lastElementBeforeProperty = propertyPath.INTERNAL_AccessPropertyContainer(target);
            DependencyProperty dp = GetProperty(target, propertyPath);

            _currentKeyFrame = GetNextKeyFrame();

            _target = target;
            _targetProperty = propertyPath;
            _targetName = Storyboard.GetTargetName(this);

            ApplyKeyFrame(_currentKeyFrame);
        }

        private void ApplyKeyFrame(DoubleKeyFrame keyFrame)
        {
            if (keyFrame != null)
            {
                _currentAnimation = InstantiateAnimationFromKeyFrame(keyFrame);
                _currentAnimation.Completed -= ApplyNextKeyFrame;
                _currentAnimation.Completed += ApplyNextKeyFrame;
                Storyboard.SetTargetName(_currentAnimation, _targetName);
                Storyboard.SetTargetProperty(_currentAnimation, _targetProperty);
                Storyboard.SetTarget(_currentAnimation, _parameters.Target);
                _currentAnimation.InitializeIteration();
                _currentAnimation.StartFirstIteration(_parameters, true, null);
            }
        }

        private DoubleAnimation InstantiateAnimationFromKeyFrame(DoubleKeyFrame keyFrame)
        {
            return new DoubleAnimation()
            {
                BeginTime = TimeSpan.Zero,
                To = keyFrame.Value,
                Duration = keyFrame.KeyTime.TimeSpan - _ellapsedTime,
                EasingFunction = keyFrame.INTERNAL_GetEasingFunction(),
            };
        }

        private void ApplyNextKeyFrame(object sender, EventArgs e)
        {
            _appliedKeyFramesCount++;
            _ellapsedTime = _currentKeyFrame.KeyTime.TimeSpan;
            _currentKeyFrame = GetNextKeyFrame();
            if(!CheckTimeLineEndAndRaiseCompletedEvent(_parameters))
            {
                ApplyKeyFrame(_currentKeyFrame);
            }
        }


        private DoubleKeyFrame GetNextKeyFrame()
        {
            int nextKeyFrameIndex = _resolvedKeyFrames.GetNextKeyFrameIndex(_appliedKeyFramesCount);
            if (nextKeyFrameIndex == -1)
            {
                return null;
            }
            else
            {
                return _keyFrames[nextKeyFrameIndex];
            }
        }

        private void ApplyLastKeyFrame(object sender, EventArgs e)
        {
            DoubleKeyFrame lastKeyFrame = _keyFrames[_resolvedKeyFrames.GetNextKeyFrameIndex(_keyFrames.Count - 1)];
            AnimationHelpers.ApplyInstantAnimation(_target, _targetProperty, lastKeyFrame.Value, _parameters.IsVisualStateChange);
        }

        internal override void Stop(FrameworkElement frameworkElement, string groupName, bool revertToFormerValue = false) //frameworkElement is for the animations requiring the use of GetCssEquivalent
        {
            base.Stop(frameworkElement, groupName, revertToFormerValue);
            StopCurrentAnimation(frameworkElement, groupName, revertToFormerValue);
        }

        private void StopCurrentAnimation(FrameworkElement frameworkElement, string groupName = "visualStateGroupName", bool revertToFormerValue = false)
        {
            if (_currentAnimation != null)
            {
                _currentAnimation.Completed -= ApplyNextKeyFrame;
                _currentAnimation.Stop(frameworkElement, groupName, revertToFormerValue);
                _currentAnimation = null;
            }
        }

        object thisLock = new object();
        private bool CheckTimeLineEndAndRaiseCompletedEvent(IterationParameters parameters)
        {
            bool raiseEvent = false;
            lock (thisLock)
            {
                if (_appliedKeyFramesCount >= _keyFrames.Count)
                {
                    raiseEvent = true;
                }
            }
            if (raiseEvent)
            {
                OnIterationCompleted(parameters);
            }
            return raiseEvent;
        }

        internal override void IterateOnce(IterationParameters parameters, bool isLastLoop)
        {
            this.Completed -= ApplyLastKeyFrame;
            this.Completed += ApplyLastKeyFrame;
            InitializeKeyFramesSet();
            base.IterateOnce(parameters, isLastLoop);
            Apply(parameters, isLastLoop);
        }

#if WORKINPROGRESS
        protected override Duration GetNaturalDurationCore()
        {
            return new Duration(LargestTimeSpanKeyTime);
        }
#endif
    }
}
