using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    public enum State { PATH, CHASE, SEARCH}
    State state;
    Rigidbody _rigidbody;
    Transform _transform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    public void SetState(State _state)
    {
        state = _state;
    }
}
