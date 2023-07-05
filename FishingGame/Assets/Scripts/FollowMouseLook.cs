using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowMouseLook : MonoBehaviour
{
    [SerializeField] private MouseLookControl followTarget;
    [SerializeField] private float distance;
    [SerializeField] private float followTime;
    [SerializeField] private Vector3 offset;

    private Vector3 _previousPosition;
    private Vector3 _targetPostition;
    private float _elapsedTime;
    private void Awake()
    {
        if (followTarget is not null)
        {
            followTarget.AfterLookAround += SetSlerpTarget;
        }
        _elapsedTime = followTime;
        transform.position = followTarget.transform.position + offset + distance * followTarget.transform.forward;
        _targetPostition = transform.position;
        _previousPosition = transform.position;
    }

    private void SetSlerpTarget(Transform target)
    {
        _previousPosition = transform.position;
        _targetPostition = target.position + offset + distance * target.forward;
        _elapsedTime = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime < followTime)
        {
            _elapsedTime += Time.deltaTime;
            transform.position = Vector3.Slerp(_previousPosition, _targetPostition, _elapsedTime);
        }
    }
}
