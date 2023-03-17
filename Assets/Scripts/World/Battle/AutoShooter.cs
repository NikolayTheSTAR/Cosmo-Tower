using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSTAR.Utility;

public class AutoShooter : Shooter
{
    private float _period;
    private List<HpOwner> _goals;
    private bool _isSimulate;
    private AutoShooterStatus _status;

    public AutoShooter(float period, float force) : base (force)
    {
        _status = AutoShooterStatus.ReadyToShoot;
        _period = period;
        _goals = new();
    }

    public void StartSimulateShooting()
    {
        if (_isSimulate) return;

        _isSimulate = true;
    }

    public void StopSimulateShooting()
    {
        if (!_isSimulate) return;

        _isSimulate = false;
    }

    public void AddGoal(HpOwner goal)
    {
        _goals.Add(goal);
        TryShoot();
    }

    public void RemoveGoal(HpOwner goal) => _goals.Remove(goal);

    private void TryShoot()
    {
        if (_status == AutoShooterStatus.ReadyToShoot && _goals.Count > 0) Shoot(_goals[0]);
    }

    public override void Shoot(HpOwner goal)
    {
        base.Shoot(goal);
        StartRecharging();
    }

    private void StartRecharging()
    {
        _status = AutoShooterStatus.Recharging;
        TimeUtility.Wait(_period, EndRecharging);
    }

    private void EndRecharging()
    {
        _status = AutoShooterStatus.ReadyToShoot;
        TryShoot();
    }
}

public enum AutoShooterStatus
{
    ReadyToShoot,
    Recharging
}