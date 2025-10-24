using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIForm_Tookit : UIFormBase
{
    public static UIForm_Tookit Instance;
    UIForm_Follow UIForm_Follow;
    public List<GameObject> childs = new List<GameObject>();
    int index = 0;
    public float WaitTime = 0.5f;
    bool isWait;
    public ToolsButton ToolsButton_Eraser;
    public OptionButton OptionButton_Undo;
    public OptionButton OptionButton_Redo;
    public PanelColorPicker PanelColorPicker;
    public Panel_BrushType Panel_BrushType;
    private void Awake()
    {
        Instance = this;
        UIForm_Follow = GetComponent<UIForm_Follow>();

    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetPos", 1);
    }
    private void OnEnable()
    {
        UIForm_Follow.SetPos();
    }
    void SetPos()
    {
        UIForm_Follow.SetPos();

    }
    public void BackHome()
    {
        HandAndControllM_OpenBrush.Instance.BackHome();
        LoadScene("Start");
    }

    public void OnlyShow(int id)
    {
        foreach (var item in childs)
        {
            item.SetActive(false);
        }
        childs[id]?.SetActive(true);
    }
    public void Hide(int id)
    {
        childs[id]?.SetActive(false);
    }
    public void HideAll()
    {
        foreach (var item in childs)
        {
            item.SetActive(false);
        }
    }
    public void SetBrushColor(Color color)
    {
        PanelColorPicker.SetBrushColor(color);

    }
    public void SetBrush(int index)
    {
        Panel_BrushType.SetBrush(index);
    }
    public void SelectEraser()
    {
        ToolsButton_Eraser.SetSetState(false);
    }
    public void CancelSelectEraser()
    {
        ToolsButton_Eraser.SetSetState(true);

    }
    public void Undo()
    {
        OptionButton_Undo.OnSelect();

    }
    public void Redo()
    {
        OptionButton_Redo.OnSelect();

    }

    public void BrushSizeBigger()
    {
        BrushManager.Instance.BrushSizeBigger();
    }
    public void BrushSizeSmaller()
    {
        BrushManager.Instance.BrushSizeSmaller();

    }
    public void LeftPage()
    {
        if (!isWait)
        {
            isWait = true;
            index--;
            index = index <= 0 ? childs.Count - 1 : index;
            OnlyShow(index);
            StartCoroutine(Wait());
        }


    }
    public void RightPage()
    {
        if (!isWait)
        {
            isWait = true;

            index++;
            index = index >= childs.Count ? 0 : index;
            OnlyShow(index);
            StartCoroutine(Wait());
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitTime);
        isWait = false;

    }
}
