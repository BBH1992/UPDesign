using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySize : MonoBehaviour
{
    public enum SizeType
    {
        Colider,
        MeshRenderer
    }
    public enum AspectType
    {
        none,
        X,
        Y,
        Z
    }
    public SizeType type;
    public AspectType aspectType;

    public List<Collider> Colliders = new List<Collider>();
    public List<MeshRenderer> MeshRenderers = new List<MeshRenderer>();
    private void Awake()
    {


    }


    public Vector3 GetSize()
    {
        Vector3 size = Vector3.zero;
        switch (type)
        {
            case SizeType.Colider:
                size = GetSizeByColider();
                break;
            case SizeType.MeshRenderer:
                size = GetSizeByMeshRenderer();
                break;
            default:
                break;
        }
        return size;
    }

    Vector3 GetSizeByColider()
    {
        Vector3 size = Vector3.zero;
        float num = 0;
        switch (aspectType)
        {
            case AspectType.X:
                foreach (var item in Colliders)
                {
                    num += item.bounds.size.x;
                }
                size = new Vector3(num, Colliders[0].bounds.size.y, Colliders[0].bounds.size.z);
                break;
            case AspectType.Y:
                foreach (var item in Colliders)
                {
                    num += item.bounds.size.y;
                }
                size = new Vector3(Colliders[0].bounds.size.x, num, Colliders[0].bounds.size.z);
                break;
            case AspectType.Z:
                foreach (var item in Colliders)
                {
                    num += item.bounds.size.z;
                }
                size = new Vector3(Colliders[0].bounds.size.x, Colliders[0].bounds.size.y, num);
                break;
            case AspectType.none:
                foreach (var item in Colliders)
                {
                    size += item.bounds.size;
                }
                break;
        }


        return size;
    }
    Vector3 GetSizeByMeshRenderer()
    {
        Vector3 size = Vector3.zero;
        float num = 0;
        switch (aspectType)
        {
            case AspectType.X:
                foreach (var item in MeshRenderers)
                {
                    num += item.bounds.size.x;
                }
                size = new Vector3(num, MeshRenderers[0].bounds.size.y, MeshRenderers[0].bounds.size.z);
                break;
            case AspectType.Y:
                foreach (var item in MeshRenderers)
                {
                    num += item.bounds.size.y;
                }
                size = new Vector3(MeshRenderers[0].bounds.size.x, num, MeshRenderers[0].bounds.size.z);
                break;
            case AspectType.Z:
                foreach (var item in MeshRenderers)
                {
                    num += item.bounds.size.z;
                }
                size = new Vector3(MeshRenderers[0].bounds.size.x, MeshRenderers[0].bounds.size.y, num);
                break;
            case AspectType.none:
                foreach (var item in MeshRenderers)
                {
                    size += item.bounds.size;
                }
                break;
        }
        return size;
    }
}
