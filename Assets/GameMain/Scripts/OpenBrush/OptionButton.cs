using System.Collections;
using System.Collections.Generic;
using TiltBrush;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    [SerializeField] public SketchControlsScript.GlobalCommands m_Command;
    [SerializeField] public int m_CommandParam = -1;
    [SerializeField] public int m_CommandParam2 = -1;

    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSelect);

    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        bool bWasAvailable = button.interactable;
        bool bAvailable = SketchControlsScript.m_Instance.IsCommandAvailable(m_Command, m_CommandParam);
        if (bWasAvailable != bAvailable)
        {
            button.interactable = bAvailable;
        }
    }
    void OnDestroy()
    {
        button.onClick.RemoveListener(OnSelect);

    }
    public void OnSelect()
    {
        SketchControlsScript.m_Instance.IssueGlobalCommand(m_Command, m_CommandParam,
                 m_CommandParam2);
    }


}
