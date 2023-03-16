using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSTAR.Utility;

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

    public void StartSimulate(Transform goal)
    {
        _enemyGoal = goal;

        _isSimulate = true;

        _spawnControl = TimeUtility.While(() => _isSimulate, _spawnPeriodMin, _spawnPeriodMax, SpawnEnemy);
    }

    public void StopSimulate()
    {
        if (!_isSimulate) return;

        _isSimulate = false;
        _spawnControl.Stop();

        HideEnemies();
    }

    private void SpawnEnemy()
    {
        if (!_isSimulate) return;

        Vector2 spawnPos =
            transform.position +
            (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _spawnDistance);
        var enemy = GetEnemyObject(spawnPos);

        activeEnemies.Add(enemy);
    }

    private Enemy GetEnemyObject(Vector2 pos)
    {
        if (enemiesPool.Count == 0) return Create();
        else
        {
            var e = enemiesPool.Find(info => !info.gameObject.activeSelf);

            if (e == null) return Create();
            else
            {
                e.transform.position = pos;
                e.gameObject.SetActive(true);
                return e;
            }
        }

        Enemy Create()
        {
            var e = Instantiate(enemyPrefab, pos, Quaternion.identity, enemyParent);
            e.Init(OnEnemyReachedGoal);
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
        activeEnemies.Remove(e);
        e.gameObject.SetActive(false);
    }

    private void HideEnemies()
    {
        foreach (var e in enemiesPool) e.gameObject.SetActive(false);
    }
}