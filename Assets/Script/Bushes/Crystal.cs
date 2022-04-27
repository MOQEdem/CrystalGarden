using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Crystal : MonoBehaviour
{
    [SerializeField] private float _decreaseScaleSpeed;
    [SerializeField] private float _collectSpeed;
    [SerializeField] private float _dealyBeforeCollect;
    [SerializeField] private float _tossForce;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void Collect(Transform backpack)
    {
        _boxCollider.enabled = false;
        StartCoroutine(MoveToBackpack(backpack));
    }

    private IEnumerator MoveToBackpack(Transform backpack)
    {
        _rigidbody.velocity = Vector3.up * _tossForce;

        yield return new WaitForSeconds(_dealyBeforeCollect);

        _rigidbody.velocity = Vector3.zero;

        while (transform.position != backpack.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, backpack.transform.position, _collectSpeed * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, _decreaseScaleSpeed * Time.deltaTime * transform.localScale.y);
            yield return null;
        }

        Destroy(gameObject);
    }
}
