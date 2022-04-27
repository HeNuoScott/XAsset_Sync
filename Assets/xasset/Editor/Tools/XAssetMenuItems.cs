using System.IO;
using UnityEditor;
using UnityEngine;

namespace xasset.editor
{
    public static class XAssetMenuItems
    {
        [MenuItem("xasset/Build Bundles", false, 10)]
        public static void BuildBundles()
        {
            BuildScript.BuildBundles();
        }

        [MenuItem("xasset/Build Player", false, 10)]
        public static void BuildPlayer()
        {
            BuildScript.BuildPlayer();
        }

        [MenuItem("xasset/Copy Build to StreamingAssets ", false, 50)]
        public static void CopyBuildToStreamingAssets()
        {
            BuildScript.CopyToStreamingAssets();
        }

        [MenuItem("xasset/Clear Build", false, 800)]
        public static void ClearBuild()
        {
            BuildScript.ClearBuild();
        }

        [MenuItem("xasset/Clear History", false, 800)]
        public static void ClearHistory()
        {
            BuildScript.ClearHistory();
        }

        [MenuItem("xasset/Clear Build from selection", false, 800)]
        public static void ClearBuildFromSelection()
        {
            BuildScript.ClearBuildFromSelection();
        }

        [MenuItem("xasset/Documentation", false, 2000)]
        private static void OpenDocumentation()
        {
            GotoHomepage();
        }

        public static void GotoHomepage(string location = null)
        {
            Application.OpenURL(
                string.IsNullOrEmpty(location) ? "https://xasset.pro" : $"https://xasset.pro/{location}");
        }

        [MenuItem("xasset/File a Bug", false, 2000)]
        public static void FileABug()
        {
            Application.OpenURL("https://github.com/xasset/xasset/issues");
        }
    }
}