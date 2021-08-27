using System;

namespace Build1.UnityUI
{
    [Flags]
    public enum InterfaceType
    {
        Unknown = 0,
        Phone   = 1 << 0,
        Tablet  = 1 << 1,
        Desktop = 1 << 2,
        Web     = 1 << 3,
        Console = 1 << 4,
        TV      = 1 << 5
    }
}