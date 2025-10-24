using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInteractor : MonoBehaviour
{
    internal XRInteractionManager InteractionManager;
    public HandType handType;

    internal XRBaseInteractor XRBaseInteractor;
    internal PXR_Hand xR_Hand;
    private bool isSelectInHand;
    public bool IsSelectInHand
    {
        get
        {
            return isSelectInHand;
        }
    }
    private bool isPinch;
    private EntityInteractable entityInteractable;

    // Start is called before the first frame update

    private void Start()
    {
        if (xR_Hand == null)
        {
            xR_Hand = GetComponentInParent<PXR_Hand>();
        }

        if (XRBaseInteractor == null)
        {
            XRBaseInteractor = GetComponent<XRBaseInteractor>();
        }
        if (InteractionManager == null)
        {
            InteractionManager = XRBaseInteractor.interactionManager;
        }
        XRBaseInteractor.hoverEntered.AddListener(HoverEntered);
        XRBaseInteractor.hoverExited.AddListener(HoverExited);

    }

    // Update is called once per frame
    private void Update()
    {
        isPinch = IsPinchByStrength();

        if (!isPinch && isSelectInHand)
        {
            if (entityInteractable != null && entityInteractable.GrabSwitch)
            {
                isSelectInHand = false;
                entityInteractable.OnDropItem(this);
                entityInteractable = null;
            }
            //松开
            //if (entityInteractable != null && entityInteractable.GrabSwitch && !CommandManger.IsCommandMode)
            //{
            //isSelectInHand = false;

            //InteractionManager.SelectExit((IXRSelectInteractor)XRBaseInteractor, entityInteractable.xRInteractable);
            //entityInteractable = null;
            //}
            if (entityInteractable != null && entityInteractable.GrabSwitch && CommandManger.IsCommandMode && CommandManger.Mode == 3)
            {
                //if (entityInteractable.GetComponent<BurbsItem>())
                //{
                //    return;
                //}
                //isSelectInHand = false;

                //InteractionManager.SelectExit((IXRSelectInteractor)XRBaseInteractor, entityInteractable.xRInteractable);
                //entityInteractable = null;
            }
        }
        else if (isPinch && !isSelectInHand)
        {
            //抓
            if (entityInteractable != null && entityInteractable.GrabSwitch)
            {
                isSelectInHand = true;
                entityInteractable.OnGrabItem(this);
            }
            //if (entityInteractable != null && entityInteractable.GrabSwitch && !CommandManger.IsCommandMode)
            //{
            //    isSelectInHand = true;
            //    InteractionManager.SelectEnter((IXRSelectInteractor)XRBaseInteractor, entityInteractable.xRInteractable);

            //}
            if (entityInteractable != null && entityInteractable.GrabSwitch && CommandManger.IsCommandMode && CommandManger.Mode == 3)
            {
                //if (entityInteractable.GetComponent<BurbsItem>())
                //{
                //    return;
                //}
                //isSelectInHand = true;
                //InteractionManager.SelectEnter((IXRSelectInteractor)XRBaseInteractor, entityInteractable.xRInteractable);
            }
        }

    }
    public bool IsPinch()
    {

        return xR_Hand.Pinch || xR_Hand.PinchStrength > 0.9;
    }
    public bool IsPinchByStrength()
    {
        return xR_Hand.PinchStrength > 0.9;
    }
    public float PinchStrengthInHand()
    {
        if (IsPinch())
        {
            return xR_Hand.PinchStrength;

        }
        return 0;
    }
    public float PinchStrength()
    {
        return xR_Hand.PinchStrength;
    }
    public void HoverEntered(HoverEnterEventArgs e)
    {
        if (isSelectInHand)
        {
            return;
        }
        entityInteractable = e.interactableObject.transform.GetComponent<EntityInteractable>();
    }
    public void HoverExited(HoverExitEventArgs e)
    {
        entityInteractable = null;


    }
    public void SelectEntered(SelectEnterEventArgs e)
    {


    }
    public void SelectExited(SelectExitEventArgs e)
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        if (isSelectInHand)
        {
            return;
        }
        entityInteractable = other.GetComponent<EntityInteractable>();

    }
    private void OnTriggerExit(Collider other)
    {
        entityInteractable = null;

    }
}
