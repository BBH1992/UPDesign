using SKODE;
using UnityEngine;
using UnityEngine.UI;

public class BaiDuVoice : MonoBehaviour
{
    public const string MoveUp = "to the up";
    public const string MoveDown = "to the down";
    public const string MoveLeft = "to the left";
    public const string MoveRight = "to the right";

    public const string TurnLeft = "turn left";
    public const string TurnRight = "turn right";
    public const string Bigger = "bigger";
    public const string Smaller = "smaller";
    public const string Copy = "copy";
    public const string Delete = "delete";
    public const string UIFollow = "ui follow";
    public const string UIOpem = "open ui";
    public const string UIClose = "close ui";
    public const string Undo = "undo";

    private void Awake()
    {

    }
    // Start is called before the first frame update
    private void Start()
    {
#if !UNITY_EDITOR
         BaiDuVoiceManager.Instance.OnRecResult += OnRec;
        Debug.Log(" BaiDuVoiceManager.Instance.OnRecResult添加事件");
#endif


    }
    private void OnDestroy()
    {
#if !UNITY_EDITOR
        BaiDuVoiceManager.Instance.OnRecResult -= OnRec;
#endif


    }
    private void OnRec(RecognitionResult obj)
    {
        if (CommandManger.CurrentSelectObj == null)
        {
            return;
        }

        GameObject Text_Voice = GameObject.Find("Text_Voice");
        if (Text_Voice)
        {
            Text text = Text_Voice.GetComponent<Text>();
            text.text = obj.best_result;
        }

        if (obj.result_type == ResultType.final_result)
        {
            obj.best_result = obj.best_result.ToLower();
            if (obj.best_result == MoveUp)
            {
                CommandManger.Instance.MoveUp();

            }
            else if (obj.best_result == MoveDown)
            {
                CommandManger.Instance.MoveDown();

            }
            else if (obj.best_result == MoveLeft)
            {
                CommandManger.Instance.MoveLeft();

            }
            else if (obj.best_result == MoveRight)
            {
                CommandManger.Instance.MoveRight();

            }
            else if (obj.best_result == TurnLeft)
            {
                CommandManger.Instance.TurnLeft();

            }
            else if (obj.best_result == TurnRight)
            {
                CommandManger.Instance.TurnRight();

            }
            else if (obj.best_result == Bigger)
            {
                CommandManger.Instance.Bigger();

            }
            else if (obj.best_result == Smaller)
            {
                CommandManger.Instance.Smaller();

            }
            else if (obj.best_result == Copy)
            {
                CommandManger.Instance.Copy();

            }
            else if (obj.best_result == Delete)
            {
                CommandManger.Instance.Delete();
            }
            else if (obj.best_result == Undo)
            {
                CommandManger.Instance.Repeal();

            }

        }
    }
}
