using CSCore.MediaFoundation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum VariantPointDir
{
    x,
    y,
    z,
    x_reversal,
    y_reversal,
    z_reversal
}
public class Variant : MonoBehaviour
{
    EntityInteractable entityInteractable_parent;
    public EntityInteractable EntityInteractable_parent
    {
        get
        {
            if (entityInteractable_parent == null)
            {
                entityInteractable_parent = GetComponentInParent<EntityInteractable>();
            }
            return entityInteractable_parent;
        }
    }

    public EntityInteractable point_up;
    public EntityInteractable point_down;
    public EntityInteractable point_left;
    public EntityInteractable point_right;
    public EntityInteractable point_forward;
    public EntityInteractable point_back;
    public Transform point;

    public List<EntityInteractable> points;
    float dis;
    float lastdis;
    public float scaleSpeed = 0.005f;

    private void Awake()
    {
        entityInteractable_parent = GetComponentInParent<EntityInteractable>();
        //point_up.OnSelectEntered += OnSelectEntered;
        //point_down.OnSelectEntered += OnSelectEntered;
        //point_left.OnSelectEntered += OnSelectEntered;
        //point_right.OnSelectEntered += OnSelectEntered;
        //point_forward.OnSelectEntered += OnSelectEntered;
        //point_back.OnSelectEntered += OnSelectEntered;

        //point_up.OnSelectExited += OnSelectExited;
        //point_down.OnSelectExited += OnSelectExited;
        //point_left.OnSelectExited += OnSelectExited;
        //point_right.OnSelectExited += OnSelectExited;
        //point_forward.OnSelectExited += OnSelectExited;
        //point_back.OnSelectExited += OnSelectExited;
        foreach (var item in points)
        {
            item.OnSelectEntered += OnSelectEntered;
            item.OnSelectUpdate += OnSelectUpdate;
            item.OnSelectExited += OnSelectExited;
        }
    }



    private void Start()
    {

        //   Hide();

    }
    float GetLength(Transform tran)
    {
        float tmp = 0;
        //   tmp = Vector3.Distance(tran.localPosition, point.localPosition);

        tmp = Vector3.Distance(point.position, tran.position);
        return tmp;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        EntityInteractable_parent.CloseGrab();
        dis = 0;
    }
    public void Hide()
    {
        EntityInteractable_parent.OpenGrab();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnSelectUpdate(EntityInteractable interactable, SelectEnterEventArgs args)
    {
        Transform interactor = args.interactorObject.transform;
        //    dis = GetLength(interactable.transform);
        lastdis = GetLength(interactor.transform);
        int num = 0;
        if (lastdis > dis)
        {
            //放大
            num = 1;
        }
        else
        {
            //缩小
            num = -1;
        }
        VariantPoint variantPoint = interactable.GetComponent<VariantPoint>();
        Vector3 scale = EntityInteractable_parent.transform.localScale;
        switch (variantPoint.variantPointDir)
        {
            case VariantPointDir.x:
                scale.x += (scaleSpeed * num * Time.deltaTime);
                scale.x = Mathf.Clamp(scale.x, CommandManger.Instance.minScale, CommandManger.Instance.maxScale);
                break;
            case VariantPointDir.y:
                scale.y += (scaleSpeed * num * Time.deltaTime);
                scale.y = Mathf.Clamp(scale.y, CommandManger.Instance.minScale, CommandManger.Instance.maxScale);
                break;
            case VariantPointDir.z:
                scale.z += (scaleSpeed * num * Time.deltaTime);
                scale.z = Mathf.Clamp(scale.z, CommandManger.Instance.minScale, CommandManger.Instance.maxScale);
                break;
            case VariantPointDir.x_reversal:
                scale.x += (scaleSpeed * num * Time.deltaTime);
                scale.x = Mathf.Clamp(scale.x, CommandManger.Instance.minScale, CommandManger.Instance.maxScale);
                break;
            case VariantPointDir.y_reversal:
                scale.y += (scaleSpeed * num * Time.deltaTime);
                scale.y = Mathf.Clamp(scale.y, CommandManger.Instance.minScale, CommandManger.Instance.maxScale);
                break;
            case VariantPointDir.z_reversal:
                scale.z += (scaleSpeed * num * Time.deltaTime);
                scale.z = Mathf.Clamp(scale.z, CommandManger.Instance.minScale, CommandManger.Instance.maxScale);
                break;
            default:
                break;
        }
        entityInteractable_parent.transform.localScale = scale;
    }
    public void OnSelectEntered(EntityInteractable interactable, SelectEnterEventArgs args)
    {

        dis = GetLength(interactable.transform);

    }
    public void OnSelectExited(EntityInteractable interactable, SelectExitEventArgs args)
    {
    }
    private void OnDestroy()
    {
        //point_up.OnSelectEntered -= OnSelectEntered;
        //point_down.OnSelectEntered -= OnSelectEntered;
        //point_left.OnSelectEntered -= OnSelectEntered;
        //point_right.OnSelectEntered -= OnSelectEntered;
        //point_forward.OnSelectEntered -= OnSelectEntered;
        //point_back.OnSelectEntered -= OnSelectEntered;

        //point_up.OnSelectExited -= OnSelectExited;
        //point_down.OnSelectExited -= OnSelectExited;
        //point_left.OnSelectExited -= OnSelectExited;
        //point_right.OnSelectExited -= OnSelectExited;
        //point_forward.OnSelectExited -= OnSelectExited;
        //point_back.OnSelectExited -= OnSelectExited;
        foreach (var item in points)
        {
            item.OnSelectEntered -= OnSelectEntered;
            item.OnSelectUpdate -= OnSelectUpdate;
            item.OnSelectExited -= OnSelectExited;
        }

    }

}
