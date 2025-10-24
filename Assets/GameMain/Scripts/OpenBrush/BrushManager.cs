using TiltBrush;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class BrushManager : MonoBehaviour
{
    public static BrushManager Instance;
    public HandInteractor brushhand;
    public Transform bi;
    public Transform biPoint;
    public float sizeOffset = 0.01f;
    private void Awake()
    {
        Instance = this;

    }

    // Start is called before the first frame update
    private void Start()
    {
        //设置笔刷语言 0英文 4中文
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }

    public void BrushSizeBigger()
    {
        UpdateSize(sizeOffset);

    }
    public void BrushSizeSmaller()
    {
        UpdateSize(-sizeOffset);
    }
    public void UpdateSize(float fAdjustAmount)
    {
        PointerManager.m_Instance.AdjustAllPointersBrushSize01(fAdjustAmount);
        PointerManager.m_Instance.MarkAllBrushSizeUsed();
    }
}
