using System.Collections.Generic;
using UnityEngine;

public class CommandManger : MonoBehaviour
{

    public static CommandManger Instance;

    public static bool IsCommandMode = false;
    public static bool VariantMode = false;

    public static CommandType CurrentCommandType;
    public static Transform CurrentSelectObj;
    public static EntityInteractable EntityInteractable
    {
        get
        {
            return CurrentSelectObj.GetComponent<EntityInteractable>();
        }
    }
    public static BurbsItem BurbsItem
    {
        get
        {
            return CurrentSelectObj.GetComponent<BurbsItem>();
        }
    }
    public static Variant Variant
    {
        get
        {
            return CurrentSelectObj.GetComponentInChildren<Variant>(true);
        }
    }
    public static Entity_AreaCopy Entity_AreaCopy
    {
        get
        {
            return CurrentSelectObj.GetComponent<Entity_AreaCopy>();
        }
    }
    public static void SetHighlight(bool boolean)
    {
        if (CommandManger.CurrentSelectObj != null && EntityInteractable != null)
        {
            CommandManger.EntityInteractable.SetHighlight(boolean);
        }
    }

    public static void SetChildsHighlight(bool boolean)
    {
        if (CommandManger.CurrentSelectObj != null && BurbsItem != null)
        {
            CommandManger.BurbsItem.SetChildsHighlight(boolean);
        }
    }
    public static int Mode = 0;
    public float MoveSpeed = 1;
    public float RoateSpeed = 1;
    public float ScalingSpeed = 1;


    public float minScale = 0.02f;
    public float maxScale = 1;
    private Stack<CommandInfo> CommandCache = new Stack<CommandInfo>();

    //撤销操作缓存
    private Stack<CommandInfo> unDoCache = new Stack<CommandInfo>();
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }
    public void Save(CommandType commandType, Transform last = null)
    {
        if (CurrentSelectObj == null)
        {
            return;
        }
        CommandInfo commandInfo = new CommandInfo
        {
            commandType = commandType
        };
        TransformInfo transformInfo = new TransformInfo
        {
            name = CurrentSelectObj.name,
            transform = CurrentSelectObj,
            position = CurrentSelectObj.position,
            rotation = CurrentSelectObj.rotation,
            scale = CurrentSelectObj.localScale,
            last = last,
            parent = CurrentSelectObj.parent,

            templateType = CurrentSelectObj.GetComponent<EntityInteractable>().TemplateType
        };

        commandInfo.transformInfo = transformInfo;
        CommandCache.Push(commandInfo);
    }
    public void SaveNurbs(CommandType commandType, Transform last = null)
    {
        if (CurrentSelectObj == null)
        {
            return;
        }
        CommandInfo commandInfo = new CommandInfo
        {
            commandType = commandType
        };
        BurbsItem burbsItem = BurbsItem;
        List<Vector3> pos = new List<Vector3>();
        foreach (Transform item in BurbsItem.controlPoints)
        {
            pos.Add(item.localPosition);
        }
        TransformInfo_Nurbs transformInfo = new TransformInfo_Nurbs
        {
            name = CurrentSelectObj.name,
            transform = CurrentSelectObj,
            position = CurrentSelectObj.position,
            rotation = CurrentSelectObj.rotation,
            scale = CurrentSelectObj.localScale,
            last = last,
            parent = CurrentSelectObj.parent,

            templateType = EntityInteractable.TemplateType,
            row = BurbsItem.Row,
            column = BurbsItem.Column,
            dis = BurbsItem.distance,
            material = BurbsItem.material,
            PointsPos = pos
        };

        commandInfo.transformInfo = transformInfo;
        CommandCache.Push(commandInfo);
    }


    /// <summary>
    /// 撤销操作
    /// </summary>
    public void Repeal()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandCache.Count == 0)
        {
            return;
        }
        CommandInfo commandInfo = CommandCache.Pop();
        TransformInfo transformInfo = commandInfo.transformInfo;
        //中间操作
        switch (commandInfo.commandType)
        {
            case CommandType.MoveLeft:
                RepealMoveLeft(transformInfo);
                break;
            case CommandType.MoveRight:
                RepealMoveRight(transformInfo);
                break;
            case CommandType.MoveUp:
                RepealMoveUp(transformInfo);
                break;
            case CommandType.MoveDown:
                RepealMoveDown(transformInfo);
                break;
            case CommandType.TurnLeft:
                RepealTurnLeft(transformInfo);
                break;
            case CommandType.TurnRight:
                RepealTurnRight(transformInfo);
                break;
            case CommandType.Bigger:
                RepealBigger(transformInfo);
                break;
            case CommandType.Smaller:
                RepealSmaller(transformInfo);
                break;
            case CommandType.Copy:
                RepealCopy(transformInfo);
                break;
            case CommandType.Delete:
                if (CommandManger.Mode == 1)
                {
                    RepealDelete(transformInfo);

                }
                else
                {
                    RepealDelete_Nurbs(transformInfo);
                }
                break;
            case CommandType.AngularCorrection:
                RepealAngularCorrection(transformInfo);
                break;
            default:
                break;
        }

        unDoCache.Push(commandInfo);
    }

    /// <summary>
    /// 取消撤销
    /// </summary>
    public void CancelRepeal()
    {
        if (unDoCache.Count == 0)
        {
            return;
        }
        CommandInfo commandInfo = unDoCache.Pop();
        //中间操作
        CommandCache.Push(commandInfo);
    }

    public void MoveLeft()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }

        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.MoveLeft);

            // CommandManger.CurrentSelectObj.Translate(Vector3.left * MoveSpeed);
            Vector3 vector3 = transform.TransformDirection(-Camera.main.transform.right);
            CommandManger.CurrentSelectObj.Translate(vector3 * MoveSpeed, Space.World);
        }
    }
    public void MoveRight()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.MoveRight);

            //   CommandManger.CurrentSelectObj.Translate(Vector3.right * MoveSpeed);
            Vector3 vector3 = transform.TransformDirection(Camera.main.transform.right);

            CommandManger.CurrentSelectObj.Translate(vector3 * MoveSpeed, Space.World);
        }
    }
    public void MoveUp()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.MoveUp);

            CommandManger.CurrentSelectObj.Translate(Vector3.up * MoveSpeed);

        }
    }
    public void MoveDown()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.MoveDown);

            CommandManger.CurrentSelectObj.Translate(Vector3.down * MoveSpeed);

        }
    }

    public void TurnLeft()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.TurnLeft);

            CommandManger.CurrentSelectObj.Rotate(Vector3.up, -CommandManger.Instance.RoateSpeed);

        }
    }
    public void TurnRight()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.TurnRight);

            CommandManger.CurrentSelectObj.Rotate(Vector3.up, CommandManger.Instance.RoateSpeed);

        }
    }
    public void Bigger()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.Bigger);

            Vector3 localScale = CommandManger.CurrentSelectObj.localScale;


            //Vector3 scale = new Vector3(localScale.x + ScalingSpeed * Time.deltaTime,
            //    localScale.y + ScalingSpeed * Time.deltaTime,
            //    localScale.z + ScalingSpeed * Time.deltaTime);
            Vector3 scale = new Vector3(localScale.x + ScalingSpeed,
              localScale.y + ScalingSpeed,
              localScale.z + ScalingSpeed);
            scale.x = Mathf.Clamp(scale.x, minScale, maxScale);
            scale.y = Mathf.Clamp(scale.y, minScale, maxScale);
            scale.z = Mathf.Clamp(scale.z, minScale, maxScale);
            CommandManger.CurrentSelectObj.localScale = scale;

        }

    }
    public void Smaller()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.Smaller);

            Vector3 localScale = CommandManger.CurrentSelectObj.localScale;
            //Vector3 scale = new Vector3(localScale.x - ScalingSpeed * Time.deltaTime,
            //    localScale.y - ScalingSpeed * Time.deltaTime,
            //    localScale.z - ScalingSpeed * Time.deltaTime);
            Vector3 scale = new Vector3(localScale.x - ScalingSpeed,
             localScale.y - ScalingSpeed,
             localScale.z - ScalingSpeed);
            scale.x = Mathf.Clamp(scale.x, minScale, maxScale);
            scale.y = Mathf.Clamp(scale.y, minScale, maxScale);
            scale.z = Mathf.Clamp(scale.z, minScale, maxScale);
            CommandManger.CurrentSelectObj.localScale = scale;

        }

    }

    public void Copy()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            if (CommandManger.Mode == 1)
            {
                EntityManger.Instance.Copy(CommandManger.CurrentSelectObj);

            }
            else
            {
                NurbsManager.Instance.Copy(CommandManger.CurrentSelectObj);

            }
        }
    }
    public void AreaCopy()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            if (CommandManger.Mode == 1)
            {
                CommandManger.Entity_AreaCopy.AreaCopy();
            }
        }
    }
    public void Delete()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            if (CommandManger.Mode == 1)
            {
                Save(CommandType.Delete);
            }
            else
            {
                SaveNurbs(CommandType.Delete);
                NurbsManager.Instance.RemoveBurbsItem(CommandManger.CurrentSelectObj.name);
            }
            EntityManger.Instance.Delete(CommandManger.CurrentSelectObj);

        }
    }
    public void AngularCorrection()
    {
        if (CommandManger.VariantMode == true)
        {
            return;
        }
        if (CommandManger.IsCommandMode && CommandManger.CurrentSelectObj != null)
        {
            Save(CommandType.AngularCorrection);

            CommandManger.CurrentSelectObj.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    //----------------------


    public void RepealMoveLeft(TransformInfo transformInfo)
    {
        transformInfo.transform.position = transformInfo.position;

    }
    public void RepealMoveRight(TransformInfo transformInfo)
    {

        transformInfo.transform.position = transformInfo.position;
    }
    public void RepealMoveUp(TransformInfo transformInfo)
    {

        transformInfo.transform.position = transformInfo.position;
    }
    public void RepealMoveDown(TransformInfo transformInfo)
    {

        transformInfo.transform.position = transformInfo.position;
    }

    public void RepealTurnLeft(TransformInfo transformInfo)
    {

        transformInfo.transform.rotation = transformInfo.rotation;
    }
    public void RepealTurnRight(TransformInfo transformInfo)
    {

        transformInfo.transform.rotation = transformInfo.rotation;
    }
    public void RepealBigger(TransformInfo transformInfo)
    {

        transformInfo.transform.localScale = transformInfo.scale;

    }
    public void RepealSmaller(TransformInfo transformInfo)
    {
        transformInfo.transform.localScale = transformInfo.scale;

    }

    public void RepealCopy(TransformInfo transformInfo)
    {

        if (CommandManger.Mode == 1)
        {
            CommandManger.SetHighlight(false);
            Destroy(transformInfo.transform.gameObject);
            CommandManger.CurrentSelectObj = transformInfo.last;
            CommandManger.SetHighlight(true);
        }
        else
        {
            NurbsManager.Instance.RepealCopy(transformInfo);


        }
    }
    public void RepealDelete(TransformInfo transformInfo)
    {

        CommandManger.SetHighlight(false);

        Transform transform1 = EntityManger.Instance.InstantiateEntityTemplate(transformInfo.templateType.ToString());
        transform1.position = transformInfo.position;
        transform1.rotation = transformInfo.rotation;
        transform1.parent = transformInfo.parent;
        transform1.localScale = transformInfo.scale;



        CommandManger.CurrentSelectObj = transform1;
        CommandManger.SetHighlight(true);

    }
    public void RepealDelete_Nurbs(TransformInfo transformInfo)
    {
        NurbsManager.Instance.RepealDelete_Nurbs(transformInfo);





    }
    public void RepealAngularCorrection(TransformInfo transformInfo)
    {
        transformInfo.transform.rotation = transformInfo.rotation;
    }
}
