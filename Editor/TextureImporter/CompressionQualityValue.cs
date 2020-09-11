namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using UnityEngine;

    [Serializable]
    public class CompressionQualityValue : IActivatableValue<int>
    {

        public bool enabled;

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.EnableIf(nameof(enabled))]
#endif
        [Range(1, 100)]
        public int value = 90;

        public bool Enabled => enabled;
        public int  Value   => value;
    }
}