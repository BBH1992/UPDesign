using Doozy.Runtime.UIManager.Animators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMeshProColorTarget : MonoBehaviour
{
    public TMP_Text textMeshPro;
    private Image _image;
    private UIToggleColorAnimator _uiToggleColorAnimator;

    private void Awake()
    {
        _uiToggleColorAnimator = GetComponent<UIToggleColorAnimator>();
        _image = GetComponent<Image>();

        _uiToggleColorAnimator.onAnimation.OnFinishCallback.AddListener(() => { textMeshPro.color = _image.color; });

        _uiToggleColorAnimator.offAnimation.OnFinishCallback.AddListener(() => { textMeshPro.color = _image.color; });
    }
}