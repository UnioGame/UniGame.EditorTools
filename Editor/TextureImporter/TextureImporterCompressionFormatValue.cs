namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using UnityEditor;

    [Serializable]
    public class TextureImporterCompressionFormatValue : ActivatableValue<TextureImporterCompression>
    {
        public TextureImporterCompressionFormatValue(TextureImporterCompression format) : base(format)
        {
            
        }
    }
}