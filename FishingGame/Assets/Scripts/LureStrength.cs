using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LureStrength : MonoBehaviour
{
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
    
    
    void Update()
    {
        if (_state is LureState.Charging)
            _currentStrength = (_currentStrength + lureSpeed * Time.deltaTime) % maxStrength;
    }

    private void OnMainAction()
    {
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
