using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float _currentHp;
    private int _maxHp;
    private TowerStatus _status;

    public event Action OnTowerDestroyed;

    public void Init(int maxHp)
    {
        this._maxHp = maxHp;
        _currentHp = maxHp;

        _status = TowerStatus.Alive;
    }

    [ContextMenu("TestDamage")]
    private void TestDamage()
    {
        Damage(_maxHp);
    }

    private void Damage(int force)
    {
        _currentHp -= force;

        if (_currentHp <= 0) Destroy();
    }

    private void Destroy()
    {
        _status = TowerStatus.Destroyed;
        OnTowerDestroyed?.Invoke();
    }
}

public enum TowerStatus
{
    Alive,
    Destroyed
}