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
                Debug.Log($"Deploy with Strength: {_currentStrength}");
                _state = LureState.Deployed;
                break;
            default:
                Debug.Log("Already Deployed");
                break;
        }
    }
    
    
}
