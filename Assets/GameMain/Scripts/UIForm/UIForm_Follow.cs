using UnityEngine;

public class UIForm_Follow : MonoBehaviour
{
    public Transform Target;
    public Vector3 posOffset;
    public Vector3 angleOffset;
    public bool IsSetPos;
    private void Start()
    {
        if (Target == null)
        {
            Target = Camera.main.transform;
        }
        SetPos();
    }
    private void Update()
    {
        if (IsSetPos)
        {
            SetPos();
            IsSetPos = false;
        }
    }
    public void SetPos()
    {
        Vector3 pos = Target.position + posOffset;
        transform.position = pos;
        transform.LookAt(Target);
        transform.rotation = Quaternion.Euler(angleOffset.x, angleOffset.y, angleOffset.z);
    }
}
