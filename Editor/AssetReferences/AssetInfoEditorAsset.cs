#if ODIN_INSPECTOR


namespace UniModules.UniGame.EditorTools.Editor.AssetReferences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UniModules.Editor;
    using Sirenix.OdinInspector;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public class AssetInfoEditorAsset : ScriptableObject
    {
        #region static data

        public const string FilterGroup = "Filter";

        #endregion


        #region inspector

#if ODIN_INSPECTOR
        [OnValueChanged("UpdateGuidData")]
#endif
        public string guid;

#if ODIN_INSPECTOR
        [OnValueChanged("UpdateGuidAssetData")]
        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [PreviewField(ObjectFieldAlignment.Center, Height = 140)]
#endif
        public Object asset;

        [TitleGroup("Class Id")]
        [OnValueChanged(nameof(UpdateClassId))]
        [InlineButton(nameof(UpdateClassId))]
        public int classId;

        [TitleGroup("Class Id")] 
        public string classIdType;
        
        [FoldoutGroup(FilterGroup)] 
        public string[] fileTypes     = AssetReferenceFinder.DefaultSearchTargets.ToArray();
        
        [FoldoutGroup(FilterGroup)] 
        public string[] regexFilters  = new string[0];
        
        [FoldoutGroup(FilterGroup)] 
        [FolderPath]
        public string[] foldersFilter = new string[0];

        [Space(6)] public List<ReferencesInfoData> dependencies = new List<ReferencesInfoData>();

        #endregion
        
        #region public methods

        [Button]
        [GUIColor(0.2f, 1, 0.2f)]
        public void FindDependencies()
        {
            CleanDependencies();
            if (!asset)
                return;

            var references = AssetReferenceFinder.FindReferences(new SearchData() {
                assets       = new[] {asset},
                regExFilters = regexFilters,
                fileTypes    = fileTypes,
                foldersFilter = foldersFilter,
            });
            
            foreach (var reference in references.referenceMap) {
                var assetItem      = reference.Key;
                var referencesData = reference.Value.Select(x => x.asset).ToList();
                var referenceData = new ReferencesInfoData() {
                    source     = assetItem.ToEditorResource(),
                    references = referencesData
                };
                dependencies.Add(referenceData);
            }
        }

        #endregion

        
        public void UpdateClassId()
        {
            classIdType = String.Empty;

            try
            {
                var assembly = Assembly.GetAssembly(typeof(UnityEditor.Selection));
                var typeName = "UnityEditor.UnityType";
                var type = assembly.GetType(typeName, false, true);
                var method = type.GetMethod("FindTypeByPersistentTypeID");
                var result = method.Invoke(null, new object[] {classId});

                if (result == null) return;

                var nameFiled = result.GetType().GetProperty("name");
                var nativeNamespaceField = result.GetType().GetProperty("nativeNamespace");
                var qualifiedNameField = result.GetType().GetProperty("qualifiedName");
            
                var name = nameFiled.GetValue(result) as string;
                var nativeNamespace = nativeNamespaceField.GetValue(result) as string;
                var qualifiedName = qualifiedNameField.GetValue(result) as string;
            
                classIdType = $"{name} | {qualifiedName} | {nativeNamespace}";
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        
        private void CleanDependencies()
        {
            dependencies.Clear();
        }


        private string UpdateGuidData()
        {
            UpdateView(guid);
            return guid;
        }

        private Object UpdateGuidAssetData()
        {
            UpdateView(asset ? AssetEditorTools.GetGUID(asset) : string.Empty);
            return asset;
        }

        private string UpdateView(string newGuid)
        {
            CleanDependencies();

            var assetPath = AssetDatabase.GUIDToAssetPath(newGuid);
            asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            guid  = newGuid;

            return newGuid;
        }
    }
}

#endif