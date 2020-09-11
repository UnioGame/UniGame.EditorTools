namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using UnityEditor;

    [Serializable]
    public class TextureResizeAlgorithmFormatValue : ActivatableValue<TextureResizeAlgorithm>
    {
        
        public TextureResizeAlgorithmFormatValue(TextureResizeAlgorithm format) : base(format)
        {
            
        }
        
    }
}