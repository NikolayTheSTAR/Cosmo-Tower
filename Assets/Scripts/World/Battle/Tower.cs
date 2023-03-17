using System;
using UnityEngine;

public class Tower : MonoBehaviour, IUpgradeReactable
{
    [SerializeField] private CircleCollider2D trigger;
    [SerializeField] private Transform arenaCircleTran;

    private float defaultRadius = 1.5f;
    private HpOwner hpOwner;
    private AutoShooter autoShooter;

    public void Init(Action<Shooter, HpOwner, float> ShootAction, Action OnTowerDestroyed)
    {
        hpOwner = new(transform, 5, OnTowerDestroyed);
        autoShooter = new(transform);
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

    public void OnUpgradeReact(UpgradeType upgradeType, float value)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                autoShooter.SetForce(value);
                break;

            case UpgradeType.AttackSpeed:
                autoShooter.SetPeriod(1 / value);
                break;

            case UpgradeType.AttackDistance:
                trigger.radius = defaultRadius * value;
                arenaCircleTran.localScale = new Vector2(value, value);
                break;
        }
    }
}