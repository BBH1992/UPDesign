using System.Collections.Generic;
using UnityEngine;

public class TransformInfo
{
    public string name;
    public Transform transform;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Transform last;

    public Transform parent;

    public TemplateType templateType;
}
public class TransformInfo_Nurbs : TransformInfo
{
    public int row;
    public int column;
    public float dis;
    public List<Vector3> PointsPos;
    public Material material;
}