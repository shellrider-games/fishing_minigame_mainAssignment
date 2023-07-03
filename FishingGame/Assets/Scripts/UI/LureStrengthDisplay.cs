using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LureStrengthDisplay : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private LureStrength lureStrength;
    private static readonly int FillAmount = Shader.PropertyToID("_FillAmount");

    public void Awake()
    {
        lureStrength.OnStrengthUpdated += UpdateDisplay;
        image.material.SetFloat(FillAmount, 0f);
    }

    private void UpdateDisplay(float value)
    {
        image.material.SetFloat(FillAmount, value);
    }
}
