using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Transform[] navPoints;
    [SerializeField] float distanceTreshold, chaseSpeedMultiplier;
    [SerializeField] GameObject player;

    Enemy enemy;
    int myNavPoint = 0;
    NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        navAgent = GetComponent<NavMeshAgent>();
        SetShorterPathIndex();
        navAgent.autoBraking = false;

        StartCoroutine(Path(2));
    }

    void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(collision.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.layer = 0;
            Destroy(this.gameObject);
        }
    }

    IEnumerator Path(float delay)
    {
        while (true)
        {
            if (!navAgent.pathPending && navAgent.remainingDistance < distanceTreshold && !navAgent.isStopped)
            {
                navAgent.isStopped = true;
                yield return new WaitForSeconds(delay);

                GotoNextNavPoint();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void GotoNextNavPoint()
    {
        if (navPoints.Length == 0) return;

        navAgent.SetDestination(navPoints[myNavPoint].position);

        navAgent.isStopped = false;

        myNavPoint = (myNavPoint + 1) % navPoints.Length;
    }

    void SetShorterPathIndex()
    {
        NavMeshPath shorterPath = new NavMeshPath();

        for (int i = 0; i < navPoints.Length; i++)
        {
            NavMeshPath path = new NavMeshPath();
            navAgent.CalculatePath(navPoints[i].position, path);

            if (shorterPath.status == NavMeshPathStatus.PathInvalid)
            {
                shorterPath = path;
                myNavPoint = i;
                continue;
            }

            if (CalculateDistance(path) < CalculateDistance(shorterPath))
            {
                shorterPath = path;
                myNavPoint = i;
            }
        }
    }

    float CalculateDistance(NavMeshPath path)
    {
        float distance = 0;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return distance;
    }
}
