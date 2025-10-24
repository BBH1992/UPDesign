using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

public class CommandHand_Brush : MonoBehaviour
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

    private void HandPoseEnd()
    {

    }

    private void HandPoseUpdate(float arg0)
    {

    }

    private void HandPoseStart()
    {
        switch (commandType)
        {
            case CommandType.BrushSizeBigger:
                CommandBrush.Instance.BrushSizeBigger();
                break;
            case CommandType.BrushSizeSmaller:
                CommandBrush.Instance.BrushSizeSmaller();
                break;
            case CommandType.SelectEraser:
                CommandBrush.Instance.SelectEraser();
                break;
            case CommandType.CancelSelectEraser:
                CommandBrush.Instance.CancelSelectEraser();
                break;
            case CommandType.Undo:
                CommandBrush.Instance.Undo();
                break;
            case CommandType.Redo:
                CommandBrush.Instance.Redo();
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
