using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] MeshFilter meshFilter;

    Enemy enemy;
    Transform _transform;
    Mesh fieldOfViewMesh;
    RaycastHitInfo info;

    void Awake()
    {
        if (!meshFilter) meshFilter = GetComponent<MeshFilter>();
        enemy = GetComponentInParent<Enemy>();

        _transform = transform;
        info = new RaycastHitInfo(enemy.GetMaxDistance(), gameObject.layer);

        fieldOfViewMesh = new Mesh();
        fieldOfViewMesh.name = "Field of View Mesh";
        meshFilter.mesh = fieldOfViewMesh;
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        float stepAngleSize = enemy.GetMaxAngle() * 2 / enemy.GetRayCount();

        HashSet<Vector3> vertices = new HashSet<Vector3>();
        vertices.Add(Vector3.zero);

        info.SetBasics(_transform.position, _transform.up, _transform.forward);

        for (int i = 0; i <= enemy.GetRayCount(); i++)
        {
            float angle = stepAngleSize * i - enemy.GetMaxAngle();

            info.RayCast(angle);

            if (i > 0 && i % 2 == 1)
            {
                if (!info.CompareWithLastHit())
                {
                    Vector3[] edges = info.FindEdges(enemy.GetEdgeIteration(), stepAngleSize, "Player", out bool tagHit);

                    if (i == 1) edges[0] = info.LastHitPoint();
                    else if (i == enemy.GetRayCount()) edges[1] = info.HitPoint();

                    if (edges[0] != Vector3.zero) vertices.Add(_transform.InverseTransformPoint(edges[0]));
                    if (edges[1] != Vector3.zero) vertices.Add(_transform.InverseTransformPoint(edges[1]));

                    if (tagHit)
                    {
                        enemy.SetState(Enemy.State.CHASE);
                        enemy.SetPlayer(info.TagHitTransform());
                    }
                }

                else
                {
                    vertices.Add(_transform.InverseTransformPoint(info.LastHitPoint()));
                    vertices.Add(_transform.InverseTransformPoint(info.HitPoint()));
                }
            }
        }

        if (vertices.Count > 3)
        {
            int[] triangles = new int[(vertices.Count - 2) * 3];

            for (int i = 0; i < vertices.Count - 2; i++)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }

            fieldOfViewMesh.Clear();

            fieldOfViewMesh.vertices = vertices.ToArray<Vector3>();
            fieldOfViewMesh.triangles = triangles;
            fieldOfViewMesh.RecalculateNormals();
        }
    }
}

public class RaycastHitInfo
{
    LayerMask layerMask;
    float maxDistance;
    Vector3 up, forward, position, direction, lastPosition, lastDirection;
    RaycastHit hit, lastHit;
    Transform tagHitTransform;

    public RaycastHitInfo(float _maxDistance, int layer)
    {
        maxDistance = _maxDistance;
        layerMask = ~LayerMask.GetMask(LayerMask.LayerToName(layer));
    }

    public void SetBasics(Vector3 _position, Vector3 _up, Vector3 _forward)
    {
        lastPosition = position;
        position = _position;
        up = _up;
        forward = _forward;

        hit = new RaycastHit();
        lastHit = new RaycastHit();
    }

    public bool RayCast(float angle)
    {
        lastHit = hit;
        lastDirection = direction;

        direction = forward;

        direction = Quaternion.AngleAxis(angle, up) * direction;

#if UNITY_EDITOR
        //Debug.DrawRay(position, direction * maxDistance, Color.green);
#endif

        return Physics.Raycast(position, direction, out hit, maxDistance, layerMask);
    }

    public Vector3 HitPoint()
    {
        if (hit.distance > 0) return hit.point;

        return position + maxDistance * direction;
    }

    public Vector3 LastHitPoint()
    {
        if (lastHit.distance > 0) return lastHit.point;

        return lastPosition + maxDistance * lastDirection;
    }

    public Transform TagHitTransform() => tagHitTransform;

    public bool CompareWithLastHit() => CompareColliders(hit.collider, lastHit.collider);

    public bool CompareColliders(Collider collider1, Collider collider2) =>
        (collider1 && collider2 && collider1.gameObject == collider2.gameObject) || (!collider1 && !collider2);

    public Vector3[] FindEdges(int findEdgeIteration, float startAngle, string tag, out bool tagHit)
    {
        tagHit = false;

        if (lastHit.collider && lastHit.collider.CompareTag(tag))
        {
            tagHit = true;
            tagHitTransform = lastHit.collider.transform;
        }

        if (hit.collider && hit.collider.CompareTag(tag))
        {
            tagHit = true;
            tagHitTransform = hit.collider.transform;
        }

        Vector3[] edges = new Vector3[2];

        if (lastHit.distance > 0) edges[0] = lastHit.point;
        else edges[0] = lastPosition + maxDistance * lastDirection;

        if (hit.distance > 0) edges[1] = hit.point;
        else edges[1] = position + maxDistance * direction;

        if (findEdgeIteration <= 0) return edges;

        for (int i = 1; i <= findEdgeIteration; i++)
        {
            Vector3 _direction = Quaternion.AngleAxis(-startAngle / (i + 1), up) * direction;

            if (Physics.Raycast(position, _direction, out RaycastHit _hit, maxDistance, layerMask))
            {
                if (_hit.collider.CompareTag(tag))
                {
                    tagHit = true;
                    tagHitTransform = _hit.collider.transform;
                }
            }

            if (CompareColliders(lastHit.collider, _hit.collider))
            {
                if (_hit.distance > 0) edges[0] = _hit.point;
                else edges[0] = position + maxDistance * _direction;
            }

            if (!CompareColliders(hit.collider, _hit.collider))
            {
                if (_hit.distance > 0) edges[1] = _hit.point;
                else edges[1] = position + maxDistance * _direction;
            }
        }

        return edges;
    }
}