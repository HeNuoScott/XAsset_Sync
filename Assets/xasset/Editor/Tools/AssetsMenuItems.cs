using System.IO;
using UnityEditor;
using UnityEngine;

namespace xasset.editor
{
    public static class AssetsMenuItems
    {
        [MenuItem("Assets/XAsset Label/Check Compute Hash")]
        public static void ComputeHash()
        {
            var target = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(target);
            var hash = Utility.ComputeHash(path);
            Debug.LogFormat("Compute Hash for {0} with {1}", path, hash);
        }

        [MenuItem("Assets/XAsset Label/SetABPack by file")]
        public static void PackByFile()
        {
            foreach (var o in Selection.GetFiltered<Object>(SelectionMode.DeepAssets))
            {
                var assetPath = AssetDatabase.GetAssetPath(o);
                if (string.IsNullOrEmpty(assetPath)) continue;

                if (Directory.Exists(assetPath)) continue;

                var assetImport = AssetImporter.GetAtPath(assetPath);
                var dir = Path.GetDirectoryName(assetPath)?.Replace('\\', '/').Replace('/', '_');
                var name = Path.GetFileNameWithoutExtension(assetPath);
                var type = Path.GetExtension(assetPath);
                assetImport.assetBundleName =
                    $"{dir}_{name}{type}".ToLower().Replace('.', '_') + Settings.BundleExtension;
            }
        }

        [MenuItem("Assets/XAsset Label/SetABPack by dir")]
        public static void PackByDir()
        {
            foreach (var o in Selection.GetFiltered<Object>(SelectionMode.DeepAssets))
            {
                var assetPath = AssetDatabase.GetAssetPath(o);
                if (string.IsNullOrEmpty(assetPath)) continue;

                if (Directory.Exists(assetPath)) continue;

                var assetImport = AssetImporter.GetAtPath(assetPath);
                var dir = Path.GetDirectoryName(assetPath)?.Replace('\\', '/').Replace('/', '_').Replace('.', '_');
                assetImport.assetBundleName = dir + Settings.BundleExtension;
            }
        }

        [MenuItem("Assets/XAsset Label/Clear ABLabel")]
        public static void ClearABLabelPackByDir()
        {
            foreach (var o in Selection.GetFiltered<Object>(SelectionMode.DeepAssets))
            {
                var assetPath = AssetDatabase.GetAssetPath(o);
                if (string.IsNullOrEmpty(assetPath)) continue;

                if (Directory.Exists(assetPath)) continue;

                var assetImport = AssetImporter.GetAtPath(assetPath);
                var dir = Path.GetDirectoryName(assetPath)?.Replace('\\', '/').Replace('/', '_').Replace('.', '_');
                assetImport.assetBundleName = dir + Settings.BundleExtension;
            }
        }
        [MenuItem("Assets/XAsset Label/Clear ABLabel All")]
        public static void RemoveALLABLabel()
        {
            // 定义需要移除AB标签的资源的文件夹根目录
            string strNeedRemoveLabelRoot = Application.dataPath + "/";
            // 目录信息（场景目录信息数组，表示所有根目录下场景目录）
            DirectoryInfo dirTempInfo = new DirectoryInfo(strNeedRemoveLabelRoot);
            DirectoryInfo[] directoryDIRArray = dirTempInfo.GetDirectories();

            // 遍历本场景目录下所有的目录或者文件
            foreach (DirectoryInfo currentDir in directoryDIRArray)
            {
                // 递归调用方法，找到文件，则使用 AssetImporter 类，标记“包名”与 “后缀名”
                JudgeDirOrFileByRecursive(currentDir);
            }

            // 清空无用的 AB 标记
            AssetDatabase.RemoveUnusedAssetBundleNames();
            // 刷新
            AssetDatabase.Refresh();

            // 提示信息，标记包名完成
            Debug.Log("AssetBundle 本次操作移除标记完成");
        }

        /// <summary>
        /// 递归判断判断是否是目录或文件
        /// 是文件，修改 Asset Bundle 标记
        /// 是目录，则继续递归
        /// </summary>
        /// <param name="fileSystemInfo">当前文件信息（文件信息与目录信息可以相互转换）</param>
        private static void JudgeDirOrFileByRecursive(FileSystemInfo fileSystemInfo)
        {
            // 参数检查
            if (fileSystemInfo.Exists == false)
            {
                Debug.LogError("文件或者目录名称：" + fileSystemInfo + " 不存在，请检查");
                return;
            }

            // 得到当前目录下一级的文件信息集合
            DirectoryInfo directoryInfoObj = fileSystemInfo as DirectoryInfo;           // 文件信息转为目录信息
            FileSystemInfo[] fileSystemInfoArray = directoryInfoObj.GetFileSystemInfos();

            foreach (FileSystemInfo fileInfo in fileSystemInfoArray)
            {
                FileInfo fileInfoObj = fileInfo as FileInfo;

                // 文件类型
                if (fileInfoObj != null)
                {
                    // 修改此文件的 AssetBundle 标签
                    RemoveFileABLabel(fileInfoObj);
                }
                // 目录类型
                else
                {

                    // 如果是目录，则递归调用
                    JudgeDirOrFileByRecursive(fileInfo);
                }
            }
        }

        /// <summary>
        /// 给文件移除 Asset Bundle 标记
        /// </summary>
        /// <param name="fileInfoObj">文件（文件信息）</param>
        static void RemoveFileABLabel(FileInfo fileInfoObj)
        {
            // 参数检查（*.meta 文件不做处理）
            if (fileInfoObj.Extension == ".meta") return;

            // 脚本文件 不包含AssetsBundles 
            if (fileInfoObj.Extension == ".cs") return;

            // 获取资源文件的相对路径
            int tmpIndex = fileInfoObj.FullName.IndexOf("Assets");
            string strAssetFilePath = fileInfoObj.FullName.Substring(tmpIndex);        // 得到文件相对路径

            // 给资源文件移除 AB 名称
            AssetImporter tmpImportObj = AssetImporter.GetAtPath(strAssetFilePath);
            tmpImportObj.assetBundleName = string.Empty;
        }
    }
}