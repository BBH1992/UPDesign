using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(EntitySize))]
[RequireComponent(typeof(HighlightEffect))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Entity_1 : EntityInteractable
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void OnGrabItem(HandInteractor handInteractor)
    {
        base.OnGrabItem(handInteractor);

    }
    public override void OnDropItem(HandInteractor handInteractor)
    {
        base.OnDropItem(handInteractor);

    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void HoverEntered(HoverEnterEventArgs e)
    {
        base.HoverEntered(e);
        HoverEntered_CommandMode_Mode(e);
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

    private void HoverEntered_CommandMode_Mode(HoverEnterEventArgs e)
    {
        if (CommandManger.IsCommandMode)
        {
            if (CommandManger.CurrentSelectObj == null)
            {
                CommandManger.CurrentSelectObj = this.transform;
                highlightEffect.highlighted = true;
            }
            else
            {
                if (CommandManger.CurrentSelectObj != this.transform)
                {

                }
                CommandManger.SetHighlight(false);

                CommandManger.CurrentSelectObj = null;
            }
        }
    }
}
