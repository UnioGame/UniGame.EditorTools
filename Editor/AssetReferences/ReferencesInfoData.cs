using UniModules.Editor;

namespace UniModules.UniGame.EditorTools.Editor.AssetReferences
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [Serializable]
    public class ReferencesInfoData
    {
        [SerializeField]
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.HideLabel]
        [Sirenix.OdinInspector.InlineProperty]
#endif
        public ResourceHandle source;
        
        [Space(4)]
        [SerializeField]
        public List<Object> references = new List<Object>();
    }
}