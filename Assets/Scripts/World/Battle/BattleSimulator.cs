using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleSimulator : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private EnemySimulator enemySimulator;
    [SerializeField] private BulletsContainer bulletsContainer;

    [Inject] private readonly GameController gameController;

    public void Init()
    {
        tower.Init(bulletsContainer.Shoot, gameController.BattleLost);
        enemySimulator.Init(tower.Damage);
    }

    public void StartBattle()
    {
        tower.Reset();
        enemySimulator.StartSimulate(tower.transform);
    }

    public void StopBattle()
    {
        enemySimulator.StopSimulate();
    }
}