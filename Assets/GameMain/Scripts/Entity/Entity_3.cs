using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Entity_3 : EntityInteractable
{
    BurbsItem burbsItem;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void OnGrabItem(HandInteractor handInteractor)
    {
        base.OnGrabItem(handInteractor);

        if (CommandManger.IsCommandMode && CommandManger.Mode == 3)
        {
            if (GetComponent<BurbsItem>())
            {
                return;
            }
            handInteractor.InteractionManager.SelectEnter((IXRSelectInteractor)handInteractor.XRBaseInteractor, xRInteractable);
        }
    }
    public override void OnDropItem(HandInteractor handInteractor)
    {
        base.OnDropItem(handInteractor);

        if (CommandManger.IsCommandMode && CommandManger.Mode == 3)
        {
            if (GetComponent<BurbsItem>())
            {
                return;
            }
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
        HoverEntered_CommandMode_Mode(e);
    }

    public override void HoverExited(HoverExitEventArgs e)
    {
        base.HoverExited(e);


    }
    public override void SelectEntered(SelectEnterEventArgs e)
    {
        base.SelectEntered(e);
        burbsItem.GetComponent<MeshRenderer>().enabled = false;
    }
    public override void SelectExited(SelectExitEventArgs e)
    {
        base.SelectExited(e);
        burbsItem.GetComponent<MeshRenderer>().enabled = true;

    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void HoverEntered_CommandMode_Mode(HoverEnterEventArgs e)
    {
        if (this.transform.parent == null)
        {
            return;
        }
        burbsItem = this.transform.parent.GetComponent<BurbsItem>();

        if (CommandManger.IsCommandMode && burbsItem != null)
        {
            if (CommandManger.CurrentSelectObj == null)
            {
                CommandManger.CurrentSelectObj = this.transform.parent;
                CommandManger.BurbsItem.SetChildsHighlight(true);
            }
            else
            {

                CommandManger.BurbsItem.SetChildsHighlight(false);

                CommandManger.CurrentSelectObj = null;
            }

        }
    }
}
