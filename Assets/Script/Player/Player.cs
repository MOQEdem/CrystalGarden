using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private Stickman _stickman;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private Backpack _backpack;

    private PlayerMover _mover;

    public Stickman Stickman => _stickman;
    public Backpack Backpack => _backpack;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if (_mover.IsNeedToMove())
        {
            _stickman.SwitchAnimation(true);
            _mover.Move();
            _cameraMover.Move();
        }
        else
        {
            _stickman.SwitchAnimation(false);
        }
    }
}
