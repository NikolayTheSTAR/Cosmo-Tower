using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsContainer : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    private List<Bullet> _activeBullets = new ();

    private bool _isSimulate = false;

    public void Shoot(Shooter shooter, HpOwner goal, float force)
    {
        var bullet = Instantiate(bulletPrefab, shooter.Transform.position, Quaternion.identity, transform);
        bullet.Init(1, goal, OnBulletReachedGoal);
        _activeBullets.Add(bullet);
    }

    public void StartSimulate()
    {
        if (_isSimulate) return;

        _isSimulate = true;
    }

    public void StopSimulate()
    {
        if (!_isSimulate) return;

        _isSimulate = false;
    }

    private void Update()
    {
        if (!_isSimulate) return;

        for (int i = 0; i < _activeBullets.Count; i++)
        {
            Bullet b = _activeBullets[i];
            b.MoveToGoal();
        }
    }

    private void OnBulletReachedGoal(Bullet b)
    {
        _activeBullets.Remove(b);
    }
}