using UnityEngine;
using UnityEngine.UI;

public class UIForm_NurbsCreate : MonoBehaviour
{
    public Slider slider_Row;
    public Slider slider_Column;
    public Slider slider_Dis;
    public Text Text_Row;
    public Text Text_Column;
    public Text Text_Dis;

    // Start is called before the first frame update
    private void Start()
    {
        Text_Row.text = slider_Row.value + "";
        Text_Column.text = slider_Column.value + "";
        Text_Dis.text = slider_Dis.value + "";
        NurbsManager.Instance.row = (int)slider_Row.value;
        NurbsManager.Instance.column = (int)slider_Column.value;
        NurbsManager.Instance.distance = slider_Dis.value;

    }

    public void ValueChanged_Row(float value)
    {
        Text_Row.text = value + "";
        NurbsManager.Instance.row = (int)value;
    }
    public void ValueChanged_Column(float value)
    {
        Text_Column.text = value + "";
        NurbsManager.Instance.column = (int)value;

    }
    public void ValueChanged_Dis(float value)
    {
        Text_Dis.text = value + "";
        NurbsManager.Instance.distance = value;

    }
    public void CreateNurbs()
    {
        NurbsManager.Instance.CreateNurbs("Surfaces");
    }
}
