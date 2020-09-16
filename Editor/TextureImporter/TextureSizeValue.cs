namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TextureSizeValue : IActivatableValue<int>
    {
        private static IEnumerable<int> TextureSizes = new List<int>() {
            32, 64, 128, 256, 512, 1024, 2048, 4096
        };
        
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.HorizontalGroup(ActivableInspector.ActivatableLabelWidth)]
        [Sirenix.OdinInspector.HideLabel]
#endif 
        public bool enabled;
        
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.HorizontalGroup(ActivableInspector.ActivatableValueLabelWidth)]
        [Sirenix.OdinInspector.EnableIf(nameof(enabled))]
        [Sirenix.OdinInspector.HideLabel]
        [Sirenix.OdinInspector.ValueDropdown(nameof(TextureSizes))]
#endif
        public int value = 1024;

        public bool Enabled => enabled;
        public int  Value   => value;

    }
}