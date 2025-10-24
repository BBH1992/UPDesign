using SKODE;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    public TextSyncer Text_CommandMode;
    public Button Btn_CommandMode;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void CreateEntityWithHeadPoint(string name)
    {
        EntityManger.Instance.InstantiateEntityTemplate(name);
    }

    public void ClickSetHandORCtrl()
    {
        HandAndControllM.Instance.SetMode();
        if (HandAndControllM.Instance.mode == HandAndControllM.Mode.MotionController)
        {
            CommandManger.IsCommandMode = false;
            SetCommandModeBtnState(false);
            CommandManger.SetHighlight(false);
        }
    }

    public void ClickCommandMode()
    {
        CommandManger.IsCommandMode = !CommandManger.IsCommandMode;
        SetCommandModeBtnState(true);
    }
    public void SetCommandModeBtnState(bool boolean)
    {
        Btn_CommandMode.interactable = boolean;

        if (CommandManger.IsCommandMode)
        {
            Text_CommandMode.text = "指令模式已开启";
        }
        else
        {
            Text_CommandMode.text = "指令模式已关闭";

        }
    }
}
