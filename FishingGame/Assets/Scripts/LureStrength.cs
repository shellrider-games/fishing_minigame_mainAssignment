using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LureStrength : MonoBehaviour
{
    public delegate void StrengthUpdateAction(float value);
    public event StrengthUpdateAction OnStrengthUpdated;
    private enum LureState
    {
        Ready,
        Charging,
        Deployed
    }
    
    [SerializeField] private float maxStrength = 1f;
    [SerializeField] private float lureSpeed = 1f;
    [SerializeField] private Transform pond;
    [SerializeField] private float maxDeployDistance;

    [SerializeField] private GameObject bobberPrefab;
    
    private float _currentStrength = 0f;
    private LureState _state = LureState.Ready;
    public float MaxStrength => maxStrength;
    
    void Update()
    {
        if (_state is LureState.Charging)
        {
            _currentStrength = (_currentStrength + lureSpeed * Time.deltaTime) % maxStrength;
            OnStrengthUpdated?.Invoke(_currentStrength/maxStrength);
        }
            
    }

    public void OnMainAction(InputAction.CallbackContext ctx)
    {
        if(!ctx.performed) return;
        switch (_state)
        {
            case LureState.Ready:
                _state = LureState.Charging;
                break;
            case LureState.Charging:
                Deploy();
                _state = LureState.Deployed;
                break;
            default:
                Debug.Log("Already Deployed");
                break;
        }
    }

    public void Deploy()
    {
        Vector3 deployTo = transform.position + transform.forward * ( 2 + maxDeployDistance * _currentStrength);
        deployTo.y = pond.position.y;
        _currentStrength = 0;
        OnStrengthUpdated?.Invoke(0);
        Instantiate(bobberPrefab, deployTo, Quaternion.identity);
    }
    
}
