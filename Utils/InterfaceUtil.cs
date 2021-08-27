using System;
using UnityEngine;

namespace Build1.UnityUI.Utils
{
    public static class InterfaceUtil
    {
        public static InterfaceType GetInterfaceType(RuntimePlatform platform, DeviceType deviceType)
        {
            switch (platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    return ScreenUtil.GetDiagonalInches() >= 7f && ScreenUtil.GetAspectRatio() < 2f ? InterfaceType.Tablet : InterfaceType.Phone;

                case RuntimePlatform.Android:
                    return deviceType switch
                    {
                        DeviceType.Handheld => ScreenUtil.GetDiagonalInches() >= 7f && ScreenUtil.GetAspectRatio() < 2f ? InterfaceType.Tablet : InterfaceType.Phone,
                        DeviceType.Console  => InterfaceType.Console,
                        DeviceType.Desktop  => InterfaceType.Desktop,
                        _                   => throw new ArgumentOutOfRangeException($"Unknown interface type. Platform: {platform} DeviceType: {deviceType}")
                    };

                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.LinuxEditor:
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.LinuxPlayer:
                    return InterfaceType.Desktop;

                case RuntimePlatform.WebGLPlayer:
                    return InterfaceType.Web;

                case RuntimePlatform.PS4:
                case RuntimePlatform.PS5:
                case RuntimePlatform.XboxOne:
                case RuntimePlatform.Switch:
                    return InterfaceType.Console;

                case RuntimePlatform.tvOS:
                    return InterfaceType.TV;

                default:
                    throw new ArgumentOutOfRangeException($"Unknown interface type. Platform: {platform} DeviceType: {deviceType}");
            }
        }
    }
}