﻿// -----------------------------------------------------------------------
// <copyright file="Style.cs" company="Tricycle">
// Copyright 2014 Tricycle. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Perspex
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using Perspex.Controls;

    public class Style : IStyle
    {
        public Style()
        {
            this.Setters = new List<Setter>();
        }

        public Style(Func<Control, Match> selector)
            : this()
        {
            this.Selector = selector;
        }

        public Func<Control, Match> Selector
        {
            get;
            set;
        }

        public IEnumerable<Setter> Setters
        {
            get;
            set;
        }

        public void Attach(Control control)
        {
            Match match = this.Selector(control);

            if (match != null)
            {
                string description = "Style " + match.ToString();
                IObservable<bool> activator = match.GetActivator();

                foreach (Setter setter in this.Setters)
                {
                    control.SetValue(setter.Property, setter.Value, activator);
                }
            }
        }
    }
}
