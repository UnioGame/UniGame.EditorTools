namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using UnityEditor;

    [Serializable]
    public class TextureImporterFormatValue : ActivatableValue<TextureImporterFormat>
    {
        public TextureImporterFormatValue(TextureImporterFormat format) : base(format)
        {
            
        }
    }
}