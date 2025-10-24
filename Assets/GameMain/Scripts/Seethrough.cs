using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

public class Seethrough : MonoBehaviour
{
    private void Awake()
    {
        PXR_MixedReality.EnableVideoSeeThrough(true);

    }
    private void Start()
    {
        RenderSettings.skybox = null;
    }
    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            PXR_MixedReality.EnableVideoSeeThrough(true);
        }
    }
}
