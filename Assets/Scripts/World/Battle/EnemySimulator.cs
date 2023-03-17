using System;
using System.Collections.Generic;
using UnityEngine;
using TheSTAR.Utility;
using Random = UnityEngine.Random;

public class EnemySimulator : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemyParent;
    
    private List<Enemy> enemiesPool = new ();
    private List<Enemy> activeEnemies = new ();
    private bool _isSimulate = false;
    private float _spawnPeriodMin = 0.25f;
    private float _spawnPeriodMax = 3f;
    private float _spawnDistance = 5f;
    private TimeCycleControl _spawnControl;
    private Transform _enemyGoal;
    private Action<float> _attackAction;

    public void Init(Action<float> attackAction)
    {
        _attackAction = attackAction;
    }

    public void StartSimulate(Transform goal)
    {
        if (_isSimulate) return;

        _enemyGoal = goal;

        _isSimulate = true;

        _spawnControl = TimeUtility.While(() => _isSimulate, _spawnPeriodMin, _spawnPeriodMax, SpawnEnemy);
    }

    public void StopSimulate()
    {
        if (!_isSimulate) return;

        _isSimulate = false;
        _spawnControl.Stop();

        activeEnemies.Clear();

        HideEnemies();
    }

    private void SpawnEnemy()
    {
        if (!_isSimulate) return;

        Vector2 spawnPos =
            transform.position +
            (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _spawnDistance);
        var enemy = PoolUtility.GetPoolObject(enemiesPool, info => !info.gameObject.activeSelf, spawnPos, CreateNewEnemy);

        activeEnemies.Add(enemy);

        Enemy CreateNewEnemy(Vector2 pos)
        {
            var e = Instantiate(enemyPrefab, pos, Quaternion.identity, enemyParent);
            e.Init(OnEnemyReachedGoal, OnEnemyDead);
            enemiesPool.Add(e);
            return e;
        }
    }

    public void Update()
    {
        if (!_isSimulate) return;

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            Enemy enemy = activeEnemies[i];
            enemy.MoveTo(_enemyGoal);
        }
    }

    private void OnEnemyReachedGoal(Enemy e)
    {
        _attackAction?.Invoke(e.Force);
        e.Die();
    }

    private void OnEnemyDead(Enemy e) => activeEnemies.Remove(e);

    private void HideEnemies()
    {
        foreach (var e in enemiesPool) e.gameObject.SetActive(false);
    }
}