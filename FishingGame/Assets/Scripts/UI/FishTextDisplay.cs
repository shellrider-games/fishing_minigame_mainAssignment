using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FishTextDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(_text != null) _text.text = $"Fish: {GameData.Instance.Fish}";
    }
}
