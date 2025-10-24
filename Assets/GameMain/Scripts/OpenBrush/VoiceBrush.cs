using SKODE;
using UnityEngine;
using UnityEngine.UI;

public class VoiceBrush : MonoBehaviour
{
    public const string Undo = "undo";
    public const string Redo = "redo";
    public const string SelectEraser = "eraser";
    public const string CancelSelectEraser = "cancel eraser";
    public const string Thicker = "thicker";
    public const string Thinner = "thinner";
    //-------------

    public const string DoubleTaperedFlat = "pinched flat";
    public const string Bubbles = "bubbles";
    public const string DoubleTaperedMarker = "pinched marker";
    public const string Embers = "embers";
    public const string Fire = "fire";
    public const string Flat = "flat";
    public const string Highlighter = "highlighter";
    public const string Light = "light";
    public const string Marker = "marker";
    public const string Rainbow = "rainbow";
    public const string Smoke = "smoke";
    public const string Snow = "snow";
    public const string SoftHighlighter = "soft highlighter";
    public const string Stars = "stars";
    public const string TaperedFlat = "tapered flat";
    public const string TaperedMarker = "tapered marker";
    public const string ThickPaint = "thick paint";
    public const string WetPaint = "wet paint";
    //---------------
    public const string Red = "red";
    public const string Yellow = "yellow";
    public const string Green = "green";
    public const string Blue = "blue";
    public const string White = "white";
    public const string Black = "black";
    public const string Gray = "gray";




    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

        BaiDuVoiceManager.Instance.OnRecResult += OnRec;


    }
    private void OnDestroy()
    {

        BaiDuVoiceManager.Instance.OnRecResult -= OnRec;


    }
    private void OnRec(RecognitionResult obj)
    {
        GameObject Text_Voice = GameObject.Find("Text_Voice");
        if (Text_Voice)
        {
            Text text = Text_Voice.GetComponent<Text>();
            text.text = obj.best_result;
        }
        if (obj.result_type == ResultType.final_result)
        {
            obj.best_result = obj.best_result.ToLower();
            if (obj.best_result == Undo)
            {
                CommandBrush.Instance.Undo();
            }
            else if (obj.best_result == Redo)
            {
                CommandBrush.Instance.Redo();

            }
            else if (obj.best_result == SelectEraser)
            {
                CommandBrush.Instance.SelectEraser();

            }
            else if (obj.best_result == CancelSelectEraser)
            {
                CommandBrush.Instance.CancelSelectEraser();

            }
            else if (obj.best_result == Thicker)
            {
                CommandBrush.Instance.BrushSizeBigger();

            }
            else if (obj.best_result == Thinner)
            {
                CommandBrush.Instance.BrushSizeSmaller();

            }
            else if (obj.best_result == DoubleTaperedFlat)//设置笔刷
            {
                CommandBrush.Instance.SetBrush(10);

            }
            else if (obj.best_result == Bubbles)
            {
                CommandBrush.Instance.SetBrush(17);

            }
            else if (obj.best_result == DoubleTaperedMarker)
            {
                CommandBrush.Instance.SetBrush(6);

            }
            else if (obj.best_result == Embers)
            {
                CommandBrush.Instance.SetBrush(14);

            }
            else if (obj.best_result == Fire)
            {
                CommandBrush.Instance.SetBrush(13);

            }
            else if (obj.best_result == Flat)
            {
                CommandBrush.Instance.SetBrush(8);

            }
            else if (obj.best_result == Highlighter)
            {
                CommandBrush.Instance.SetBrush(7);

            }
            else if (obj.best_result == Light)
            {
                CommandBrush.Instance.SetBrush(12);

            }
            else if (obj.best_result == Marker)
            {
                CommandBrush.Instance.SetBrush(4);

            }
            else if (obj.best_result == Rainbow)
            {
                CommandBrush.Instance.SetBrush(1);

            }
            else if (obj.best_result == Smoke)
            {
                CommandBrush.Instance.SetBrush(15);

            }
            else if (obj.best_result == Snow)
            {
                CommandBrush.Instance.SetBrush(16);

            }
            else if (obj.best_result == SoftHighlighter)
            {
                CommandBrush.Instance.SetBrush(11);

            }
            else if (obj.best_result == Stars)
            {
                CommandBrush.Instance.SetBrush(0);

            }
            else if (obj.best_result == TaperedFlat)
            {
                CommandBrush.Instance.SetBrush(9);

            }
            else if (obj.best_result == TaperedMarker)
            {
                CommandBrush.Instance.SetBrush(5);

            }
            else if (obj.best_result == ThickPaint)
            {
                CommandBrush.Instance.SetBrush(2);

            }
            else if (obj.best_result == WetPaint)
            {
                CommandBrush.Instance.SetBrush(3);
            }
            else if (obj.best_result == Red)
            {
                CommandBrush.Instance.SetBrushColor(Color.red);
            }
            else if (obj.best_result == Yellow)
            {
                CommandBrush.Instance.SetBrushColor(Color.yellow);
            }
            else if (obj.best_result == Green)
            {
                CommandBrush.Instance.SetBrushColor(Color.green);
            }
            else if (obj.best_result == Blue)
            {
                CommandBrush.Instance.SetBrushColor(Color.blue);
            }
            else if (obj.best_result == White)
            {
                CommandBrush.Instance.SetBrushColor(Color.white);
            }
            else if (obj.best_result == Black)
            {
                CommandBrush.Instance.SetBrushColor(Color.black);
            }
            else if (obj.best_result == Gray)
            {
                CommandBrush.Instance.SetBrushColor(Color.gray);
            }
        }
    }
}

