using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HpOwner
{
    private float _currentHp;
    private float _maxHp;
    private HpOwnerStatus _status;
    private Action onDeadAction;

    private Transform transform;
    public Transform Transform => transform;

    public HpOwner(Transform transform, float maxHp, Action onDeadAction)
    {
        this.transform = transform;
        this._maxHp = maxHp;
        _currentHp = maxHp;
        _status = HpOwnerStatus.Alive;

        this.onDeadAction = onDeadAction;
    }

    public void Reset()
    {
        _currentHp = _maxHp;
    }

    public void Damage(float force)
    {
        _currentHp -= force;

        if (_currentHp <= 0)
        {
            _currentHp = 0;
            Die();
        }
    }

    private void Die() => onDeadAction?.Invoke();
}

public enum HpOwnerStatus
{
    Alive,
    Dead
}