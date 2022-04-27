using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace xasset.editor
{
    [CreateAssetMenu(menuName = "XAsset/Settings", fileName = "Settings", order = 100)]
    public sealed class Settings : ScriptableObject
    {
        public static string BundleExtension { get; set; } = ".bundle";

        /// <summary>
        ///     采集资源或依赖需要过滤掉的文件
        /// </summary>
        [Header("Bundle")]
        [Tooltip("采集资源或依赖需要过滤掉的文件")]
        public List<string> excludeFiles =
            new List<string>
            {
                ".spriteatlas",
                ".giparams",
                "LightingData.asset"
            };

        /// <summary>
        ///     播放器的运行模式。Preload 模式不更新资源，并且打包的时候会忽略分包配置。
        /// </summary>
        [Header("PlayMode")]
        [Tooltip("播放器的运行模式")] 
        public ScriptPlayMode scriptPlayMode = ScriptPlayMode.Simulation;

        public bool requestCopy;

        public static List<string> ExcludeFiles { get; private set; }

        /// <summary>
        ///     打包输出目录
        /// </summary>
        public static string PlatformBuildPath
        {
            get
            {
                var dir = $"{Utility.buildPath}/{GetPlatformName()}";
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                return dir;
            }
        }

        /// <summary>
        ///     安装包资源目录, 打包安装包的时候会自动根据分包配置将资源拷贝到这个目录
        /// </summary>
        public static string BuildPlayerDataPath => $"{Application.streamingAssetsPath}/{Utility.buildPath}";

        public void Initialize()
        {
            ExcludeFiles = excludeFiles;
        }

        public static Settings GetDefaultSettings()
        {
            return EditorUtility.FindOrCreateAsset<Settings>("Assets/xasset/Settings.asset");
        }

        /// <summary>
        ///     获取包含在安装包的资源
        /// </summary>
        /// <returns></returns>
        public List<ManifestBundle> GetBundlesInBuild(BuildVersions versions)
        {
            var bundles = new List<ManifestBundle>();
            foreach (var version in versions.data)
            {
                var manifest = Manifest.LoadFromFile(GetBuildPath(version.file));
                bundles.AddRange(manifest.bundles);
            }

            return bundles;
        }

        public static string GetBuildPath(string file)
        {
            return $"{PlatformBuildPath}/{file}";
        }

        public static string GetPlatformName()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.StandaloneOSX:
                    return "OSX";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.WebGL:
                    return "WebGL";
                default:
                    return Utility.nonsupport;
            }
        }

        public static bool IsExcluded(string path)
        {
            return ExcludeFiles.Exists(path.EndsWith) || path.EndsWith(".cs") || path.EndsWith(".dll");
        }

        public static IEnumerable<string> GetDependencies(string path)
        {
            var set = new HashSet<string>(AssetDatabase.GetDependencies(path, true));
            set.Remove(path);
            set.RemoveWhere(IsExcluded);
            return set.ToArray();
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Settings))]
    public class SettingsEditor : Editor
    {
        GUIStyle style;

        private void Awake()
        {
            style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 16;
            style.fontStyle = FontStyle.Bold;
            //<color=#00ffffff>text</color>
            style.richText = true;
        }

        public override void OnInspectorGUI()
        {
            Settings settings = Settings.GetDefaultSettings();
            string label = "";
            switch (settings.scriptPlayMode)
            {
                case ScriptPlayMode.Simulation:
                    label = "<color=#00ff00>仿真模式，可以不打包快速运行，不触发版本更新</color>";
                    break;
                case ScriptPlayMode.Preload:
                    label = "<color=#ffff00>预加载模式，需要先打包，不触发版本更新</color>";
                    break;
                case ScriptPlayMode.Increment:
                    label = "<color=#ffff00>增量模式，需要先打包，可以在编辑器调试真机热更加载逻辑</color>";
                    break;
                default: break;
            }
            GUILayout.Label(label, style);
            base.OnInspectorGUI();
        }
    }
#endif
}