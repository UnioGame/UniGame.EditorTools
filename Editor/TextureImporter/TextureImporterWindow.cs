#if ODIN_INSPECTOR

namespace UniModules.UniGame.EditorTools.Editor.TestureImporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Core.EditorTools.Editor.AssetOperations;
    using Core.Runtime.Extension;
    using Sirenix.OdinInspector.Editor;
    using UniGreenModules.UniCore.EditorTools.Editor;
    using UniGreenModules.UniCore.EditorTools.Editor.Utility;
    using UniGreenModules.UniCore.Runtime.Extension;
    using UniGreenModules.UniCore.Runtime.Utils;
    using UnityEditor;
    using UnityEditor.U2D.PSD;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [Flags]
    public enum TextureImporterFilter
    {
        All = ~0,
        TextureImporter = 1<< 1,
        PSDImporter = 1 << 2,
    }
    
    public class TextureImporterWindow : OdinEditorWindow
    {
        #region statics

        private const string psdImporterPlatformField     = "m_PlatformSettings";
        private const string defaultTarget                = "Default";
        private const string textureImporterDefaultTarget = "DefaultTexturePlatform";

        private static FieldInfo _psdImporterPlatformsFieldInfo;

        private static List<string> buildTargets = defaultTarget.Yield().Concat(EnumValue<BuildTarget>.Names).ToList();

        [MenuItem("UniGame/Tools/Texture Importer")]
        public static void Open()
        {
            var window = GetWindow<TextureImporterWindow>();
            window.Show();
        }

        #endregion

        #region inspector

        [Space(6)]
        [Sirenix.OdinInspector.BoxGroup("Import Settings")]
        [Sirenix.OdinInspector.ValueDropdown("buildTargets")]
        public string buildTarget = "Default";
        [Space(4)]
        [Sirenix.OdinInspector.BoxGroup("Import Settings")]
        public TextureImporterFilter importersFilter = (TextureImporterFilter)~0;
        
        [SerializeField]
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.HideLabel]
        [Sirenix.OdinInspector.BoxGroup("Import Settings")]
        public TexturePlatformSettings platformSettings = new TexturePlatformSettings();


        [Space(8)] 
        [Sirenix.OdinInspector.BoxGroup("Search Filter")]
        [Sirenix.OdinInspector.InlineEditor]
        public List<Object> targetAssets = new List<Object>();

        [Space(4)]
        [Sirenix.OdinInspector.FolderPath]
        [Sirenix.OdinInspector.BoxGroup("Search Filter")]
        public List<string> searchFolders = new List<string>();


        [Sirenix.OdinInspector.InlineEditor]
        [Space(8)]
        public List<AssetImporter> resultAssets = new List<AssetImporter>();

        #endregion

        #region public methods

        [Sirenix.OdinInspector.Button]
        [Sirenix.OdinInspector.GUIColor(0.2f, 1, 0.2f)]
        public void Search()
        {
            UpdateSearchResults();
        }

        [Sirenix.OdinInspector.Button]
        [Sirenix.OdinInspector.GUIColor(0.2f, 1, 0.2f)]
        public void ApplyImportSettings()
        {
            if (resultAssets.Count == 0) {
                Search();
            }

            AssetEditorTools.ShowProgress(ImportProgressAction());

            var guid = GUID.Generate().ToString();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            
            ClearSearch();
        }

        public void ResetOverrirdes()
        {
            if (resultAssets.Count == 0) {
                Search();
            }
        }        

        public void ClearSearch()
        {
            resultAssets = new List<AssetImporter>();
        }
        
        #endregion

        private IEnumerable<ProgressData> ImportProgressAction()
        {
            var importProgressTemplate = "importing: {0} / {1}";
            var assetsCount            = resultAssets.Count;
            var current                = 0;
            var progress = new ProgressData() {
                IsDone = false,
            };
            
            foreach (var assetImporter in resultAssets) {
                progress.Progress = current / (float)assetsCount;
                progress.Content  = string.Format(importProgressTemplate, current, assetsCount);
                progress.Title    = assetImporter.assetPath;
                yield return progress;
                
                Import(assetImporter);
                
                current++;
            }

            progress.IsDone = true;
            yield return progress;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _psdImporterPlatformsFieldInfo = typeof(PSDImporter).GetField(psdImporterPlatformField, BindingFlags.NonPublic |
                                                                                                    BindingFlags.Instance);
        }

        private void UpdateSearchResults()
        {
            ClearSearch();

            var folderFilters = searchFolders.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            if (folderFilters.Length > 0) {
                var importers = Filter(AssetEditorTools.GetAssetImporters<Object>(folderFilters));
                resultAssets.AddRange(importers);
            }

            var result = Filter(targetAssets.Where(x => x).
                Select(AssetEditorTools.GetAssetImporter)).
                ToList();

            var assetsPaths = new  HashSet<string>();
            resultAssets.AddRange(result);
            resultAssets = resultAssets.
                Where(x => assetsPaths.Add(x.assetPath)).
                ToList();
        }

        private IEnumerable<AssetImporter> Filter(IEnumerable<AssetImporter> importers)
        {
            foreach (var assetImporter in importers) {
                switch (assetImporter) {
                    case TextureImporter textureImporter:
                        if(!importersFilter.IsFlagSet(TextureImporterFilter.TextureImporter))
                            break;
                        yield return textureImporter;
                        break;
                    case PSDImporter psdImporter:
                        if(!importersFilter.IsFlagSet(TextureImporterFilter.PSDImporter))
                            break;
                        yield return psdImporter;
                        break;
                    default:
                        continue;
                }
            }
        }

        public bool IsDefaultTarget(string target)
        {
            return defaultTarget.Equals(target, StringComparison.OrdinalIgnoreCase) || textureImporterDefaultTarget.Equals(target, StringComparison.OrdinalIgnoreCase);
        }

        public string GetDefaultImporterTarget(AssetImporter importer)
        {
            switch (importer) {
                case TextureImporter textureImporter:
                    return textureImporterDefaultTarget;
                case PSDImporter psdImporter:
                    return defaultTarget;
            }

            return defaultTarget;
        }

        private bool Import(AssetImporter assetImporter)
        {
            var result = false;
            
            switch (assetImporter) {
                case TextureImporter textureImporter:
                    result = UpdateTextureImporter(textureImporter);
                    break;
                case PSDImporter psdImporter:
                    result = UpdatePsbImporter(psdImporter);
                    break;
            }

            if (result) {
                assetImporter.MarkDirty();
                assetImporter.SaveAndReimport();
            }

            return result;
        }

        private bool UpdateTextureImporter(TextureImporter textureImporter)
        {
            var target = string.Equals(buildTarget, defaultTarget, StringComparison.OrdinalIgnoreCase) ? 
                textureImporterDefaultTarget : buildTarget;
            var current = textureImporter.GetPlatformTextureSettings(target);
            var result = UpdateSettings(current,platformSettings);
            if (result) {
                textureImporter.SetPlatformTextureSettings(current);
            }
            
            return result;
        }

        private bool UpdatePsbImporter(PSDImporter psdImporter)
        {
            var settings = _psdImporterPlatformsFieldInfo.GetValue(psdImporter) as List<TextureImporterPlatformSettings>;
            var importerPlatformSettings = settings?.FirstOrDefault(x =>
                string.Equals(x.name, buildTarget, StringComparison.OrdinalIgnoreCase));
            if (importerPlatformSettings == null) {
                importerPlatformSettings = new TextureImporterPlatformSettings() {
                    name = buildTarget,
                };
                settings.Add(importerPlatformSettings);
            }

            var result = UpdateSettings(importerPlatformSettings,platformSettings);
            if (result) {
                _psdImporterPlatformsFieldInfo.SetValue(psdImporter, settings);
            }
            
            return result;
        }
        
        private bool UpdateSettings(
            TextureImporterPlatformSettings source, TexturePlatformSettings settings)
        {
            var result = false;
            result |= Select(settings.overriden , source.overridden,x => source.overridden                                              = x);
            result |= Select(settings.textureImporterFormat , source.format,x => source.format                                          = x );
            result |= Select(settings.compressionQuality , source.compressionQuality, x=> source.compressionQuality                     = x);
            result |= Select(settings.сrunchedCompression , source.crunchedCompression, x => source.crunchedCompression                 = x);
            result |= Select(settings.maxTextureSize , source.maxTextureSize,x => source.maxTextureSize                                 = x);
            result |= Select(settings.alphaSplitting , source.allowsAlphaSplitting,x => source.allowsAlphaSplitting                     = x);
            result |= Select(settings.ETC2FallbackOverride , source.androidETC2FallbackOverride,x => source.androidETC2FallbackOverride = x);
            result |= Select(settings.textureCompression , source.textureCompression,x => source.textureCompression                     = x);
            result |= Select(settings.resizeAlgorithm , source.resizeAlgorithm, x => source.resizeAlgorithm                             = x);

            return result;
        }

        private bool Select<T>(IActivatableValue<T> value, T defaultValue,Action<T> setter)
        {
            var targetValue = value.Enabled ? value.Value :defaultValue;
            
            setter?.Invoke(targetValue);
            
            return !object.Equals(value.Value,defaultValue);
        }
    }
}

#endif