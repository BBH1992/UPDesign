using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntitySize;

public class Entity_AreaCopy : MonoBehaviour
{
    public Transform cloneobj;
    public Transform bottomobj;
    public List<Transform> topobj;

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            AreaCopy();

        }
    }
    public void AreaCopy()
    {
        Transform clone = Instantiate(cloneobj, this.transform);
        clone.localPosition = cloneobj.localPosition;
        clone.localRotation = cloneobj.localRotation;

        Vector3 sieze = GetSizeByColider(clone.GetComponent<Collider>());
        sieze = clone.GetComponent<MeshFilter>().mesh.bounds.size;

        EntitySize entitySize = GetComponent<EntitySize>();
        if (entitySize.aspectType == AspectType.Y)
        {
            if (entitySize.type == SizeType.Colider)
            {
                entitySize.Colliders.Add(clone.GetComponent<Collider>());
            }
            else
            {
                entitySize.MeshRenderers.Add(clone.GetComponent<MeshRenderer>());
            }
        }
        // sieze.y = sieze.y / this.transform.localScale.y;

        topobj.Add(clone);
        foreach (var item in topobj)
        {
            item.localPosition += new Vector3(0, sieze.y, 0);
        }


    }
    Vector3 GetSizeByColider(Collider collider)
    {
        Vector3 size = Vector3.zero;
        size = collider.bounds.size;
        return size;
    }
    Vector3 GetSizeByMeshRenderer(MeshRenderer meshRenderer)
    {
        Vector3 size = Vector3.zero;
        size = meshRenderer.bounds.size;
        return size;
    }
}
