using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider2D))]
[ExecuteInEditMode]
public class MeshColliderFill : MonoBehaviour
{
    public Color _fillColor = Color.white;
    public bool transparent = false;

    private Collider2D _collider = null;
    private MeshRenderer _renderer = null;
    private MeshFilter _filter = null;
    private Material _material = null;

    private Vector2[] _colliderPoints = null;
    private Quaternion _rotation = Quaternion.identity;

    bool _highlight = false;

    private void InitMaterial()
    {
        if (_renderer == null)
        {
            return;
        }

        if (_material)
        {
            Destroy(_material);
        }
        if (transparent || _highlight)
        {
            _material = new Material(Shader.Find("NAKAI/Unlit/Transparent Colored"));
        }
        else
        {
            _material = new Material(Shader.Find("Unlit/Color"));
        }
        _material.color = _fillColor;
        _material.renderQueue = 3000;

        if (Application.isPlaying)
        {
            _renderer.material = _material;
        }
        else
        {
            _renderer.sharedMaterial = _material;
        }
    }

    // Use this for initialization
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        if (_collider == null)
        {
            Debug.Assert(false, "Collider not found");
            return;
        }
        if (!(_collider is BoxCollider2D)
            && !(_collider is PolygonCollider2D))
        {
            Debug.Assert(false, "Only BoxCollider2D and PolygonCollider2D are supported");
            return;
        }

        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        InitMaterial();

        _colliderPoints = GetColliderPoints();
        _rotation = transform.rotation;
        UpdateMesh(_colliderPoints);
    }

    private Vector2[] GetBoxXolliderPoints(BoxCollider2D boxCollider)
    {
        bool distortion = ShouldApplyDistortion(boxCollider);
        Rect playerRect;
        if (distortion)
        {
            playerRect = GetLocalPlayerRect();
            playerRect.position -= boxCollider.offset;
        }
        else
        {
            playerRect = Rect.zero;
        }
        Vector2 center = _collider.offset;
        Vector2[] maxCorners = new Vector2[6];
        float hWidth = boxCollider.size.x / 2;
        float hHeight = boxCollider.size.y / 2;

        int i = 0;
        maxCorners[i] = center + new Vector2(-hWidth, hHeight);
        ++i;
        if (distortion && Mathf.Abs(playerRect.yMax - hHeight) <= 0.55)
        {
            maxCorners[i] = center + new Vector2(playerRect.center.x, playerRect.yMax);
            ++i;
        }
        maxCorners[i] = center + new Vector2(hWidth, hHeight);
        ++i;
        maxCorners[i] = center + new Vector2(hWidth, -hHeight);
        ++i;
        if (distortion && Mathf.Abs(playerRect.yMin + hHeight)  <= 0.55)
        {
            maxCorners[i] = center + new Vector2(playerRect.center.x, playerRect.yMin);
            ++i;
        }
        maxCorners[i] = center + new Vector2(-hWidth, -hHeight);
        ++i;

        Vector2[] corners = new Vector2[i];
        Array.Copy(maxCorners, corners, i);

        return corners;
    }

    Vector2[] GetColliderPoints()
    {
        if (_collider is PolygonCollider2D)
        {
            PolygonCollider2D polygonCollider = _collider as PolygonCollider2D;
            Vector2 center = polygonCollider.offset;
            Vector2[] points = new Vector2[polygonCollider.points.Length];
            for (int i=0; i<polygonCollider.points.Length; ++i)
            {
                points[i] = polygonCollider.points[i] + center;
            }
            return points;
        }
        else if (_collider is BoxCollider2D)
        {
            BoxCollider2D boxCollider = _collider as BoxCollider2D;
            return GetBoxXolliderPoints(boxCollider);

        }
        Debug.Assert(false, "Unsupported collider type");
        return null;
    }

    private void UpdateMesh(Vector2[] points)
    {
        Mesh mesh = new Mesh();
        _filter.mesh = mesh;

        Triangulator tr = new Triangulator(points);
        int[] trinagles = tr.Triangulate();

        Vector3[] vertices = new Vector3[points.Length];
        for (int i = 0; i < points.Length; ++i)
        {
            int index = (transform.rotation.eulerAngles.y > 90) ? points.Length - i - 1 : i;
            vertices[i] = new Vector3(points[index].x, points[index].y, 0);
        }

        mesh.vertices = vertices;
        mesh.triangles = trinagles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        _colliderPoints = points;
        _rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2[] points = GetColliderPoints();
        if (points.Length != _colliderPoints.Length
            || _rotation != transform.rotation)
        {
            UpdateMesh(points);
        }
        else
        {
            for (int i = 0; i<points.Length; ++i)
            {
                if (points[i] != _colliderPoints[i])
                {
                    UpdateMesh(points);
                    break;
                }
            }
        }
        Color color = _fillColor;
        if (_highlight)
        {
            color.a = 0.7f;
        }
        if (Application.isPlaying)
        {
            _renderer.material.color = color;
            
        }
        else
        {
            _renderer.sharedMaterial.color = color;
        }
    }

    public void HighlightObject()
    {
        _highlight = true;
        InitMaterial();
    }

    public void TurnHighlightOff()
    {
        _highlight = false;
        InitMaterial();
    }

    public void SetTransparent(bool transparent)
    {
        if (this.transparent != transparent)
        {
            this.transparent = transparent;
            InitMaterial();
        }
    }

    private bool ShouldApplyDistortion(BoxCollider2D boxCollider)
    {
        /*if (!_highlight)
        {
            return false;
        }

        Rect playerRect = GetLocalPlayerRect();
        playerRect.position -= boxCollider.offset;
        Vector2 bottomLeft = boxCollider.offset - boxCollider.size / 2;
        Vector2 topRight = boxCollider.offset + boxCollider.size / 2;
        return bottomLeft.x <= playerRect.center.x
            && playerRect.center.x <= topRight.x
            && playerRect.yMax >= bottomLeft.y
            && playerRect.yMin <= topRight.y;*/
        return false;
    }

    private Rect GetLocalPlayerRect()
    {
        /*GameObject player = Renovation.Utils.ApplicationUtils.GetPlayerObject();
        ColliderParams box = player.GetComponent<PlayerCollidersController>().GetIdleColliderParams();
        return new Rect(
            box.offset + (Vector2)player.transform.position - (Vector2)this.transform.position - box.size/2,
            box.size);*/
        return Rect.zero;
    }

}
