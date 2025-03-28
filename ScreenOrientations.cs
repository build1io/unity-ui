using System;

namespace Build1.UnityUI
{
    [Flags]
    public enum ScreenOrientations
    {
        Unknown            = 1 << 0,
        Portrait           = 1 << 1,
        PortraitUpsideDown = 1 << 2,
        LandscapeLeft      = 1 << 3,
        LandscapeRight     = 1 << 4
    }
}