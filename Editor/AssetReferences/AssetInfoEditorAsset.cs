﻿#if ODIN_INSPECTOR


namespace UniModules.UniGame.EditorTools.Editor.AssetReferences
{
    using System.Collections.Generic;
    using System.Linq;
    using UniModules.Editor;
    using Sirenix.OdinInspector;
    using UnityEditor;
    using UnityEngine;

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