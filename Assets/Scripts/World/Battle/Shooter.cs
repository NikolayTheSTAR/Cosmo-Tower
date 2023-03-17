using System;
using System.Collections.Generic;
using UnityEngine;

public class Shooter
{
    private float _force;
    private Transform _transform;

    public event Action<Shooter, HpOwner, float> ShootEvent;

    public Transform Transform => _transform;

    public Shooter(Transform transform, float force)
    {
        _force = force;
        _transform = transform;
    }

    public virtual void Shoot(HpOwner goal)
    {
        ShootEvent?.Invoke(this, goal, _force);
    }
}