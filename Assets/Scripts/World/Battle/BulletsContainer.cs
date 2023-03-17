using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsContainer : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    public void Shoot(Shooter shooter, HpOwner goal, float force)
    {
        Instantiate(bulletPrefab, goal.Transform.position, Quaternion.identity, transform);
    }
}