using SKODE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIForm_Nurbs
    : UIFormBase
{
    public static UIForm_Nurbs Instance;
    private DreamRiver_LazyFollow item_LazyFollow;

    public List<GameObject> childs = new List<GameObject>();
    private int index = 0;
    public float WaitTime = 0.5f;
    private bool isWait;
    private void Awake()
    {
        Instance = this;
        item_LazyFollow = GetComponent<DreamRiver_LazyFollow>();
        CommandManger.Mode = 3;

    }
    private void OnEnable()
    {
        item_LazyFollow.StartFollowing(1f);
    }

    // Start is called before the first frame update
    private void Start()
    {

    }
    public void SetMaterial(int num)
    {
        NurbsManager.Instance.SetMaterial(num);
    }
    public void ClickCommandMode(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            CommandManger.IsCommandMode = true;
        }
        else
        {
            CommandManger.IsCommandMode = false;
            //CommandManger.SetChildsHighlight(false);
            //if (CommandManger.BurbsItem)
            //{
            //    CommandManger.BurbsItem.SetChildsCollider(false);
            //}

            //CommandManger.CurrentSelectObj = null;
        }
        NurbsManager.Instance.SetCommandMode();
    }
    public void CreateRectangle1()
    {
        NurbsManager.Instance.CreateNurbs("Rectangle1");
    }
    public void CreateSquare1()
    {
        NurbsManager.Instance.CreateNurbs("Square1");
    }
    public void CreateSquare2()
    {
        NurbsManager.Instance.CreateNurbs("Square2");
    }
    public void OnlyShow(int index)
    {
        HideAll();
        childs[index].SetActive(true);
    }
    public void HideAll()
    {
        foreach (GameObject item in childs)
        {
            item.SetActive(false);
        }
    }
    public void LeftPage()
    {
        if (!isWait)
        {
            isWait = true;
            index--;
            index = index <= 0 ? childs.Count - 1 : index;
            OnlyShow(index);
            StartCoroutine(Wait());
        }


    }
    public void RightPage()
    {
        if (!isWait)
        {
            isWait = true;

            index++;
            index = index >= childs.Count ? 0 : index;
            OnlyShow(index);
            StartCoroutine(Wait());
        }

    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitTime);
        isWait = false;

    }
    public void MoveUp()
    {
        CommandManger.Instance.MoveUp();

    }
    public void MoveDown()
    {
        CommandManger.Instance.MoveDown();

    }
    public void MoveLeft()
    {
        CommandManger.Instance.MoveLeft();

    }
    public void MoveRight()
    {
        CommandManger.Instance.MoveLeft();

    }
    public void TurnLeft()
    {
        CommandManger.Instance.MoveLeft();

    }
    public void TurnRight()
    {
        CommandManger.Instance.TurnRight();

    }
    public void Bigger()
    {
        CommandManger.Instance.Bigger();

    }
    public void Smaller()
    {
        CommandManger.Instance.Smaller();

    }
    public void Copy()
    {
        CommandManger.Instance.Copy();

    }
    public void Delete()
    {
        CommandManger.Instance.Delete();

    }
    public void AngularCorrection()
    {
        CommandManger.Instance.AngularCorrection();

    }

    public void Repeal()
    {
        CommandManger.Instance.Repeal();

    }
}
