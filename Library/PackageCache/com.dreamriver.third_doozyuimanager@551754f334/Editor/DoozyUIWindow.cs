using UnityEditor;
using UnityEngine;

namespace SKODE
{
    [InitializeOnLoad]
    public static class DoozyUIWindow
    {
        // 折叠标题的状态
        private static bool _foldout;

        // 初始颜色设为黑色
        private static Color _selectedColor = Color.black;

        //是否覆盖已有的音效
        private static bool _isOverride = true;

        private static string _buttonCategory = "None";

        static DoozyUIWindow()
        {
            WindowCore.OnDrawGUI += DrawGUI;
        }

        private static void DrawGUI()
        {
            GUILayout.BeginVertical("box"); // 开始一个垂直布局
            GUILayout.Space(5);

            // 折叠标题
            _foldout = EditorGUILayout.Foldout(_foldout, "DoozyUI", true);
            GUILayout.Space(5);

            if (_foldout)
            {
                // 定义背景板的样式
                var backgroundStyle = new GUIStyle(GUI.skin.box)
                {
                    padding = new RectOffset(10, 10, 10, 10),
                    margin = new RectOffset(10, 10, 10, 10),
                    normal = { background = Texture2D.grayTexture }
                };

                DrawCopyComponent(backgroundStyle);
                DrawChangeFront(backgroundStyle);
                AddButtonAudio(backgroundStyle);
            }

            GUILayout.Space(10); // 底部空间
            GUILayout.EndVertical(); // 结束垂直布局
        }

        /// <summary>
        /// 将当前选中的组件复制到每一个Prefab和Hierarchy中
        /// </summary>
        private static void DrawCopyComponent(GUIStyle backgroundStyle)
        {
            // 开始背景板
            EditorGUILayout.BeginVertical(backgroundStyle);
            GUILayout.Label("将当前选中的组件复制到每一个Prefab和Hierarchy中\n支持UISelectableUIAnimator、UIContainerUIAnimator", EditorStyles.boldLabel);
            GUILayout.Space(5);
            if (GUILayout.Button("确定", GUILayout.Height(25), GUILayout.Width(50)))
            {
                DoozyUIWindow_CopyComponent.CopyComponent();
            }

            // 结束背景板
            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// 设置Doozy字体为TMP默认字体,字体颜色为黑色,字体增加ContentSizeFitter
        /// </summary>
        private static void DrawChangeFront(GUIStyle backgroundStyle)
        {
            // 开始背景板
            EditorGUILayout.BeginVertical(backgroundStyle);
            GUILayout.Label("设置Doozy字体:TMP默认字体,增加ContentSizeFitter", EditorStyles.boldLabel);
            GUILayout.Space(5);

            // 颜色选择器
            _selectedColor =
                EditorGUILayout.ColorField("选择颜色", _selectedColor, GUILayout.Width(300));
            GUILayout.Space(5);
            if (GUILayout.Button("确定", GUILayout.Height(25), GUILayout.Width(50)))
            {
                DoozyUIWindow_ChangeFront.ChangeFont(_selectedColor);
            }

            // 结束背景板
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 给按钮添加并更新音效
        /// </summary>
        private static void AddButtonAudio(GUIStyle backgroundStyle)
        {
            // 开始背景板
            EditorGUILayout.BeginVertical(backgroundStyle);
            GUILayout.Label($"给button、uibutton、toggle、uitoggle添加音效系统\n音频配置文件位于Resources中", EditorStyles.boldLabel);
            GUILayout.Space(5);

            if (GUILayout.Button("确定", GUILayout.Height(25), GUILayout.Width(50)))
            {
                DoozyUIWindow_AddButtonAudio.AddButtonAudioSys();
            }

            // 结束背景板
            EditorGUILayout.EndVertical();
        }
    }
}