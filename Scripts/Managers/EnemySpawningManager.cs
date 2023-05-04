using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _cooldownBetweenWaves;
    [SerializeField] private int _spawnCount;
    [SerializeField] private int _numberOfWaves;
    [SerializeField] private Mutant _enemy;
    [SerializeField] private Transform[] spawnPositions;

    void Start()
    {
        StartCoroutine(Spawn());
        if (_numberOfWaves == 0)
        {
            StopCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        while (_spawnCount > 0)
        {
            int randomSpawn = Random.Range(0, 4);
            Instantiate(_enemy, spawnPositions[randomSpawn].position, spawnPositions[randomSpawn].rotation);
            _spawnCount--;
            yield return new WaitForSeconds(_cooldown);
        }
        yield return new WaitForSeconds(_cooldownBetweenWaves);
        _numberOfWaves--;
        StartCoroutine(Spawn());
    }
}
