using System.IO;
using UnityEditor.Compilation;
using UnityEditor;
using UnityEngine;

namespace SKODE
{
    [InitializeOnLoad]
    public class NativeBaiduVoiceAndroid_GitIgonreFixer
    {
        static NativeBaiduVoiceAndroid_GitIgonreFixer()
        {
            // 将本插件加入到.gitignore中
            FixerGitIgonre();

            //避免刚导入时有报错导致无法初始化
            CompilationPipeline.compilationFinished += OnCompilationFinished;
        }

        private static void OnCompilationFinished(object target)
        {
            FixerGitIgonre();
        }

        /// <summary>
        /// 将本插件更新到.gitignore中
        /// </summary>
        private static void FixerGitIgonre()
        {
            //获取当前脚本的路径
            string scriptPath = DreamRiverEditorExtend.GetScriptAssetPath(nameof(NativeBaiduVoiceAndroid_GitIgonreFixer));
            DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(scriptPath));

            //循环向上递归，查找文件夹名中含有"@"字符的文件夹
            string packagePath = null;
            while (directory != null)
            {
                if (directory.Name.Contains("@"))
                {
                    packagePath = directory.FullName.Replace("\\", "/").Replace(Application.dataPath, "Assets");
                    break;
                }

                directory = directory.Parent;
            }

            if (packagePath != null)
            {
                int startIndex = packagePath.IndexOf("Library/PackageCache/");
                if (startIndex != -1)
                {
                    string desiredPath = packagePath.Substring(startIndex);
                    Assets_Packages.RefreshGitIgnore(desiredPath);
                }
            }
        }
    }
}