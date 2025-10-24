using Unity.XR.PXR;
using UnityEngine;

public enum HandCommand
{
    MoveLeft,
    MoveRight,
    MoveUp,
    MoveDown
}
public class CommandHand : MonoBehaviour
{
    PXR_HandPose pXR_HandPose;
    public CommandType commandType;

    void Awake()
    {
        pXR_HandPose = GetComponent<PXR_HandPose>();
    }
    private void Start()
    {
        pXR_HandPose.handPoseStart.AddListener(HandPoseStart);
        pXR_HandPose.handPoseUpdate.AddListener(HandPoseUpdate);
        pXR_HandPose.handPoseEnd.AddListener(HandPoseEnd);
    }
    void MoveLeft()
    {
        CommandManger.Instance.MoveLeft();
    }
    void MoveRight()
    {
        CommandManger.Instance.MoveRight();

    }
    void MoveUp()
    {
        CommandManger.Instance.MoveUp();

    }
    void MoveDown()
    {
        CommandManger.Instance.MoveDown();

    }
    public void TurnLeft()
    {
        CommandManger.Instance.TurnLeft();

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
    private void HandPoseEnd()
    {

    }

    private void HandPoseUpdate(float arg0)
    {
        return;
        switch (commandType)
        {
            case CommandType.MoveLeft:
                MoveLeft();
                break;
            case CommandType.MoveRight:
                MoveRight();
                break;
            case CommandType.MoveUp:
                MoveUp();
                break;
            case CommandType.MoveDown:
                MoveDown();
                break;
            case CommandType.TurnLeft:
                TurnLeft();
                break;
            case CommandType.TurnRight:
                TurnRight();
                break;
            case CommandType.Bigger:
                Bigger();
                break;
            case CommandType.Smaller:
                Smaller();
                break;
            case CommandType.Copy:
                Copy();
                break;
            case CommandType.Delete:
                Delete();
                break;
            case CommandType.AngularCorrection:
                break;
            default:
                break;
        }
    }

    private void HandPoseStart()
    {
        switch (commandType)
        {
            case CommandType.MoveLeft:
                MoveLeft();
                break;
            case CommandType.MoveRight:
                MoveRight();
                break;
            case CommandType.MoveUp:
                MoveUp();
                break;
            case CommandType.MoveDown:
                MoveDown();
                break;
            case CommandType.TurnLeft:
                TurnLeft();
                break;
            case CommandType.TurnRight:
                TurnRight();
                break;
            case CommandType.Bigger:
                Bigger();
                break;
            case CommandType.Smaller:
                Smaller();
                break;

            case CommandType.Copy:
                Copy();
                break;
            case CommandType.Delete:
                Delete();
                break;
            case CommandType.Rotate:
                break;
            default:
                break;
        }

    }
    private void OnDestroy()
    {
        pXR_HandPose.handPoseStart.RemoveListener(HandPoseStart);
        pXR_HandPose.handPoseUpdate.RemoveListener(HandPoseUpdate);
        pXR_HandPose.handPoseEnd.RemoveListener(HandPoseEnd);
    }
}
