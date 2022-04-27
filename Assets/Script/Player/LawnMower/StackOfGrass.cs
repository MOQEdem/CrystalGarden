using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOfGrass : MonoBehaviour
{
    [SerializeField] private float _maxScaleY;
    [SerializeField] private float _scaleDelta;
    [SerializeField] private float _increaseSpeed;
    [SerializeField] private float _decreaseSpeed;
    [SerializeField] private float _stackUpSpeed;

    private float _currentScaleY;
    private bool _isAlreadyIncreasing;

    public float MaxScaleY => _maxScaleY;
    public float CurrentScaleY => _currentScaleY;

    private void Awake()
    {
        _currentScaleY = transform.localScale.y;
        _isAlreadyIncreasing = false;
    }

    public void AddGrassToStack()
    {
        if (_currentScaleY < _maxScaleY)
        {
            _currentScaleY += _scaleDelta;
        }

        if (!_isAlreadyIncreasing)
        {
            StartCoroutine(IncreaseScale());
        }
    }

    public void StackUp(List<StackUpPoint> waypoints)
    {
        StartCoroutine(MoveToBackpack(waypoints));
    }

    private IEnumerator IncreaseScale()
    {
        _isAlreadyIncreasing = true;

        while (transform.localScale.y <= _currentScaleY)
        {
            transform.localScale += new Vector3(0, _increaseSpeed * Time.deltaTime, 0);
            yield return null;
        }

        _isAlreadyIncreasing = false;
    }

    private IEnumerator MoveToBackpack(List<StackUpPoint> waypoints)
    {
        StopCoroutine(IncreaseScale());

        transform.parent = null;

        var currentPoint = 0;

        while (currentPoint < waypoints.Count)
        {
            Transform target = waypoints[currentPoint].transform;
            transform.position = Vector3.MoveTowards(transform.position, target.position, _stackUpSpeed * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, _decreaseSpeed * Time.deltaTime * transform.localScale.y);

            if (transform.position == target.position)
            {
                currentPoint++;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
