using System.Collections;
using System.Collections.Generic;
using TiltBrush;
using UnityEngine;

public class ToolsButton : MonoBehaviour
{
    [SerializeField] private BaseTool.ToolType m_Tool;
    [SerializeField] private bool m_EatGazeInputOnPress = false;
    SetBtnSelectedState SetBtnSelectedState;
    private void Awake()
    {
        SetBtnSelectedState = GetComponent<SetBtnSelectedState>();
        SetBtnSelectedState.SelectEvent.AddListener(OnSelect);
        SetBtnSelectedState.UnSelectEvent.AddListener(UnSelect);

    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDestroy()
    {
        SetBtnSelectedState.SelectEvent.RemoveListener(OnSelect);
        SetBtnSelectedState.UnSelectEvent.RemoveListener(UnSelect);
    }
    public void OnSelect()
    {
        if (m_EatGazeInputOnPress)
        {
            SketchControlsScript.m_Instance.EatGazeObjectInput();
        }
        SketchSurfacePanel.m_Instance.RequestHideActiveTool(true);
        SketchSurfacePanel.m_Instance.EnableSpecificTool(m_Tool);
    }
    public void UnSelect()
    {
        SketchSurfacePanel.m_Instance.DisableSpecificTool(m_Tool);
    }
    public void SetSetState(bool boolean)
    {
        SetBtnSelectedState.SetState(boolean);
    }
}
