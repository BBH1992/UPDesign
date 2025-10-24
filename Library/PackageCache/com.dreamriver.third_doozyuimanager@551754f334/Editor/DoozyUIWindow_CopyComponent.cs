using Doozy.Runtime.UIManager.Animators;
using UnityEditor;
using UnityEngine;

namespace SKODE
{
    public class DoozyUIWindow_CopyComponent : MonoBehaviour
    {
        /// <summary>
        /// 将当前选中的组件复制到每一个Prefab和Hierarchy中
        /// </summary>
        public static void CopyComponent()
        {
            var selects = Selection.gameObjects;
            if (selects.Length == 0 || selects.Length > 1)
            {
                Debug.LogError("请选中一个组件");
                return;
            }

            var select = selects[0];
            if (select.GetComponent<UIContainerUIAnimator>() == null &&
                select.GetComponent<UISelectableUIAnimator>() == null)
            {
                Debug.LogError("当前仅支持UIButton、UIView、UIContainer");
                return;
            }

            string assetsPath = "Assets/";
            var uiContainerUIAnimator = select.GetComponent<UIContainerUIAnimator>();
            var uiSelectableUIAnimator = select.GetComponent<UISelectableUIAnimator>();

            //遍历Hierarchy
            foreach (var o in FindObjectsOfType(typeof(GameObject)))
            {
                var obj = (GameObject)o;

                if (uiContainerUIAnimator != null && obj.GetComponent<UIContainerUIAnimator>() != null)
                {
                    CopyAtoB_UIContainerUIAnimator(uiContainerUIAnimator, obj.GetComponent<UIContainerUIAnimator>());
                }

                if (uiSelectableUIAnimator != null && obj.GetComponent<UISelectableUIAnimator>() != null)
                {
                    CopyAtoB_UISelectableUIAnimator(uiSelectableUIAnimator, obj.GetComponent<UISelectableUIAnimator>());
                }
            }

            // Assets下的所有Prefab
            string[] allPrefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { assetsPath });
            if (allPrefabGuids != null && allPrefabGuids.Length > 0)
            {
                EditorUtility.DisplayProgressBar("Replacing...", "Start replace", 0);
                int progress = 0;

                //遍历所有Prefab
                foreach (string guid in allPrefabGuids)
                {
                    progress++;
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    EditorUtility.DisplayProgressBar("Replacing....", path,
                        ((float)progress / allPrefabGuids.Length));
                    GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;

                    bool isChanged = false;

                    if (uiContainerUIAnimator != null)
                    {
                        var lblist = obj.GetComponentsInChildren<UIContainerUIAnimator>(true);
                        foreach (var item in lblist)
                        {
                            isChanged = true;
                            Debug.Log(obj.name, obj);
                            CopyAtoB_UIContainerUIAnimator(uiContainerUIAnimator, item);
                        }
                    }

                    if (uiSelectableUIAnimator != null)
                    {
                        var lblist = obj.GetComponentsInChildren<UISelectableUIAnimator>(true);
                        foreach (var item in lblist)
                        {
                            isChanged = true;
                            Debug.Log(obj.name, obj);
                            CopyAtoB_UISelectableUIAnimator(uiSelectableUIAnimator, item);
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

        private static void CopyAtoB_UIContainerUIAnimator(UIContainerUIAnimator a, UIContainerUIAnimator b)
        {
            b.showAnimation.Move = a.showAnimation.Move;
            b.showAnimation.Rotate = a.showAnimation.Rotate;
            b.showAnimation.Scale = a.showAnimation.Scale;
            b.showAnimation.Fade = a.showAnimation.Fade;

            b.hideAnimation.Move = a.hideAnimation.Move;
            b.hideAnimation.Rotate = a.hideAnimation.Rotate;
            b.hideAnimation.Scale = a.hideAnimation.Scale;
            b.hideAnimation.Fade = a.hideAnimation.Fade;
        }

        private static void CopyAtoB_UISelectableUIAnimator(UISelectableUIAnimator a, UISelectableUIAnimator b)
        {
            b.normalAnimation.Move = a.normalAnimation.Move;
            b.normalAnimation.Rotate = a.normalAnimation.Rotate;
            b.normalAnimation.Scale = a.normalAnimation.Scale;
            b.normalAnimation.Fade = a.normalAnimation.Fade;

            b.highlightedAnimation.Move = a.highlightedAnimation.Move;
            b.highlightedAnimation.Rotate = a.highlightedAnimation.Rotate;
            b.highlightedAnimation.Scale = a.highlightedAnimation.Scale;
            b.highlightedAnimation.Fade = a.highlightedAnimation.Fade;

            b.pressedAnimation.Move = a.pressedAnimation.Move;
            b.pressedAnimation.Rotate = a.pressedAnimation.Rotate;
            b.pressedAnimation.Scale = a.pressedAnimation.Scale;
            b.pressedAnimation.Fade = a.pressedAnimation.Fade;

            b.selectedAnimation.Move = a.selectedAnimation.Move;
            b.selectedAnimation.Rotate = a.selectedAnimation.Rotate;
            b.selectedAnimation.Scale = a.selectedAnimation.Scale;
            b.selectedAnimation.Fade = a.selectedAnimation.Fade;

            b.disabledAnimation.Move = a.disabledAnimation.Move;
            b.disabledAnimation.Rotate = a.disabledAnimation.Rotate;
            b.disabledAnimation.Scale = a.disabledAnimation.Scale;
            b.disabledAnimation.Fade = a.disabledAnimation.Fade;
        }
    }
}