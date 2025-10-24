using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("我显示了");
    }
    private void OnDisable()
    {
        Debug.Log("我隐藏了");

    }


}
