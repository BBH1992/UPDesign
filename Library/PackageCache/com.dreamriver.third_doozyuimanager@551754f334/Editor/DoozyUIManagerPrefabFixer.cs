using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Containers;
using UnityEditor;
using UnityEngine;

public class DoozyUIManagerPrefabFixer : AssetPostprocessor
{
    static DoozyUIManagerPrefabFixer()
    {
        FixPrefab();
    }

    private static void FixPrefab()
    {
        FixViewPrefab();
        FixContainerPrefab();
    }

    private static void FixViewPrefab()
    {
        string[] viewPrefabPaths =
            AssetDatabase.FindAssets("Simple View t:prefab");

        if (viewPrefabPaths.Length == 0)
        {
            return;
        }

        string prefabPath = AssetDatabase.GUIDToAssetPath(viewPrefabPaths[0]);
        GameObject root = PrefabUtility.LoadPrefabContents(prefabPath);
        root.GetComponent<UIView>().OnStartBehaviour = ContainerBehaviour.Show;

        PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
        PrefabUtility.UnloadPrefabContents(root);
    }

    private static void FixContainerPrefab()
    {
        string[] viewPrefabPaths =
            AssetDatabase.FindAssets("Simple Container t:prefab");

        if (viewPrefabPaths.Length == 0)
        {
            return;
        }

        string prefabPath = AssetDatabase.GUIDToAssetPath(viewPrefabPaths[0]);
        GameObject root = PrefabUtility.LoadPrefabContents(prefabPath);
        root.GetComponent<UIContainer>().OnStartBehaviour = ContainerBehaviour.Show;

        PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
        PrefabUtility.UnloadPrefabContents(root);
    }
}