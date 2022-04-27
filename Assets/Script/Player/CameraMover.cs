using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _position;
    [SerializeField] private UIGrassHarvest _ui;

    public void Move()
    {
        transform.position = _player.transform.position + _position;
        _ui.LookAtCamera();
    }
}
