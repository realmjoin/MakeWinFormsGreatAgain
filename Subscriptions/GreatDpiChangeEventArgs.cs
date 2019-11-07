using System;
using System.Drawing;

namespace Mwfga.Subscriptions
{
    public class GreatDpiChangeEventArgs : EventArgs
    {
        public GreatDpiChangeEventArgs(Size newDpi)
        {
            NewDpi = newDpi;
        }

        public Size NewDpi { get; }
    }
}