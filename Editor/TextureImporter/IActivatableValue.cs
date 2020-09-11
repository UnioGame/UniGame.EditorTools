namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    public interface IActivatableValue<T>
    {
        bool Enabled { get; }
        T    Value   { get; }
        
    }
}