using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private Stickman _stickman;

    private PlayerMover _mover;

    public Stickman Stickman => _stickman;

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
        }
        else
        {
            _stickman.SwitchAnimation(false);
        }
    }
}
