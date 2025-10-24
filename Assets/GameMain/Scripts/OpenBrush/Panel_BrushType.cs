using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TiltBrush;
using UnityEngine;

public class Panel_BrushType : MonoBehaviour
{
    private List<BrushTypeButton> m_BrushButtons = new List<BrushTypeButton>();
    private List<BrushDescriptor> m_TagFilteredBrushes = new List<BrushDescriptor>();
    public Transform clone;
    public Transform clone_Parent;
    private void Awake()
    {
        SketchControlsScript.Init += Init;


    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDestroy()
    {
        SketchControlsScript.Init -= Init;
        BrushController.m_Instance.BrushSetToDefault -= OnBrushSetToDefault;

        BrushCatalog.m_Instance.BrushCatalogChanged -= OnBrushCatalogChanged;

    }


    void Init()
    {
        BrushController.m_Instance.BrushSetToDefault += OnBrushSetToDefault;

        BrushCatalog.m_Instance.BrushCatalogChanged += OnBrushCatalogChanged;

    }
    private void OnBrushCatalogChanged()
    {

        m_TagFilteredBrushes = BrushCatalog.m_Instance.GetTagFilteredBrushList().ToList();
        m_BrushButtons.Clear();
        foreach (var item in m_TagFilteredBrushes)
        {
            Transform tmp = Instantiate(clone, clone_Parent);
            tmp.gameObject.SetActive(true);
            BrushTypeButton brushTypeButton = tmp.GetComponent<BrushTypeButton>();
            m_BrushButtons.Add(brushTypeButton);
            brushTypeButton.SetButtonProperties(item);
        }
    }

    private void OnBrushSetToDefault()
    {
        BrushDescriptor rDefaultBrush = BrushCatalog.m_Instance.DefaultBrush;
        int index = m_TagFilteredBrushes.IndexOf(rDefaultBrush);
        m_BrushButtons[index].OnSelect();
        //PointerManager.m_Instance.SetAllPointersBrushSize01(0.05f);

    }

    public void UnSelect(BrushTypeButton brushTypeButton)
    {
        foreach (var item in m_BrushButtons)
        {
            if (brushTypeButton != item)
            {
                item.OnUnSelect();
            }
        }
    }
    public void SetBrush(int index)
    {
        if (index > m_BrushButtons.Count - 1 || index < 0)
        {
            return;
        }
        m_BrushButtons[index].OnSelect();
    }
}
