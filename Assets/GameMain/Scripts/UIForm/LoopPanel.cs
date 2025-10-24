using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPanel : MonoBehaviour
{
    private int index = 0;
    public float WaitTime = 0.5f;
    private bool isWait;
    public List<GameObject> Panels = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {

    }

    public void LeftPage()
    {
        if (!isWait)
        {
            isWait = true;
            index--;
            index = index <= 0 ? Panels.Count - 1 : index;
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
            index = index >= Panels.Count ? 0 : index;
            OnlyShow(index);
            StartCoroutine(Wait());
        }

    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitTime);
        isWait = false;

    }
    public void OnlyShow(int index)
    {
        HideAll();
        Panels[index].SetActive(true);
    }
    public void HideAll()
    {
        foreach (GameObject item in Panels)
        {
            item.SetActive(false);
        }
    }
}
