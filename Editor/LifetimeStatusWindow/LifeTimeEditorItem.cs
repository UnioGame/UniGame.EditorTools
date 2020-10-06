namespace UniModules.UniGame.EditorTools.Editor.LifetimeStatusWindow
{
    using System;
    using Core.Runtime.ScriptableObjects;
    using Object = UnityEngine.Object;

    [Serializable]
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.FoldoutGroup("Lifetime")]
#endif
    public class LifeTimeEditorItem
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineEditor(Sirenix.OdinInspector.InlineEditorObjectFieldModes.Foldout)]
#endif
        public LifetimeScriptableObject LifeTime;

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
#endif
        public bool IsTerminated;

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
#endif
        public string Name { get; private set; }

        public LifeTimeEditorItem(LifetimeScriptableObject lifeTime)
        {
            LifeTime = lifeTime;
            Name     = LifeTime.name;
            LifeTime.AddCleanUpAction(() => IsTerminated = true);
            IsTerminated = LifeTime.LifeTime.IsTerminated;
        }
        
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Destroy()
        {
            if (LifeTime) {
                Object.DestroyImmediate(LifeTime);
            }
        }
        
    }
}