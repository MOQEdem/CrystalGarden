using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIPreBuilding : MonoBehaviour
{
    [SerializeField] private PreBuilding _preBuilding;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _countingSpeed;

    private int _currentValue;
    private int _newValue;

    private void Awake()
    {
        _currentValue = _preBuilding.CostLeft;
        _text.text = _currentValue.ToString();
    }

    public void SetValue()
    {
        _newValue = _preBuilding.CostLeft;

        _text.DOCounter(_currentValue, _newValue, _countingSpeed);

        _currentValue = _newValue;
    }
}
