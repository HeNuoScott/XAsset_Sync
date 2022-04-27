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
            // ������Ҫ�Ƴ�AB��ǩ����Դ���ļ��и�Ŀ¼
            string strNeedRemoveLabelRoot = Application.dataPath + "/";
            // Ŀ¼��Ϣ������Ŀ¼��Ϣ���飬��ʾ���и�Ŀ¼�³���Ŀ¼��
            DirectoryInfo dirTempInfo = new DirectoryInfo(strNeedRemoveLabelRoot);
            DirectoryInfo[] directoryDIRArray = dirTempInfo.GetDirectories();

            // ����������Ŀ¼�����е�Ŀ¼�����ļ�
            foreach (DirectoryInfo currentDir in directoryDIRArray)
            {
                // �ݹ���÷������ҵ��ļ�����ʹ�� AssetImporter �࣬��ǡ��������� ����׺����
                JudgeDirOrFileByRecursive(currentDir);
            }

            // ������õ� AB ���
            AssetDatabase.RemoveUnusedAssetBundleNames();
            // ˢ��
            AssetDatabase.Refresh();

            // ��ʾ��Ϣ����ǰ������
            Debug.Log("AssetBundle ���β����Ƴ�������");
        }

        /// <summary>
        /// �ݹ��ж��ж��Ƿ���Ŀ¼���ļ�
        /// ���ļ����޸� Asset Bundle ���
        /// ��Ŀ¼��������ݹ�
        /// </summary>
        /// <param name="fileSystemInfo">��ǰ�ļ���Ϣ���ļ���Ϣ��Ŀ¼��Ϣ�����໥ת����</param>
        private static void JudgeDirOrFileByRecursive(FileSystemInfo fileSystemInfo)
        {
            // �������
            if (fileSystemInfo.Exists == false)
            {
                Debug.LogError("�ļ�����Ŀ¼���ƣ�" + fileSystemInfo + " �����ڣ�����");
                return;
            }

            // �õ���ǰĿ¼��һ�����ļ���Ϣ����
            DirectoryInfo directoryInfoObj = fileSystemInfo as DirectoryInfo;           // �ļ���ϢתΪĿ¼��Ϣ
            FileSystemInfo[] fileSystemInfoArray = directoryInfoObj.GetFileSystemInfos();

            foreach (FileSystemInfo fileInfo in fileSystemInfoArray)
            {
                FileInfo fileInfoObj = fileInfo as FileInfo;

                // �ļ�����
                if (fileInfoObj != null)
                {
                    // �޸Ĵ��ļ��� AssetBundle ��ǩ
                    RemoveFileABLabel(fileInfoObj);
                }
                // Ŀ¼����
                else
                {

                    // �����Ŀ¼����ݹ����
                    JudgeDirOrFileByRecursive(fileInfo);
                }
            }
        }

        /// <summary>
        /// ���ļ��Ƴ� Asset Bundle ���
        /// </summary>
        /// <param name="fileInfoObj">�ļ����ļ���Ϣ��</param>
        static void RemoveFileABLabel(FileInfo fileInfoObj)
        {
            // ������飨*.meta �ļ���������
            if (fileInfoObj.Extension == ".meta") return;

            // �ű��ļ� ������AssetsBundles 
            if (fileInfoObj.Extension == ".cs") return;

            // ��ȡ��Դ�ļ������·��
            int tmpIndex = fileInfoObj.FullName.IndexOf("Assets");
            string strAssetFilePath = fileInfoObj.FullName.Substring(tmpIndex);        // �õ��ļ����·��

            // ����Դ�ļ��Ƴ� AB ����
            AssetImporter tmpImportObj = AssetImporter.GetAtPath(strAssetFilePath);
            tmpImportObj.assetBundleName = string.Empty;
        }
    }
}