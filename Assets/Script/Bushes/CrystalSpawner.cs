using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    [SerializeField] private List<Crystal> _crystalTemplates;
    [SerializeField] private float _crystalSpawnRandomizer;
    [SerializeField] private int _minRandom;
    [SerializeField] private int _maxRandom;
    [SerializeField] private int _spawnChance;
    [SerializeField] private float _minStartSpawTime;
    [SerializeField] private float _maxStartSpawTime;

    private CrystalHolder _crystalHolder;

    private void Awake()
    {
        _crystalHolder = GetComponentInParent<CrystalHolder>();
    }

    public void Spawn()
    {
        if (Random.Range(_minRandom, _maxRandom + 1) <= _spawnChance)
        {
            var crystalSpawnPosition = transform.position + new Vector3(Random.Range(-_crystalSpawnRandomizer, _crystalSpawnRandomizer), 0, Random.Range(-_crystalSpawnRandomizer, _crystalSpawnRandomizer));
            Instantiate(_crystalTemplates[Random.Range(0, _crystalTemplates.Count)], crystalSpawnPosition, Quaternion.identity, _crystalHolder.transform);
        }
    }

    public void SpawnAtStart()
    {
        StartCoroutine(RandomizeSpawnTime());
    }

    private IEnumerator RandomizeSpawnTime()
    {
        yield return new WaitForSeconds(Random.Range(_minStartSpawTime, _maxStartSpawTime));

        Spawn();
    }
}
