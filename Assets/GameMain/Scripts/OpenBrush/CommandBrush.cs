using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBrush : MonoBehaviour
{
    public static CommandBrush Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void BrushSizeBigger()
    {
        UIForm_Tookit.Instance.BrushSizeBigger();
    }
    public void BrushSizeSmaller()
    {
        UIForm_Tookit.Instance.BrushSizeSmaller();

    }

    public void SelectEraser()
    {
        UIForm_Tookit.Instance.SelectEraser();

    }
    public void CancelSelectEraser()
    {
        UIForm_Tookit.Instance.CancelSelectEraser();
    }
    public void Undo()
    {
        UIForm_Tookit.Instance.Undo();

    }
    public void Redo()
    {
        UIForm_Tookit.Instance.Redo();

    }
    public void SetBrushColor(Color color)
    {
        UIForm_Tookit.Instance.SetBrushColor(color);

    }
    public void SetBrush(int index)
    {
        UIForm_Tookit.Instance.SetBrush(index);
    }
}
