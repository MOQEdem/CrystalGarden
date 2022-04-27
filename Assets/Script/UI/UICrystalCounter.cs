using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICrystalCounter : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _backpack.CrystalAmountChanged += SetValue;
    }

    private void OnDisable()
    {
        _backpack.CrystalAmountChanged -= SetValue;
    }

    private void SetValue()
    {
        _text.text = _backpack.CollectedCrystal.ToString();
    }
}
