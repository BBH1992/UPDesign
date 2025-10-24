using SKODE;
using System;
using System.Collections;
using System.Collections.Generic;
using TiltBrush;
using UnityEngine;
using UnityEngine.UI;

public class BrushTypeButton : MonoBehaviour
{
    [NonSerialized] public BrushDescriptor m_Brush;
    public GameObject norobj;
    public GameObject selectobj;
    Button button;
    public TextSyncer textSyncer;
    public Text text;

    Panel_BrushType panel_BrushType;
    private void Awake()
    {
        panel_BrushType = GetComponentInParent<Panel_BrushType>();


        button = GetComponent<Button>();
        button.onClick.AddListener(OnSelect);
        // norobj.SetActive(true);
        selectobj.SetActive(false);


    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDestroy()
    {
        button.onClick.RemoveListener(OnSelect);

    }
    public void OnSelect()
    {
        selectobj.SetActive(true);
        BrushController.m_Instance.SetActiveBrush(m_Brush);
        panel_BrushType.UnSelect(this);

    }
    public void OnUnSelect()
    {
        //  norobj.SetActive(true);
        selectobj.SetActive(false);
    }
    public void SetButtonProperties(BrushDescriptor rBrush)
    {
        m_Brush = rBrush;
        text.text = (rBrush.Description);
        textSyncer.text = (rBrush.Description);
    }

}
