using System;
using TMPro;

namespace Build1.UnityUI.TextMeshPro
{
    public static class TextMeshProExtensions
    {
        public static bool TrySetText(this TextMeshProUGUI text, Func<string> valueCallback)
        {
            if (text == null)
                return false;
            text.text = valueCallback.Invoke();
            return true;
        }
    }
}