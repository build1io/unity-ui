using UnityEngine;

namespace Build1.UnityUI
{
    public static class UnityUI
    {
        private static GameObject _root;
        
        public static GameObject GetRoot()
        {
            if (_root == null)
            {
                _root = new GameObject("[UnityUI]");
                Object.DontDestroyOnLoad(_root);
            }

            return _root;
        }
    }
}