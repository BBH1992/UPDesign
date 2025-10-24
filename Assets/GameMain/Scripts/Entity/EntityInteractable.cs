using HighlightPlus;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(EntitySize))]
[RequireComponent(typeof(HighlightEffect))]
//[RequireComponent(typeof(XRGrabInteractable))]
public class EntityInteractable : MonoBehaviour
{
    public TemplateType TemplateType;
    internal XRBaseInteractable xRInteractable;
    public bool GrabSwitch = true;
    internal HighlightEffect highlightEffect;
    public bool IsHoverEntered
    {
        get;
        set;
    }
    public bool IsSelectEntered
    {
        get;
        set;
    }
    public Action<EntityInteractable, HoverEnterEventArgs> OnHoverEntered;
    public Action<EntityInteractable, HoverExitEventArgs> OnHoverExited;
    public Action<EntityInteractable, SelectEnterEventArgs> OnSelectEntered;
    public Action<EntityInteractable, SelectExitEventArgs> OnSelectExited;
    public Action<EntityInteractable, SelectEnterEventArgs> OnSelectUpdate;
    SelectEnterEventArgs selectEnterEventArgs;

    protected virtual void Awake()
    {
        highlightEffect = GetComponent<HighlightEffect>();
        xRInteractable = GetComponent<XRBaseInteractable>();
        xRInteractable.hoverEntered.AddListener(HoverEntered);
        xRInteractable.hoverExited.AddListener(HoverExited);
        xRInteractable.selectEntered.AddListener(SelectEntered);
        xRInteractable.selectExited.AddListener(SelectExited);

    }
    /// <summary>
    /// 抓取
    /// </summary>
    public virtual void OnGrabItem(HandInteractor handInteractor)
    {
        if (!CommandManger.IsCommandMode)
        {
            handInteractor.InteractionManager.SelectEnter((IXRSelectInteractor)handInteractor.XRBaseInteractor, xRInteractable);

        }
    }
    /// <summary>
    /// 放下
    /// </summary>
    public virtual void OnDropItem(HandInteractor handInteractor)
    {
        if (!CommandManger.IsCommandMode)
        {
            handInteractor.InteractionManager.SelectExit((IXRSelectInteractor)handInteractor.XRBaseInteractor, xRInteractable);
        }
    }

    public void SetHighlight(bool boolean)
    {
        highlightEffect.highlighted = boolean;
    }
    public void HoverEntered_CommandMode(HoverEnterEventArgs e)
    {

        switch (CommandManger.Mode)
        {
            case 1:
                HoverEntered_CommandMode_Mode1(e);

                break;
            case 2:
                HoverEntered_CommandMode_Mode2(e);
                break;
            case 3:
                HoverEntered_CommandMode_Mode3(e);
                break;
        }
    }
    protected virtual void Update()
    {
        //if (CommandManger.Mode == 3)
        //{
        //    if (CommandManger.IsCommandMode)
        //    {
        //        if (CommandManger.CurrentSelectObj != null)
        //        {

        //            //   CommandManger.BurbsItem.SetCollider(false);
        //            //   CommandManger.SetChildsHighlight(true);
        //            //  CommandManger.BurbsItem.SetChildsCollider(true);

        //        }
        //    }
        //    else
        //    {
        //        if (CommandManger.CurrentSelectObj != null)
        //        {
        //            //  CommandManger.SetChildsHighlight(false);
        //            //  CommandManger.BurbsItem.SetChildsCollider(false);
        //            //   CommandManger.BurbsItem.SetCollider(true);
        //        }
        //    }
        //}

        if (IsSelectEntered)
        {
            OnSelectUpdate?.Invoke(this, selectEnterEventArgs);
        }
    }
    private void HoverEntered_CommandMode_Mode3(HoverEnterEventArgs e)
    {
        if (this.transform.parent == null)
        {
            return;
        }
        BurbsItem burbsItem = this.transform.parent.GetComponent<BurbsItem>();

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

    private void HoverEntered_CommandMode_Mode2(HoverEnterEventArgs e)
    {
    }

    private void HoverEntered_CommandMode_Mode1(HoverEnterEventArgs e)
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
    public virtual void HoverEntered(HoverEnterEventArgs e)
    {
        IsHoverEntered = true;
        OnHoverEntered?.Invoke(this, e);
        //  HoverEntered_CommandMode(e);
    }
    public void HoverExited_CommandMode(HoverExitEventArgs e)
    {

    }
    public virtual void HoverExited(HoverExitEventArgs e)
    {
        IsHoverEntered = false;

        OnHoverExited?.Invoke(this, e);
        HoverExited_CommandMode(e);

    }
    public virtual void SelectEntered(SelectEnterEventArgs e)
    {
        IsSelectEntered = true;
        selectEnterEventArgs = e;

        OnSelectEntered?.Invoke(this, e);
    }
    public virtual void SelectExited(SelectExitEventArgs e)
    {
        IsSelectEntered = false;

        OnSelectExited?.Invoke(this, e);
    }

    private void OnTriggerEnter(Collider other)
    {

        //  HoverEntered_CommandMode(new HoverEnterEventArgs());
    }
    protected virtual void OnDestroy()
    {
        xRInteractable.hoverEntered.RemoveListener(HoverEntered);
        xRInteractable.hoverExited.RemoveListener(HoverExited);
        xRInteractable.selectEntered.RemoveListener(SelectEntered);
        xRInteractable.selectExited.RemoveListener(SelectExited);
    }

    public void CloseGrab()
    {
        xRInteractable.enabled = false;
        GrabSwitch = false;
    }
    public void OpenGrab()
    {
        xRInteractable.enabled = true;
        GrabSwitch = true;
    }
}
