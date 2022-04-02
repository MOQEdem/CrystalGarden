using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _moveVector;

    public bool IsNeedToMove()
    {
        return Input.GetMouseButton(0);
    }

    public void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = Input.GetAxis("Mouse X");
        _moveVector.z = Input.GetAxis("Mouse Y");

        if (Vector3.Angle(Vector3.forward, _moveVector) > 1f)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        transform.position += transform.TransformDirection(Vector3.forward * _moveSpeed * Time.deltaTime);

    }
}
