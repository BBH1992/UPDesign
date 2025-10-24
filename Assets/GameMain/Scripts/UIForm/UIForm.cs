using SKODE;
using System.Collections.Generic;
using UnityEngine;

public class UIForm : UIFormBase
{
    public GameObject Panel;
    public GameObject Panel_Command;
    private DreamRiver_LazyFollow item_LazyFollow;
    public List<GameObject> ChildsPanel = new List<GameObject>();
    private void Awake()
    {
        item_LazyFollow = GetComponent<DreamRiver_LazyFollow>();
        CommandManger.Mode = 1;

    }
    private void OnEnable()
    {
        item_LazyFollow.StartFollowing(1f);
    }

    // Start is called before the first frame update
    private void Start()
    {
        OnlyShow(0);
        Panel_Command.SetActive(false);
        item_LazyFollow.StartFollowing(1f);


        // item_LazyFollow.ToggleFollow(false);
    }

    public void CreateEntityWithHeadPoint(string name)
    {
        EntityManger.Instance.InstantiateEntityTemplate(name);
    }

    public void ClickMenu()
    {
        Panel.SetActive(!Panel.activeSelf);
    }

    public void OnlyShow(int index)
    {
        HideAll();
        ChildsPanel[index].SetActive(true);
    }
    public void HideAll()
    {
        foreach (GameObject item in ChildsPanel)
        {
            item.SetActive(false);
        }
    }
    public void ClickSetHandORCtrl()
    {
        HandAndControllM.Instance.SetMode();
    }

    public void ClickCommandMode()
    {
        Panel_Command.SetActive(!Panel_Command.activeSelf);
        if (Panel_Command.activeSelf)
        {
            CommandManger.IsCommandMode = true;
        }
        else
        {
            CommandManger.IsCommandMode = false;
            CommandManger.SetHighlight(false);
            CommandManger.VariantMode = false;
            EntityManger.Instance.CloseDeformation();
            CommandManger.CurrentSelectObj = null;
        }
    }

    public void MoveUp()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.MoveUp();

    }
    public void MoveDown()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.MoveDown();

    }
    public void MoveLeft()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.MoveLeft();

    }
    public void MoveRight()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.MoveRight();

    }
    public void TurnLeft()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.TurnLeft();

    }
    public void TurnRight()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.TurnRight();

    }
    public void Bigger()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.Bigger();

    }
    public void Smaller()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.Smaller();

    }
    public void Copy()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.Copy();

    }
    public void Delete()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.Delete();

    }
    public void AngularCorrection()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.AngularCorrection();

    }

    public void Repeal()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        CommandManger.Instance.Repeal();

    }

    //形变拉伸
    public void Deformations()
    {
        if (CommandManger.CurrentSelectObj == null)
        {
            return;
        }
        if (CommandManger.Variant)
        {
            if (CommandManger.VariantMode)
            {
                CommandManger.VariantMode = false;
                EntityManger.Instance.CloseDeformation();
            }
            else
            {
                CommandManger.VariantMode = true;
                EntityManger.Instance.OpenDeformation(CommandManger.CurrentSelectObj);
            }
        }

    }
    public void SelectDeformations_Event(SetBtnSelectedState setBtnSelectedState)
    {
        if (CommandManger.CurrentSelectObj == null)
        {
            setBtnSelectedState.SetState(true);
            return;
        }
        if (CommandManger.Variant)
        {
            CommandManger.VariantMode = true;
            EntityManger.Instance.OpenDeformation(CommandManger.CurrentSelectObj);
        }
    }
    public void UnSelectDeformations_Event()
    {
        CommandManger.VariantMode = false;
        EntityManger.Instance.CloseDeformation();
    }
    public void UnSelectDeformations(SetBtnSelectedState setBtnSelectedState)
    {
        setBtnSelectedState.SetState(true);
    }

    public void AreaCopy()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.CurrentSelectObj == null)
        {
            return;
        }
        if (CommandManger.Entity_AreaCopy)
        {
            CommandManger.Instance.AreaCopy();

        }

    }
}
