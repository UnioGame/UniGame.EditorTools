namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using UnityEngine;

    [Serializable]
    public class CompressionQualityValue : IActivatableValue<int>
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
        [Range(1, 100)]
        public int value = 90;

        public bool Enabled => enabled;
        public int  Value   => value;
    }
}