using TiltBrush;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAndControllM_OpenBrush : MonoBehaviour
{
    public static HandAndControllM_OpenBrush Instance;
#if UNITY_EDITOR
    public ControllerStyle mode;
#endif
    public InputSystemUIInputModule inputSystemUIInputModule;


    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject LeftControll
    {
        get
        {
            return App.VrSdk.VrControls.Wand.gameObject;
        }
    }
    public GameObject RightControll
    {
        get
        {
            return App.VrSdk.VrControls.Brush.gameObject;
        }
    }
    public GameObject HandPoseGenerator;

    public ControllerStyle Mode
    {
        get
        {

            return App.VrSdk.controllerStyle;
        }
        set
        {
            App.VrSdk.controllerStyle = value;
        }
    }
    public Transform UIPoint_Hand;
    public Transform UIPoint_Ctrl;

    public VrControllers VrControls
    {
        get
        {
            return App.VrSdk.VrControls;
        }
    }
    bool inSelectUI;
    public bool InSelectUI
    {
        get
        {
            return inSelectUI;
        }
    }


    private void Awake()
    {
        Instance = this;

    }
    InputDevice _inputDevice;
    // Start is called before the first frame update
    void Start()
    {
        Mode = ControllerStyle.PicoHands;
        // Mode = ControllerStyle.Unset;
        SetMode(Mode);
        //   _inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }
    public void SetInSelectUIState(bool boolean)
    {
        inSelectUI = boolean;
    }
    private void Update()
    {
        //if (_inputDevice.isValid)
        //{
        //    bool isPressed;
        //    _inputDevice.IsPressed(InputHelpers.Button.GripButton, out isPressed);
        //    if (isPressed)
        //    {
        //        UIForm_Tookit.Instance.LoadScene("Start");

        //    }
        //}

#if UNITY_EDITOR
        Mode = mode;
        SetMode(Mode);

#endif
    }
    public void BackHome()
    {
        Mode = ControllerStyle.Unset;

        SetMode(Mode);
    }
    public void SetMode()
    {
        if (VrControls == null)
        {
            return;
        }
        if (Mode == ControllerStyle.Unset)
        {
            Mode = ControllerStyle.PicoHands;
        }
        else
        {
            Mode = ControllerStyle.Unset;
            CommandManger.IsCommandMode = false;

            CommandManger.SetHighlight(false);
            CommandManger.CurrentSelectObj = null;

        }
        SetMode(Mode);

    }
    public void SetMode(ControllerStyle mode)
    {
        if (VrControls == null)
        {
            return;
        }
        SetLeftMode(mode);
        SetRightMode(mode);

        if (mode == ControllerStyle.PicoHands)
        {
            inputSystemUIInputModule.enabled = false;
            UIPoint_Ctrl.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            UIPoint_Hand.gameObject.SetActive(true);


            HandPoseGenerator?.SetActive(true);

        }
        else
        {

            inputSystemUIInputModule.enabled = true;
            UIPoint_Hand.gameObject.SetActive(false);
            HandPoseGenerator?.SetActive(false);
            UIPoint_Ctrl.localScale = new Vector3(1f, 1f, 1f);




        }
    }
    void SetLeftMode(ControllerStyle mode)
    {
        SafeSetActive(LeftHand, mode == ControllerStyle.PicoHands);
        SafeSetActive(LeftControll, mode == ControllerStyle.Unset);
    }

    void SetRightMode(ControllerStyle mode)
    {
        SafeSetActive(RightHand, mode == ControllerStyle.PicoHands);
        SafeSetActive(RightControll, mode == ControllerStyle.Unset);
    }
    void SafeSetActive(GameObject gameObject, bool active)
    {
        if (gameObject != null && gameObject.activeSelf != active)
            gameObject.SetActive(active);
    }
    public void SelectUI(SelectEnterEventArgs e)
    {
        Debug.Log($"我选中UI了:{e.interactableObject.transform.name}");
    }
    private void OnDisable()
    {


    }
}
