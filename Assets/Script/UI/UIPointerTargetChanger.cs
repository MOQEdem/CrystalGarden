using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPointerTargetChanger : MonoBehaviour
{
    [SerializeField] private UIPointer _pointer;
    [SerializeField] private List<PreBuilding> _targets;

    private int _currentTarget;

    private void Awake()
    {
        _currentTarget = 0;
        _pointer.SetTarget(_targets[_currentTarget].PointerTarget);
    }

    private void OnEnable()
    {
        foreach (var prebuilding in _targets)
        {
            prebuilding.Builded += OnTargetBuilded;
        }
    }

    private void OnTargetBuilded()
    {
        _targets[_currentTarget].Builded -= OnTargetBuilded;
        _currentTarget++;

        if (_currentTarget >= _targets.Count)
        {
            Destroy(_pointer.gameObject);
        }
        else
        {
            _pointer.SetTarget(_targets[_currentTarget].PointerTarget);
        }
    }
}
