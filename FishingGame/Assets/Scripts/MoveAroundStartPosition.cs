using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveAroundStartPosition : MonoBehaviour
{

    [SerializeField] private float PatrolRadius;

    [Header("Traveltime")]
    [SerializeField] private float TravelTime;
    [SerializeField] private float TravelTimeVariance;
    
    [Header("Waittime")]
    [SerializeField] private float WaitTime;
    [SerializeField] private float WaitTimeVariance;

    [Header("Animator")] [SerializeField] private Animator Animator;

    private Vector3 _startPos;
    private Vector3 _lastPos;
    private Vector3 _targetPos;

    private float _waitTime;
    private float _travelTime;
    
    private float _elapsedTime;
    private bool _waiting;

    private void Awake()
    {
        _travelTime = TravelTime + Random.Range(-TravelTimeVariance,TravelTimeVariance);
        _waitTime = WaitTime + Random.Range(-WaitTimeVariance,WaitTimeVariance);
    }

    void Start()
    {
        _startPos = transform.position;
        NewTargetPosition();
    }
    
    void Update()
    {
        if(_waiting) return;
        
        InterpolatePosition(Time.deltaTime);
    }

    private void NewTargetPosition()
    {
        float radius = Random.Range(-PatrolRadius, PatrolRadius);
        float rad = Random.value * Mathf.PI;
        _lastPos = transform.position;
        _targetPos = _startPos + new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius);
        transform.LookAt(_targetPos);
        _elapsedTime = 0;
        if(Animator != null) Animator.SetBool("moving", true);
    }

    private void InterpolatePosition(float delta)
    {
        _elapsedTime += delta;
        var t = _elapsedTime / _travelTime;
        transform.position = Vector3.Lerp(_lastPos,_targetPos, t);
        if (t > 1) StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        if(Animator != null) Animator.SetBool("moving", false);
        _waiting = true;
        yield return new WaitForSeconds(_waitTime);
        NewTargetPosition();
        _waiting = false;
    }
}
