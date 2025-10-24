using HighlightPlus;
using NURBS;
using System.Collections.Generic;
using UnityEngine;

public class BurbsItem : MonoBehaviour
{
    private Surface surface;
    public List<Transform> controlPoints = new List<Transform>();
    [Range(3, 20)]
    [SerializeField]
    private int row = 4;
    public int Row
    {
        get
        {
            return row;
        }
        set
        {
            Row = value;
        }
    }
    [Range(3, 20)]
    [SerializeField]
    private int column = 4;
    public int Column
    {
        get
        {
            return column;
        }
        set
        {
            column = value;
        }
    }
    public float distance;
    [Range(1, 3)]
    public int degreeU = 2;
    [Range(1, 3)]
    public int degreeV = 2;
    [Range(1, 50)]
    public int resolutionU = 25;
    [Range(1, 50)]
    public int resolutionV = 25;

    public bool UseManagerData;

    //----
    private Transform point;
    internal Material material;
    private Mesh mesh;
    private MeshRenderer render;
    private MeshFilter meshFilter;
    internal BoxCollider collider;
    private List<Collider> childsCollider = new List<Collider>();
    private List<HighlightEffect> childsHighlightEffects = new List<HighlightEffect>();
#if UNITY_EDITOR
    public bool StartCreate;
#endif
    private void Awake()
    {
        collider = GetComponent<BoxCollider>();

    }
    private void Start()
    {
#if UNITY_EDITOR
        if (StartCreate)
        {
            CreateNurbs();

        }
#endif

    }
    public void SetMaterial(Material material)
    {
        render.material = material;
    }

    public void CreateNurbs()
    {
        if (UseManagerData)
        {
            row = NurbsManager.Instance.row;
            column = NurbsManager.Instance.column;
            distance = NurbsManager.Instance.distance;
            degreeU = NurbsManager.Instance.degreeU;
            degreeV = NurbsManager.Instance.degreeV;
            resolutionU = NurbsManager.Instance.row;
            resolutionV = NurbsManager.Instance.row;
        }

        point = NurbsManager.Instance.point;
        material = NurbsManager.Instance.DefaultMaterial;
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh = new Mesh();

        render = GetComponent<MeshRenderer>();
        SetMaterial(material);
        if (distance < point.localScale.x)
        {
            distance = 2 * point.localScale.x;
        }
        controlPoints.Clear();
        childsCollider.Clear();
        childsHighlightEffects.Clear();
        //生成point位置
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Transform clone = Instantiate(point, transform.position, Quaternion.identity, transform);
                Vector3 pos = clone.localPosition;
                pos.x = -j * distance;
                // pos.y = 0;

                pos.z = i * distance;
                clone.localPosition = pos;
                controlPoints.Add(clone);
                BoxCollider boxCollider = clone.GetComponent<BoxCollider>();
                //  boxCollider.enabled = false;
                childsCollider.Add(boxCollider);
                childsHighlightEffects.Add(clone.GetComponent<HighlightEffect>());
                clone.gameObject.SetActive(false);
            }
        }

        BuildMesh();
        collider = GetComponent<BoxCollider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            Vector3 center = render.bounds.center;
            Vector3 size = render.bounds.size;
            center.y = size.y;
            center.x = -(size.x / 2);
            center.z = (size.z / 2);

            collider.center = center;
            collider.size = size;
            EntitySize entitySize = gameObject.GetComponent<EntitySize>();
            entitySize.Colliders.Add(collider);
        }
    }
    public void CreateNurbs(int row, int column, float distance, Material material, List<Transform> points)
    {

        this.row = row;
        this.column = column;
        this.distance = distance;
        point = NurbsManager.Instance.point;
        this.material = material;
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh = new Mesh();

        render = GetComponent<MeshRenderer>();
        SetMaterial(material);
        if (distance < point.localScale.x)
        {
            distance = 2 * point.localScale.x;
        }
        this.controlPoints.Clear();
        childsCollider.Clear();
        childsHighlightEffects.Clear();
        //生成point位置
        foreach (Transform item in points)
        {
            Vector3 pos = item.localPosition;
            Transform clone = Instantiate(point, pos, Quaternion.identity, transform);
            controlPoints.Add(clone);
            BoxCollider boxCollider = clone.GetComponent<BoxCollider>();
            // boxCollider.enabled = false;
            childsCollider.Add(boxCollider);
            childsHighlightEffects.Add(clone.GetComponent<HighlightEffect>());
            clone.gameObject.SetActive(false);
        }


        BuildMesh();
        collider = GetComponent<BoxCollider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            Vector3 center = render.bounds.center;
            Vector3 size = render.bounds.size;
            center.y = size.y;
            center.x = -(size.x / 2);
            center.z = (size.z / 2);

            collider.center = center;
            collider.size = size;
            EntitySize entitySize = gameObject.GetComponent<EntitySize>();
            entitySize.Colliders.Add(collider);
        }
    }
    public void CreateNurbs(TransformInfo_Nurbs transformInfo_Nurbs)
    {

        row = transformInfo_Nurbs.row;
        column = transformInfo_Nurbs.column;
        distance = transformInfo_Nurbs.dis;
        point = NurbsManager.Instance.point;
        this.material = transformInfo_Nurbs.material;
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh = new Mesh();

        render = GetComponent<MeshRenderer>();
        SetMaterial(material);
        if (distance < point.localScale.x)
        {
            distance = 2 * point.localScale.x;
        }
        controlPoints.Clear();
        childsCollider.Clear();
        childsHighlightEffects.Clear();
        //生成point位置
        foreach (Vector3 item in transformInfo_Nurbs.PointsPos)
        {
            Transform clone = Instantiate(point, transform);
            clone.localPosition = item;
            controlPoints.Add(clone);
            BoxCollider boxCollider = clone.GetComponent<BoxCollider>();
            // boxCollider.enabled = false;
            childsCollider.Add(boxCollider);
            childsHighlightEffects.Add(clone.GetComponent<HighlightEffect>());
            clone.gameObject.SetActive(false);
        }


        BuildMesh();
        collider = GetComponent<BoxCollider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            Vector3 center = render.bounds.center;
            Vector3 size = render.bounds.size;
            center.y = size.y;
            center.x = -(size.x / 2);
            center.z = (size.z / 2);

            collider.center = center;
            collider.size = size;
            EntitySize entitySize = gameObject.GetComponent<EntitySize>();
            entitySize.Colliders.Add(collider);
        }
    }
    public void SetChildsHighlight(bool boolean)
    {
        foreach (HighlightEffect item in childsHighlightEffects)
        {
            item.highlighted = boolean;
        }
    }
    public void SetCollider(bool boolean)
    {
        collider.enabled = boolean;
    }

    public void SetChildsCollider(bool boolean)
    {
        foreach (Collider item in childsCollider)
        {
            item.enabled = boolean;
        }
    }
    public void SetPointActive(bool boolean)
    {
        foreach (Transform item in controlPoints)
        {
            item.gameObject.SetActive(boolean);
        }
    }
    private void BuildMesh()
    {
        ControlPoint[,] cps = new NURBS.ControlPoint[row, column];
        int c = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Vector3 pos = controlPoints[c].transform.localPosition;
                cps[i, j] = new NURBS.ControlPoint(pos.x, pos.y, pos.z, 1);
                c++;
            }
        }
        if (surface == null)
        {
            surface = new NURBS.Surface(cps, degreeU, degreeV);
        }

        surface.DegreeU(degreeU);
        surface.DegreeV(degreeV);
        //Update control points
        surface.controlPoints = cps;

        //Build mesh (reusing Mesh to save GC allocation)
        surface.BuildMesh(resolutionU, resolutionV, mesh);


    }
    private void Update()
    {
        BuildMesh();
    }
}
