namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using UnityEditor;
    using UnityEngine;


    [Serializable]
    public class TexturePlatformSettings
    {
        private const int LabelWidth = 160;
        
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/overriden", LabelWidth = LabelWidth)]
#endif
        public BoolValue overriden = new BoolValue(true);
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/alphaSplitting", LabelWidth = LabelWidth)]
#endif
        public BoolValue alphaSplitting = new BoolValue(false);
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/androidETC2FallbackOverride", LabelWidth = LabelWidth)]
#endif
        [Space]
        public AndroidFallbackFormatValue ETC2FallbackOverride = new AndroidFallbackFormatValue();
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/compressionQuality", LabelWidth = LabelWidth)]
#endif
        public CompressionQualityValue compressionQuality = new CompressionQualityValue();
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/useCrunchedCompression", LabelWidth = LabelWidth)]
#endif
        public BoolValue сrunchedCompression = new BoolValue(true);
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/textureImporterFormat", LabelWidth = LabelWidth)]
#endif
        public TextureImporterFormatValue textureImporterFormat = new TextureImporterFormatValue(TextureImporterFormat.ETC2_RGBA8Crunched);
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/textureCompression", LabelWidth = LabelWidth)]
#endif
        public TextureImporterCompressionFormatValue textureCompression = new TextureImporterCompressionFormatValue(TextureImporterCompression.Compressed);
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/resizeAlgorithm", LabelWidth = LabelWidth)]
#endif
        public TextureResizeAlgorithmFormatValue resizeAlgorithm = new TextureResizeAlgorithmFormatValue(TextureResizeAlgorithm.Mitchell);
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.FoldoutGroup("TexturePlatformSettings")]
        [Sirenix.OdinInspector.HorizontalGroup("TexturePlatformSettings/maxTextureSize", LabelWidth = LabelWidth)]
#endif
        [Space]
        public TextureSizeValue maxTextureSize = new TextureSizeValue();
        
    }
}