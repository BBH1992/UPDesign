using UnityEngine;

namespace SKODE
{
    [CreateAssetMenu(fileName = "DoozyUIAudioData", menuName = "DreamRiver/DoozyUIAudioData")]
    public class DoozyUIAudioData : ScriptableObject
    {
        [Header("按钮音效")] public AudioClip buttonPressed;
        public AudioClip buttonHighlighted;

        [Space(10), Header("toggle音效")] public AudioClip togglePressed;
        public AudioClip toggleHighlighted;
    }
}