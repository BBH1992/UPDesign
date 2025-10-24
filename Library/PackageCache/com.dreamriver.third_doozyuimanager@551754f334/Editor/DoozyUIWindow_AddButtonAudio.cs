using Doozy.Runtime.UIManager.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SKODE
{
    public class DoozyUIWindow_AddButtonAudio : MonoBehaviour
    {
        /// <summary>
        /// 给按钮添加音效系统
        /// </summary>
        public static void AddButtonAudioSys()
        {
            //添加音效配置文件
            var va = DoozyUIAudioManager.Data;
            Debug.Log("开始配置按钮音频系统,注意查看是否有配置完成的log...");

            string assetsPath = "Assets/";

            // Assets下的所有Prefab
            string[] allPrefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { assetsPath });
            if (allPrefabGuids != null && allPrefabGuids.Length > 0)
            {
                EditorUtility.DisplayProgressBar("Adding...", "Start replace", 0);
                int progress = 0;

                //遍历所有Prefab
                foreach (string guid in allPrefabGuids)
                {
                    progress++;
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    EditorUtility.DisplayProgressBar("Adding....", path,
                        ((float)progress / allPrefabGuids.Length));

                    GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;

                    // 添加组件
                    var lblist = obj.GetComponentsInChildren<UIButton>(true);
                    foreach (var item in lblist)
                    {
                        if (item.GetComponent<DoozyUIAudio>() == false)
                            item.gameObject.AddComponent<DoozyUIAudio>();
                    }

                    EditorUtility.SetDirty(obj);

                    EditorUtility.ClearProgressBar();
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }

            //遍历Hierarchy
            foreach (GameObject root in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                RefreshButton(root.transform);
                RefreshUIButton(root.transform);
                RefreshUIToggle(root.transform);
                RefreshToggle(root.transform);
            }

            Debug.Log("音频系统配置完成! 配置文件位于Resources中,您可移动到其他Resources文件夹中");
        }

        private static void RefreshButton(Transform parent)
        {
            foreach (Transform child in parent)
            {
                // 检查并添加DoozyUIAudio组件
                var uiButton = child.GetComponent<Button>();
                if (uiButton != null && child.GetComponent<DoozyUIAudio>() == null)
                {
                    child.gameObject.AddComponent<DoozyUIAudio>();
                }

                // 递归检查所有子物体
                RefreshButton(child);
            }
        }

        private static void RefreshUIButton(Transform parent)
        {
            foreach (Transform child in parent)
            {
                // 检查并添加DoozyUIAudio组件
                var uiButton = child.GetComponent<UIButton>();
                if (uiButton != null && child.GetComponent<DoozyUIAudio>() == null)
                {
                    child.gameObject.AddComponent<DoozyUIAudio>();
                }

                // 递归检查所有子物体
                RefreshUIButton(child);
            }
        }

        private static void RefreshUIToggle(Transform parent)
        {
            foreach (Transform child in parent)
            {
                // 检查并添加DoozyUIAudio组件
                var uiButton = child.GetComponent<UIToggle>();
                if (uiButton != null && child.GetComponent<DoozyUIAudio>() == null)
                {
                    child.gameObject.AddComponent<DoozyUIAudio>();
                }

                // 递归检查所有子物体
                RefreshUIToggle(child);
            }
        }

        private static void RefreshToggle(Transform parent)
        {
            foreach (Transform child in parent)
            {
                // 检查并添加DoozyUIAudio组件
                var uiButton = child.GetComponent<Toggle>();
                if (uiButton != null && child.GetComponent<DoozyUIAudio>() == null)
                {
                    child.gameObject.AddComponent<DoozyUIAudio>();
                }

                // 递归检查所有子物体
                RefreshToggle(child);
            }
        }
    }
}