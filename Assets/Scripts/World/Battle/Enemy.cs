using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _force = 1;
    private HpOwner _hpOwner;
    private MovableToGoal _movableToGoal;

    public float Force => _force;
    public HpOwner HpOwner => _hpOwner;

    private Action<Enemy> _onDeadAction;

    public void Init(Action<Enemy> goalReachedAction, Action<Enemy> deadAction)
    {
        _hpOwner = new(transform, 1, Die);
        _movableToGoal = new(transform, 1, 0.25f, () => goalReachedAction?.Invoke(this));
        _onDeadAction = deadAction;
    }

    public void MoveTo(Transform goal) => _movableToGoal.MoveTo(goal);

    public void Die()
    {
        gameObject.SetActive(false);
        _onDeadAction(this);
    }
}