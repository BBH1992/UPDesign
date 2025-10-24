using K_UnityGF.ColorPalette;
using System;
using System.Collections;
using System.Collections.Generic;
using TiltBrush;
using UnityEngine;
using static TiltBrush.App;

public class PanelColorPicker : MonoBehaviour
{
    public ColorPaletteManager colorPaletteManager;
    private void Awake()
    {
        SketchControlsScript.Init += Init;
    }
    private void Start()
    {



    }
    public void Init()
    {

        colorPaletteManager.Event_UpdateColor += UpdateBrushColor;
    }
    private void UpdateBrushColor(Color color)
    {
        App.BrushColor.SetCurrentColorSilently(color);

    }
    public void SetBrushColor(Color color)
    {
        App.BrushColor.SetCurrentColorSilently(color);
        colorPaletteManager.SetColorAndTarget(color, null);

    }
    private void Update()
    {
        switch (App.CurrentState)
        {
            case AppState.LoadingBrushesAndLighting:

                if (!BrushCatalog.m_Instance.IsLoading
                    && !EnvironmentCatalog.m_Instance.IsLoading
                    && !App.Instance.ShaderWarmup.activeInHierarchy)
                {
                    if (AppAllowsCreation())
                    {

                        colorPaletteManager.SetColorAndTarget(BrushColor.DefaultColor, null);

                    }
                }
                break;
            default:
                break;
        }
    }
    private void OnDestroy()
    {
        SketchControlsScript.Init -= Init;

        colorPaletteManager.Event_UpdateColor -= UpdateBrushColor;

    }
}
