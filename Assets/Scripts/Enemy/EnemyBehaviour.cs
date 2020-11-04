using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    Enemy enemy;
    int myNavPoint = 0;
    NavMeshAgent navAgent;
    Transform[] navPoints;
    bool increasingIndex = true;

    Coroutine checking;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        navPoints = enemy.GetNavPoints();

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoBraking = false;
        navAgent.stoppingDistance = enemy.GetDistanceThreshold();
        navAgent.speed = enemy.GetSpeed();

        SetShorterPathIndex();
        StartCoroutine(Path());
        StartCoroutine(Chase());
    }

    public void GoCheck(Vector3 dest)
    {
        if (checking != null)
        {
            StopCoroutine(checking);
            checking = null;
        }

        enemy.SetState(Enemy.State.CHECKING);
        checking = StartCoroutine(Check(dest));
    }

    void GotoNextNavPoint()
    {
        if (navPoints.Length == 0) return;

        navAgent.SetDestination(navPoints[myNavPoint].position);
        navAgent.isStopped = false;

        enemy.SetState(Enemy.State.PATH);

        if (enemy.GetRandomizeNavPoints())
        {
            int randomInt = Mathf.RoundToInt(UnityEngine.Random.Range(0, navPoints.Length - 1));

            while (randomInt == myNavPoint) randomInt = Mathf.RoundToInt(UnityEngine.Random.Range(0, navPoints.Length - 1));

            myNavPoint = randomInt;
        }

        else if (enemy.TurnBack())
        {
            if (increasingIndex) myNavPoint++;
            else myNavPoint--;

            if (myNavPoint >= navPoints.Length)
            {
                myNavPoint = navPoints.Length - 2;
                increasingIndex = false;
            }

            else if (myNavPoint < 0)
            {
                myNavPoint = 0;
                increasingIndex = true;
            }
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
            if (enemy.IsDistracted())
            {
                navAgent.isStopped = true;
                yield return new WaitForSeconds(1);
                continue;
            }

            else if (enemy.CompareState(Enemy.State.CHASE))
            {
                navAgent.isStopped = false;
                yield return new WaitForSeconds(wait);
                continue;
            }

            navAgent.isStopped = false;
            navAgent.speed = enemy.GetSpeed();

            if (!navAgent.pathPending && navAgent.remainingDistance < enemy.GetDistanceThreshold() && !navAgent.isStopped)
            {
                if (enemy.GetSearchDelay() > 0)
                {
                    navAgent.isStopped = true;
                    enemy.SetState(Enemy.State.SEARCH);
                    yield return new WaitForSeconds(enemy.GetSearchDelay());
                }

                GotoNextNavPoint();
            }

            yield return new WaitForSeconds(wait);
        }
    }

    IEnumerator Chase()
    {
        while (true)
        {
            if (enemy.IsDistracted())
            {
                navAgent.isStopped = true;
                yield return new WaitForSeconds(1);
                continue;
            }

            else if (!enemy.CompareState(Enemy.State.CHASE))
            {
                navAgent.isStopped = false;
                yield return new WaitForSeconds(1f);
                continue;
            }

            if (enemy.player)
            {
                navAgent.isStopped = false;
                navAgent.speed = enemy.GetSpeed() * enemy.GetChaseSpeedMultiplier();
                navAgent.SetDestination(enemy.player.position);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Check(Vector3 dest)
    {
        Vector3 init = transform.position;

        navAgent.isStopped = false;
        navAgent.speed = enemy.GetSpeed();
        navAgent.SetDestination(dest);

        float distance = enemy.GetDistanceThreshold();

        while (Vector3.Distance(transform.position, dest) > distance)
        {
            if (enemy.IsDistracted() || !enemy.CompareState(Enemy.State.CHECKING))
            {
                StopCoroutine(checking);
                checking = null;
                break;
            }

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(enemy.GetSearchDelay());

        navAgent.SetDestination(init);

        while (Vector3.Distance(transform.position, init) > distance)
        {
            if (enemy.IsDistracted() || !enemy.CompareState(Enemy.State.CHECKING))
            {
                StopCoroutine(checking);
                checking = null;
                break;
            }

            yield return new WaitForSeconds(1f);
        }

        enemy.SetState(Enemy.State.PATH);
    }
}
