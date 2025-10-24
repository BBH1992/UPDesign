using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SKODE
{
    public class DoozyUIWindow_ChangeFront
    {
        public static void ChangeFont(Color frontColor)
        {
            TMP_FontAsset mfont = TMP_Settings.defaultFontAsset;

            string[] guidsAssets = AssetDatabase.FindAssets("", new[] { "Assets" }); // 查找Assets目录下的所有资产

            string doozyFolderPath = string.Empty;

            foreach (var guid in guidsAssets)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (assetPath.Contains("/Doozy/"))
                {
                    doozyFolderPath = assetPath.Substring(0, assetPath.LastIndexOf("/Doozy/") + 7);
                    break;
                }
            }

            if (string.IsNullOrEmpty(doozyFolderPath))
            {
                Debug.LogWarning("Doozy folder not found!");
                return;
            }

            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { doozyFolderPath });
            if (guids != null && guids.Length > 0)
            {
                EditorUtility.DisplayProgressBar("Replacing...", "Start replace", 0);
                int progress = 0;
                foreach (string guid in guids)
                {
                    progress++;
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    EditorUtility.DisplayProgressBar("Replacing....", path, ((float)progress / guids.Length));
                    GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;

                    bool isChanged = false;
                    TextMeshProUGUI[] lblist = obj.GetComponentsInChildren<TextMeshProUGUI>(true);
                    foreach (TextMeshProUGUI item in lblist)
                    {
                        if (item.font.name != mfont.name)
                        {
                            isChanged = true;
                            Debug.Log(obj.name + "===============" + item.font.name, obj);
                            item.font = mfont;
                            item.color = frontColor;

                            //设置 ContentSizeFitter
                            var contentSizeFitter = item.GetComponent<ContentSizeFitter>();
                            if (item.GetComponent<ContentSizeFitter>() == null)
                            {
                                contentSizeFitter = item.gameObject.AddComponent<ContentSizeFitter>();
                            }

                            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                        }
                    }

                    if (isChanged)
                    {
                        EditorUtility.SetDirty(obj);
                    }

                    EditorUtility.ClearProgressBar();
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}