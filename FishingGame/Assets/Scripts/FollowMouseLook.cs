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

    private Vector3 _previousPosition;
    private Vector3 _targetPostition;

    private Quaternion _previousRotation;
    private Quaternion _targetRotation;
    
    private float _elapsedTime;
    private void Awake()
    {
        if (followTarget is not null)
        {
            followTarget.AfterLookAround += SetSlerpTarget;
        }
        _elapsedTime = followTime;
        transform.position = followTarget.transform.position  + distance * followTarget.transform.forward;
        _targetPostition = transform.position;
        _previousPosition = transform.position;
        transform.rotation = followTarget.transform.rotation;
        _targetRotation = transform.rotation;
        _previousRotation = transform.rotation;
    }

    private void SetSlerpTarget(Transform target)
    {
        _previousPosition = transform.position;
        _targetPostition = target.position + distance * target.forward;
        _previousRotation = transform.rotation;
        _targetRotation = target.rotation;
        _elapsedTime = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime < followTime)
        {
            _elapsedTime += Time.deltaTime;
            transform.position = Vector3.Slerp(_previousPosition, _targetPostition, _elapsedTime/followTime);
            transform.rotation = Quaternion.Slerp(_previousRotation, _targetRotation, _elapsedTime / followTime);
        }
    }
}
