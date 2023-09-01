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

    [SerializeField] private Transform bobberSpawn;
    
    private float _currentStrength = 0f;
    private LureState _state = LureState.Ready;

    private Bobber _bobber;
    
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
            case LureState.Deployed:
                ReelIn();
                break;
            default:
                Debug.Log("Undefined");
                break;
        }
    }

    private void Deploy()
    {
        Vector3 deployTo = transform.position + transform.forward * ( 2 + maxDeployDistance * _currentStrength);
        deployTo.y = pond.position.y;
        Vector3 controlPoint = transform.position + transform.forward * (2 + maxDeployDistance * _currentStrength) / 2;
        controlPoint.y = pond.position.y + 2f;
        _currentStrength = 0;
        OnStrengthUpdated?.Invoke(0);
        GameObject noob = Instantiate(bobberPrefab, bobberSpawn.position, Quaternion.identity);
        _bobber = noob.GetComponent<Bobber>();
        noob.GetComponent<MoveAlongQuadraticBezier>().MoveTo(deployTo, controlPoint);
    }

    private void ReelIn()
    {
        if(_bobber == null || !_bobber.Ready) return;
        _bobber.ReelIn();
        _bobber = null;
        _state = LureState.Ready;
    }
}
