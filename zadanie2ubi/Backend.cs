using System;
namespace zadanie2ubi
{
    public class Backend
    {
        private static readonly Lazy<Backend>
        lazy =
        new Lazy<Backend>
            (() => new Backend());

        public static Backend Instance { get { return lazy.Value; } }

        private Backend()
        {
        }
    }
}
