#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SKODE
{
    public class DoozyUIAudioManager
    {
        private static DoozyUIAudioData _data;

        public static DoozyUIAudioData Data
        {
            get
            {
                if (_data == null)
                {
                    _data = Resources.Load<DoozyUIAudioData>($"{typeof(DoozyUIAudioData).Name}");
                    if (_data == null)
                    {
#if UNITY_EDITOR
                        _data = CreateAndSaveDoozyUIAudioData();
#endif
                    }
                }

                return _data;
            }
        }

#if UNITY_EDITOR
        private static DoozyUIAudioData CreateAndSaveDoozyUIAudioData()
        {
            var value = ScriptableObject.CreateInstance<DoozyUIAudioData>();
            var path = $"Assets/Resources/{typeof(DoozyUIAudioData).Name}.asset";
            AssetDatabase.CreateAsset(value, path);
            AssetDatabase.SaveAssets();

            return value;
        }
#endif
    }
}