using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongQuadraticBezier : MonoBehaviour
{
    [SerializeField] private float travelTime;
    private Vector3 _to;
    private Vector3 _controlPoint;
    private Vector3 _previousPosition;
    private float _elapsedTime;
    private bool _move = false;
    
    
    
    void Update()
    {
        if (_move)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / travelTime;
            transform.position = Mathf.Pow(1f - t, 2) * _previousPosition +
                                 2 * t * (1f-t) * _controlPoint 
                                 + Mathf.Pow(t,2 ) * _to;
            if (_elapsedTime >= travelTime)
            {
                transform.position = _to;
                _move = false;
            }
        }
    }

    public void MoveTo(Vector3 to, Vector3 via)
    {
        _previousPosition = transform.position;
        _to = to;
        _controlPoint = via;
        _elapsedTime = 0;
        _move = true;
    }
    
}
