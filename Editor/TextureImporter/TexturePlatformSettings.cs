namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEditor;


    [Serializable]
    public class TexturePlatformSettings
    {
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public BoolValue                             overriden                   = new BoolValue(true);
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public BoolValue                             allowsAlphaSplitting        = new BoolValue(false);
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public AndroidFallbackFormatValue            androidETC2FallbackOverride = new AndroidFallbackFormatValue();
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public CompressionQualityValue               compressionQuality          = new CompressionQualityValue();
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public BoolValue                             useCrunchedCompression      = new BoolValue(true);
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public TextureImporterFormatValue            textureImporterFormat       = new TextureImporterFormatValue(TextureImporterFormat.ETC2_RGBA8Crunched);
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public TextureImporterCompressionFormatValue textureCompression          = new TextureImporterCompressionFormatValue(TextureImporterCompression.Compressed);
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public TextureResizeAlgorithmFormatValue     resizeAlgorithm             = new TextureResizeAlgorithmFormatValue(TextureResizeAlgorithm.Mitchell);
#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        public TextureSizeValue                      maxTextureSize              = new TextureSizeValue();
    }
}