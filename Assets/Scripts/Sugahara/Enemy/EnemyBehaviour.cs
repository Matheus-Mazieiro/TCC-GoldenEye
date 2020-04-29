using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    Enemy enemy;
    int myNavPoint = 0;
    NavMeshAgent navAgent;
    Transform[] navPoints;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        navPoints = enemy.GetNavPoints();

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoBraking = false;
        navAgent.stoppingDistance = enemy.GetDistanceTreshold();
        navAgent.speed = enemy.GetSpeed();

        SetShorterPathIndex();
        StartCoroutine(Path());
        StartCoroutine(Chase());
    }

    void FixedUpdate()
    {

    }

    void GotoNextNavPoint(bool random)
    {
        if (navPoints.Length == 0) return;

        navAgent.SetDestination(navPoints[myNavPoint].position);
        navAgent.isStopped = false;

        enemy.SetState(Enemy.State.PATH);

        if (random)
        {
            int randomInt = Mathf.RoundToInt(UnityEngine.Random.Range(0, navPoints.Length - 1));

            while (randomInt == myNavPoint) randomInt = Mathf.RoundToInt(UnityEngine.Random.Range(0, navPoints.Length - 1));

            myNavPoint = randomInt;
        }

        else myNavPoint = (myNavPoint + 1) % navPoints.Length;
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

    IEnumerator Path()
    {
        float wait = 0.5f;

        while (true)
        {
            if (enemy.CompareState(Enemy.State.CHASE))
            {
                yield return new WaitForSeconds(wait);
                continue;
            }

            navAgent.speed = enemy.GetSpeed();

            if (!navAgent.pathPending && navAgent.remainingDistance < enemy.GetDistanceTreshold() && !navAgent.isStopped)
            {
                navAgent.isStopped = true;
                enemy.SetState(Enemy.State.SEARCH);
                yield return new WaitForSeconds(enemy.GetSearchDelay());

                GotoNextNavPoint(random: true);
            }

            yield return new WaitForSeconds(wait);
        }
    }

    IEnumerator Chase()
    {
        while (true)
        {
            if (!enemy.CompareState(Enemy.State.CHASE))
            {
                yield return new WaitForSeconds(1f);
                continue;
            }

            if (enemy.player)
            {
                navAgent.speed = enemy.GetSpeed() * enemy.GetChaseSpeedMultiplier();
                navAgent.SetDestination(enemy.player.position);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
