using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private HpOwner hpOwner;
    private AutoShooter autoShooter;

    public void Init(Action<Shooter, HpOwner, float> ShootAction, Action OnTowerDestroyed)
    {
        hpOwner = new(transform, 5, OnTowerDestroyed);
        autoShooter = new(transform, 1, 1);

        autoShooter.ShootEvent += ShootAction;
    }

    public void Reset() => hpOwner.Reset();

    public void Damage(float force) => hpOwner.Damage(force);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) autoShooter.AddGoal(collision.GetComponent<Enemy>().HpOwner);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) autoShooter.RemoveGoal(collision.GetComponent<Enemy>().HpOwner);
    }
}