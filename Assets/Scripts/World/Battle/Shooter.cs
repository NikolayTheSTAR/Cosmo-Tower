using System;
using System.Collections.Generic;
using UnityEngine;

public class Shooter
{
    private float _force;

    public event Action<Shooter, HpOwner, float> ShootEvent;

    public Shooter(float force)
    {
        _force = force;
    }

    public virtual void Shoot(HpOwner goal)
    {
        ShootEvent?.Invoke(this, goal, _force);
    }
}