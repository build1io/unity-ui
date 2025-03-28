using UnityEngine;

namespace Build1.UnityUI
{
    public static class UnityUIUtils
    {
        public static bool IsPortrait(this ScreenOrientation orientation)
        {
            return orientation is ScreenOrientation.Portrait or ScreenOrientation.PortraitUpsideDown;
        }
        
        public static bool IsLandscape(this ScreenOrientation orientation)
        {
            return orientation is ScreenOrientation.LandscapeLeft or ScreenOrientation.LandscapeRight;
        }
    }
}