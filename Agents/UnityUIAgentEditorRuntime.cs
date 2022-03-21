using UnityEngine;

#if UNITY_EDITOR

namespace Build1.UnityUI.Agents
{
    internal sealed class UnityUIAgentEditorRuntime : UnityUIAgentRuntime
    {
        public override InterfaceType GetInterfaceType()
        {
            return UnityUIAgentEditor.GetInterfaceTypeStatic(this);
        }
        
        /*
         * Static.
         */
        
        public new static IUnityUIAgent Create(GameObject root)
        {
            return root.AddComponent<UnityUIAgentEditorRuntime>();
        }
    }
}

#endif