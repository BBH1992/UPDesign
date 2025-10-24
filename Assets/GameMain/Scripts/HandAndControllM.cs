using Unity.XR.PXR;
using UnityEngine;

public class HandAndControllM : MonoBehaviour
{
    public static HandAndControllM Instance;
    public enum Mode
    {

        MotionController,
        TrackedHand,
    }
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject LeftControll;
    public GameObject RightControll;
    public GameObject HandPoseGenerator;

    public Mode mode;
    public Transform UI;
    public Transform UIPoint_Hand;
    public Transform UIPoint_Ctrl;


    private void Awake()
    {
        Instance = this;

    }

    // Start is called before the first frame update
    private void Start()
    {
        PXR_Plugin.System.InputDeviceChanged += OnDeviceChange;
        SetMode(Mode.TrackedHand);
    }
    private void Update()
    {
#if UNITY_EDITOR
        SetMode(mode);
#endif
    }
    public void SetMode()
    {
        if (mode == Mode.MotionController)
        {
            mode = Mode.TrackedHand;
        }
        else
        {
            mode = Mode.MotionController;
            CommandManger.IsCommandMode = false;

            CommandManger.SetHighlight(false);
            CommandManger.CurrentSelectObj = null;

        }
        SetMode(mode);

    }
    public void SetMode(Mode mode)
    {
        SetLeftMode(mode);
        SetRightMode(mode);
        if (mode == Mode.TrackedHand)
        {
            UI.SetParent(UIPoint_Hand);
            UI.localPosition = new Vector3(0, 0, 0);
            UI.localRotation = Quaternion.Euler(90, 0, 0);
            UI.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            HandPoseGenerator?.SetActive(true);

        }
        else
        {
            UI.SetParent(UIPoint_Ctrl);
            UI.localPosition = new Vector3(0, 0, 0);
            UI.localRotation = Quaternion.Euler(90, 0, 0);
            UI.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            HandPoseGenerator?.SetActive(false);

        }
    }

    private void SetLeftMode(Mode mode)
    {
        SafeSetActive(LeftHand, mode == Mode.TrackedHand);
        SafeSetActive(LeftControll, mode == Mode.MotionController);
    }

    private void SetRightMode(Mode mode)
    {
        SafeSetActive(RightHand, mode == Mode.TrackedHand);
        SafeSetActive(RightControll, mode == Mode.MotionController);
    }

    private void SafeSetActive(GameObject gameObject, bool active)
    {
        if (gameObject != null && gameObject.activeSelf != active)
        {
            gameObject.SetActive(active);
        }
    }

    private void OnDeviceChange(int id)
    {
        Debug.Log("输入源发生变化:" + id);
    }
    private void OnDisable()
    {
        PXR_Plugin.System.InputDeviceChanged -= OnDeviceChange;

    }
}
