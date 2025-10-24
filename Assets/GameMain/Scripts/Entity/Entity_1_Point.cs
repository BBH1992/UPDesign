using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Entity_1_Point : EntityInteractable
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void OnGrabItem(HandInteractor handInteractor)
    {
        if (CommandManger.IsCommandMode && CommandManger.VariantMode)
        {
            handInteractor.InteractionManager.SelectEnter((IXRSelectInteractor)handInteractor.XRBaseInteractor, xRInteractable);
        }
    }
    public override void OnDropItem(HandInteractor handInteractor)
    {
        if (CommandManger.IsCommandMode && CommandManger.VariantMode)
        {
            handInteractor.InteractionManager.SelectExit((IXRSelectInteractor)handInteractor.XRBaseInteractor, xRInteractable);

        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void HoverEntered(HoverEnterEventArgs e)
    {
        base.HoverEntered(e);
    }

    public override void HoverExited(HoverExitEventArgs e)
    {
        base.HoverExited(e);


    }
    public override void SelectEntered(SelectEnterEventArgs e)
    {
        base.SelectEntered(e);

    }
    public override void SelectExited(SelectExitEventArgs e)
    {
        base.SelectExited(e);
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

}
