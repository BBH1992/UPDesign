using System.Collections.Generic;
using UnityEngine;

public class EntityManger : MonoBehaviour
{

    public static EntityManger Instance;
    public Dictionary<string, EntityInteractable> EntityDic = new Dictionary<string, EntityInteractable>();
    public Dictionary<string, Transform> TemplateDic = new Dictionary<string, Transform>();
    private Transform entityHeadPoint;

    public Transform EntityHeadPoint
    {
        get
        {
            if (entityHeadPoint == null)
            {
                entityHeadPoint = GameObject.Find("EntityHeadPoint").transform;
            }
            return entityHeadPoint;
        }
    }
    private void Awake()
    {
        Instance = this;
        EntityDic.Clear();
    }

    // Start is called before the first frame update
    private void Start()
    {

    }
    public void OpenDeformation(Transform obj)
    {
        foreach (var item in EntityDic)
        {
            item.Value.CloseGrab();
            if (item.Value.transform == obj)
            {
                Variant variant = item.Value.GetComponentInChildren<Variant>(true);
                if (variant)
                {
                    variant.Show();
                }
            }
        }
    }

    public void CloseDeformation()
    {
        foreach (var item in EntityDic)
        {

            Variant variant = item.Value.GetComponentInChildren<Variant>(true);
            if (variant)
            {
                variant.Hide();
            }
        }
    }
    public void Delete(Transform transform)
    {
        CommandManger.SetHighlight(false);
        GameObject gameObject = CommandManger.CurrentSelectObj.gameObject;
        CommandManger.CurrentSelectObj = null;
        RemoveEntity(gameObject.name);

        Destroy(gameObject);
    }
    public void Copy(Transform transform)
    {
        Transform tmp = CommandManger.CurrentSelectObj;


        if (CommandManger.Mode == 1)
        {
            CommandManger.SetHighlight(false);

            Transform transform1 = Instantiate(CommandManger.CurrentSelectObj);

            Vector3 size = transform1.GetComponent<EntitySize>().GetSize();
            Vector3 pos = new Vector3(CommandManger.CurrentSelectObj.position.x, CommandManger.CurrentSelectObj.position.y, CommandManger.CurrentSelectObj.position.z + size.z);
            transform1.position = pos;
            transform1.parent = CommandManger.CurrentSelectObj.parent;
            CommandManger.CurrentSelectObj = transform1;
            CommandManger.SetHighlight(true);
            AddEntity(transform1);
        }
        else
        {
            CommandManger.SetChildsHighlight(false);
            CommandManger.BurbsItem.SetPointActive(false);
            CommandManger.BurbsItem.SetCollider(true);


            Transform transform1 = Instantiate(CommandManger.CurrentSelectObj);

            Vector3 size = transform1.GetComponent<EntitySize>().GetSize();
            Vector3 pos = new Vector3(CommandManger.CurrentSelectObj.position.x, CommandManger.CurrentSelectObj.position.y, CommandManger.CurrentSelectObj.position.z + size.z);
            transform1.position = pos;
            transform1.parent = CommandManger.CurrentSelectObj.parent;
            CommandManger.CurrentSelectObj = transform1;


            CommandManger.BurbsItem.SetCollider(false);
            CommandManger.BurbsItem.SetPointActive(true);
            CommandManger.SetChildsHighlight(true);
            AddEntity(transform1);

        }
        CommandManger.Instance.Save(CommandType.Copy, tmp);

    }
    public void AreaCopy(Transform transform)
    {
        Transform tmp = CommandManger.CurrentSelectObj;

        if (CommandManger.Mode == 1)
        {

        }
        //  CommandManger.Instance.Save(CommandType.Copy, tmp);

    }


    public Transform InstantiateEntityTemplate(string name, Transform createPoint)
    {
        Transform transform = Instantiate(GetEntityTemplate(name));
        transform.position = createPoint.position;
        transform.rotation = createPoint.rotation;
        AddEntity(transform);
        return transform;

    }
    public Transform InstantiateEntityTemplate(string name, Transform createPoint, string path)
    {
        Transform transform = Instantiate(GetEntityTemplate(name, path));
        transform.position = createPoint.position;
        transform.rotation = createPoint.rotation;
        AddEntity(transform);
        return transform;

    }
    public Transform InstantiateEntityTemplate(string name)
    {
        Transform transform = Instantiate(GetEntityTemplate(name));
        transform.position = EntityHeadPoint.position;
        transform.rotation = Quaternion.identity;

        AddEntity(transform);
        return transform;
    }

    public Transform InstantiateEntityTemplate(string name, string path)
    {
        Transform transform = Instantiate(GetEntityTemplate(name, path));
        transform.position = EntityHeadPoint.position;
        transform.rotation = Quaternion.identity;

        AddEntity(transform);
        return transform;
    }
    public Transform InstantiateEntityTemplate(string name, Vector3 createPoint, Quaternion quaternion)
    {
        Transform transform = Instantiate(GetEntityTemplate(name));
        transform.position = createPoint;
        transform.rotation = quaternion;
        AddEntity(transform);
        return transform;

    }
    public Transform InstantiateEntityTemplate(string name, Vector3 createPoint, Quaternion quaternion, string path)
    {
        Transform transform = Instantiate(GetEntityTemplate(name, path));
        transform.position = createPoint;
        transform.rotation = quaternion;
        AddEntity(transform);
        return transform;

    }

    public EntityInteractable GetEntity(string name)
    {
        EntityInteractable transform = null;
        if (EntityDic.ContainsKey(name))
        {
            transform = EntityDic[name];
        }
        return transform;
    }
    public T GetEntity<T>(string name) where T : MonoBehaviour
    {
        T t = null;
        if (EntityDic.ContainsKey(name))
        {
            t = EntityDic[name].GetComponent<T>();
        }
        return t;
    }
    public void AddEntity(Transform transform)
    {
        if (transform == null)
        {
            return;
        }
        if (!EntityDic.ContainsKey(transform.name))
        {
            EntityInteractable entityInteractable = transform.GetComponent<EntityInteractable>();
            EntityDic.Add(transform.name, entityInteractable);
        }
    }
    public void RemoveEntity(string name)
    {
        EntityDic.Remove(name);
    }
    public Transform GetEntityTemplate(string name)
    {
        Transform transform = null;
        if (TemplateDic.ContainsKey(name))
        {
            transform = TemplateDic[name];
        }
        if (transform == null)
        {
            transform = LoadResourceEntityTemplate(name);
        }
        return transform;
    }
    public Transform GetEntityTemplate(string name, string path)
    {
        Transform transform = null;
        if (TemplateDic.ContainsKey(name))
        {
            transform = TemplateDic[name];
        }
        if (transform == null)
        {
            transform = LoadResourceEntityTemplate(name, path);
        }
        return transform;
    }
    public void AddEntityTemplate(Transform transform)
    {
        if (transform == null)
        {
            return;
        }
        if (!TemplateDic.ContainsKey(transform.name))
        {
            TemplateDic.Add(transform.name, transform);
        }
    }
    public Transform LoadResourceEntityTemplate(string name)
    {
        Transform transform = null;
        transform = Resources.Load<Transform>("Entity/" + name);
        AddEntityTemplate(transform);
        return transform;
    }
    public Transform LoadResourceEntityTemplate(string name, string path)
    {
        Transform transform = null;
        transform = Resources.Load<Transform>(path + "/" + name);
        AddEntityTemplate(transform);
        return transform;
    }
    public void LoadResourceEntityTemplate(string name, ref Transform transform)
    {
        transform = Resources.Load<Transform>("Entity/" + name);
        AddEntityTemplate(transform);
    }
}
