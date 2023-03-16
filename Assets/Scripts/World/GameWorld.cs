using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameWorld : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private Animation contentAnim;
    [SerializeField] private Tower tower;
    [SerializeField] private EnemySimulator enemySimulator;

    [Inject] private readonly GameController gameController;

    public void Init()
    {
        gameController.StartBattleEvent += ActivateContent;
        gameController.ExitBattleEvent += DeactivateContent;
        gameController.BattleLostEvent += DeactivateContent;

        enemySimulator.Init(tower.Damage);

        tower.OnTowerDestroyed += gameController.BattleLost;
    }

    private void ActivateContent()
    {
        content.SetActive(true);
        tower.InitForBattle(5);

        enemySimulator.StartSimulate(tower.transform);
    }

    private void DeactivateContent()
    {
        content.SetActive(false);
        enemySimulator.StopSimulate();
    }

    public void AnimateHideContent()
    {
        contentAnim.Play("Hide");
        enemySimulator.StopSimulate();
    }
}