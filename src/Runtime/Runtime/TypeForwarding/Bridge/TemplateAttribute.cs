﻿using System;

namespace Bridge
{
    public sealed class TemplateAttribute : Attribute
    {
        public TemplateAttribute()
        {
        }

        public TemplateAttribute(string format)
        {
        }

        public TemplateAttribute(string format, string nonExpandedFormat)
        {
        }

        public string Fn
        {
            get; set;
        }
    }
}