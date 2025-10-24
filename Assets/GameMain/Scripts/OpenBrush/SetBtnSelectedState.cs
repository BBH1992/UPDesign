/*
* FileName:          SetBtnSelectedState
* CompanyName:       
* Author:            relly
* Description:       
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class SetBtnSelectedState : MonoBehaviour
{

    public GameObject SelectImg;
    Button button;


    bool IsSelected;

    public UnityEvent SelectEvent;
    public UnityEvent UnSelectEvent;


    private void Awake()
    {

        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(SetState);

        SelectImg?.SetActive(false);

    }

    private void Start()
    {

    }

    public void SetState()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            SelectImg?.SetActive(true);
            SelectEvent?.Invoke();

        }
        else
        {
            IsSelected = false;
            SelectImg?.SetActive(false);
            UnSelectEvent?.Invoke();
        }
    }
    public void SetState(bool boolean)
    {
        if (!boolean)
        {
            IsSelected = true;
            SelectImg?.SetActive(true);
            SelectEvent?.Invoke();

        }
        else
        {
            IsSelected = false;
            SelectImg?.SetActive(false);
            UnSelectEvent?.Invoke();
        }
    }
}
