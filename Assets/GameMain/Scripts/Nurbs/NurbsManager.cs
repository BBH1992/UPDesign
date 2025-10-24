using HighlightPlus;
using System.Collections.Generic;
using UnityEngine;

public class NurbsManager : MonoBehaviour
{
    public static NurbsManager Instance;
    [Range(3, 20)]
    public int row = 4;
    [Range(3, 20)]
    public int column = 4;
    public float distance = 0.2f;
    public Transform point;
    [Range(1, 3)]
    public int degreeU = 2;
    [Range(1, 3)]
    public int degreeV = 2;

    [Range(1, 50)]
    public int resolutionU = 25;
    [Range(1, 50)]
    public int resolutionV = 25;
    public Material DefaultMaterial;
    public List<Material> Materials = new List<Material>();

    public float scaleMin = 0.5f;
    public float scaleMax = 1f;

    public HighlightProfile highlightProfile;
    public Dictionary<string, BurbsItem> nrusItemDic = new Dictionary<string, BurbsItem>();
    private static int num = 0;
    private void Awake()
    {
        Instance = this;
        nrusItemDic.Clear();
        num = 0;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }
    public void RepealDelete_Nurbs(TransformInfo transformInfo)
    {
        TransformInfo_Nurbs transformInfo_Nurbs = (TransformInfo_Nurbs)transformInfo;
        Transform transform1 = EntityManger.Instance.InstantiateEntityTemplate(transformInfo.templateType.ToString());
        transform1.position = transformInfo.position;
        transform1.rotation = transformInfo.rotation;
        transform1.parent = transformInfo.parent;
        transform1.GetComponent<BurbsItem>().CreateNurbs(transformInfo_Nurbs);
        transform1.name = transformInfo.name;
        transform1.localScale = transformInfo.scale;
        AddBurbsItem(transform1);

        CommandManger.CurrentSelectObj = transform1;
        CommandManger.BurbsItem.SetCollider(false);
        CommandManger.BurbsItem.SetPointActive(true);
        CommandManger.SetChildsHighlight(true);
    }
    public void RepealCopy(TransformInfo transformInfo)
    {
        RemoveBurbsItem(transformInfo.transform.name);

        Destroy(transformInfo.transform.gameObject);
        CommandManger.CurrentSelectObj = transformInfo.last;
        CommandManger.BurbsItem.SetCollider(false);
        CommandManger.BurbsItem.SetPointActive(true);
        CommandManger.SetChildsHighlight(true);
    }
    public void SetCommandMode()
    {
        if (CommandManger.IsCommandMode)
        {
            foreach (KeyValuePair<string, BurbsItem> item in nrusItemDic)
            {
                if (item.Value == null)
                {
                    continue;
                }
                item.Value.SetPointActive(true);
                item.Value.GetComponent<EntityInteractable>().CloseGrab();
                //   item.Value.SetCollider(false);
            }
        }
        else
        {
            foreach (KeyValuePair<string, BurbsItem> item in nrusItemDic)
            {
                if (item.Value == null)
                {
                    continue;
                }
                item.Value.SetPointActive(false);
                item.Value.GetComponent<EntityInteractable>().OpenGrab();

                // item.Value.SetCollider(true);


            }
        }
    }
    public void AddBurbsItem(Transform tmp)
    {
        if (tmp == null)
        {
            return;
        }
        if (!nrusItemDic.ContainsKey(tmp.name))
        {
            BurbsItem nrusItem = tmp.GetComponent<BurbsItem>();
            nrusItemDic.Add(tmp.name, nrusItem);
        }
    }
    public void AddBurbsItem(BurbsItem tmp)
    {

        if (!nrusItemDic.ContainsKey(tmp.name))
        {
            nrusItemDic.Add(tmp.name, tmp);
        }
    }
    public void RemoveBurbsItem(string name)
    {
        nrusItemDic.Remove(name);
    }
    public void Copy(Transform tmp)
    {
        CommandManger.SetChildsHighlight(false);
        Transform transform1 = CreateNurbs(CommandManger.EntityInteractable.TemplateType.ToString());
        float tm = CommandManger.BurbsItem.Row * CommandManger.BurbsItem.distance;
        Vector3 pos = new Vector3(tmp.position.x, tmp.position.y, tmp.position.z + tm);
        transform1.position = pos;
        transform1.parent = tmp.parent;
        transform1.rotation = tmp.rotation;
        num++;
        transform1.name = CommandManger.EntityInteractable.TemplateType + "_" + num;
        AddBurbsItem(transform1);
        CommandManger.CurrentSelectObj = transform1;
        CommandManger.EntityInteractable.xRInteractable.colliders.Clear();
        CommandManger.EntityInteractable.xRInteractable.colliders.Add(CommandManger.BurbsItem.collider);

        CommandManger.BurbsItem.SetCollider(false);
        CommandManger.BurbsItem.SetPointActive(true);
        CommandManger.SetChildsHighlight(true);
        CommandManger.Instance.Save(CommandType.Copy, tmp);

    }

    public void SetMaterial(int num)
    {
        if (CommandManger.CurrentSelectObj)
        {
            CommandManger.BurbsItem.SetMaterial(Materials[num]);
        }
    }
    public Transform CreateNurbs(string name)
    {
        Transform clone = EntityManger.Instance.InstantiateEntityTemplate(name, "Nurbs");
        BurbsItem burbsItem = clone.GetComponent<BurbsItem>();
        num++;
        clone.name = name + "_" + num;
        AddBurbsItem(burbsItem);
        burbsItem.CreateNurbs();
        return clone;
    }
}
