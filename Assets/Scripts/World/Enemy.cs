using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 1;
    private float _force = 1;
    private float _neededGoalDistance = 0.25f;

    public float Force => _force;

    private Action<Enemy> OnGoalReached;

    public void Init(Action<Enemy> goalReachedAction) => OnGoalReached = goalReachedAction;

    public void MoveTo(Transform goal)
    {
        var offset = goal.transform.position - transform.position;

        var distance = Vector2.Distance(Vector2.zero, offset);
        if (distance <= _neededGoalDistance)
        {
            OnGoalReached?.Invoke(this);
            return;
        }

            var direction = offset.normalized;
        var step = _speed * Time.deltaTime * direction;

        transform.Translate(step);
    }
}