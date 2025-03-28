using System;

namespace Build1.UnityUI
{
    [Flags]
    public enum ScreenOrientations
    {
        Portrait           = 1 << 0,
        PortraitUpsideDown = 1 << 1,
        LandscapeLeft      = 1 << 2,
        LandscapeRight     = 1 << 3
    }
}