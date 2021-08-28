using System;

namespace Build1.UnityUI.Utils
{
    [Flags]
    public enum InterfaceType
    {
        Phone   = 1 << 0,
        Tablet  = 1 << 1,
        Desktop = 1 << 2,
        Web     = 1 << 3,
        Console = 1 << 4,
        TV      = 1 << 5
    }
}