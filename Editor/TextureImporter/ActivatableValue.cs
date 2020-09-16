namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;

    [Serializable]
    public class ActivatableValue<T> : IActivatableValue<T>
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.HorizontalGroup(ActivableInspector.ActivatableLabelWidth)]
        [Sirenix.OdinInspector.HideLabel]
#endif 
        public bool enabled;
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.HorizontalGroup(ActivableInspector.ActivatableValueLabelWidth)]
        [Sirenix.OdinInspector.EnableIf(nameof(enabled))]
        [Sirenix.OdinInspector.HideLabel]
#endif
        public T value;

        public ActivatableValue() { }

        public ActivatableValue(T defaultValue) => value = defaultValue;

        public bool Enabled => enabled;
        public T    Value   => value;

        public static implicit operator T(ActivatableValue<T> source) => source.value;
    }
}