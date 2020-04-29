using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] Transform[] navPoints;
    [SerializeField] float distanceTreshold, speed, chaseSpeedMultiplier, searchDelay;

    [Header("Field of View")]
    [SerializeField] float maxAngle;
    [SerializeField] float maxDistance;
    [SerializeField] int rayCount, edgeIteration;

    public enum State { PATH, CHASE, SEARCH }
    public State state { get; private set; }
    public Transform player { get; private set; }

    private void Awake()
    {
        state = State.PATH;

        if (chaseSpeedMultiplier < 1) chaseSpeedMultiplier = 1;
    }

    public Transform[] GetNavPoints() => navPoints;
    public float GetDistanceTreshold() => distanceTreshold;
    public float GetSpeed() => speed;
    public float GetChaseSpeedMultiplier() => chaseSpeedMultiplier;
    public float GetSearchDelay() => searchDelay;
    public float GetMaxAngle() => maxAngle;
    public float GetMaxDistance() => maxDistance;
    public int GetRayCount() => rayCount;
    public int GetEdgeIteration() => edgeIteration;

    public void SetState(State _state) => state = _state;
    public void SetPlayer(Transform _player) => player = _player;

    public bool CompareState(State _state) => state == _state;
}
