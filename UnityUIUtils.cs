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

        public static bool IsPortraitOrLandscape(this DeviceOrientation orientation)
        {
            return orientation is DeviceOrientation.Portrait or DeviceOrientation.PortraitUpsideDown or DeviceOrientation.LandscapeLeft or DeviceOrientation.LandscapeRight;
        }

        public static bool IsPortrait(this DeviceOrientation orientation)
        {
            return orientation is DeviceOrientation.Portrait or DeviceOrientation.PortraitUpsideDown;
        }
        
        public static bool IsLandscape(this DeviceOrientation orientation)
        {
            return orientation is DeviceOrientation.LandscapeLeft or DeviceOrientation.LandscapeRight;
        }
    }
}