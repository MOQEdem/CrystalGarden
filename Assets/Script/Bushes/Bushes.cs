using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CrystalSpawner))]
public class Bushes : MonoBehaviour
{
    [SerializeField] private float _timeToGrowUp;
    [SerializeField] private float _cuttingSpeed;
    [SerializeField] private float _growingSpeed;
    [SerializeField] private GameObject _grass;

    private bool _isCutOff;
    private Vector3 _normalScale;
    private CrystalSpawner _crystalSpawner;

    public bool IsCutOff => _isCutOff;

    private void Awake()
    {
        _isCutOff = false;
        _normalScale = _grass.transform.localScale;
        _crystalSpawner = GetComponent<CrystalSpawner>();
        _crystalSpawner.SpawnAtStart();
    }

    public void Cut()
    {
        StartCoroutine(CuttingDown());
    }

    private IEnumerator CuttingDown()
    {
        _isCutOff = true;

        Vector3 cutScale = new Vector3(_normalScale.x, -_normalScale.y, _normalScale.z);

        while (_grass.transform.localScale != cutScale)
        {
            _grass.transform.localScale = Vector3.MoveTowards(_grass.transform.localScale, cutScale, _cuttingSpeed);
            yield return null;
        }

        StartCoroutine(GrowingUp());
    }

    private IEnumerator GrowingUp()
    {
        yield return new WaitForSeconds(_timeToGrowUp);

        while (_grass.transform.localScale != _normalScale)
        {
            _grass.transform.localScale = Vector3.MoveTowards(_grass.transform.localScale, _normalScale, _growingSpeed);
            yield return null;
        }

        _crystalSpawner.Spawn();

        _isCutOff = false;
    }
}
