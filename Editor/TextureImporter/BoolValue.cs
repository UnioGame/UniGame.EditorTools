namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;

    [Serializable]
    public class BoolValue : ActivatableValue<bool>
    {
        public BoolValue(bool defaultValue) : base(defaultValue)
        {
        }
    }
}