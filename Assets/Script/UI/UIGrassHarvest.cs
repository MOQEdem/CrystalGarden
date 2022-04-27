using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGrassHarvest : MonoBehaviour
{
    [SerializeField] private CameraMover _camera;
    [SerializeField] private GrassBin _grassBin;
    [SerializeField] TMP_Text _text;

    private void OnEnable()
    {
        _grassBin.GrassHarvested += SetValue;
    }

    private void OnDisable()
    {
        _grassBin.GrassHarvested -= SetValue;
    }

    public void LookAtCamera()
    {
        transform.LookAt(_camera.transform);
    }

    private void SetValue()
    {
        _text.text = $"+ {_grassBin.HarvestedGrass.ToString()}";
    }
}
