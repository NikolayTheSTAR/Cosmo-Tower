using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameWorld : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private Animation contentAnim;
    [SerializeField] private Tower tower;

    [Inject] private readonly GameController gameController;

    public void Init()
    {
        gameController.StartBattleEvent += ActivateContent;
        gameController.ExitBattleEvent += DeactivateContent;
        gameController.BattleLostEvent += DeactivateContent;

        tower.OnTowerDestroyed += gameController.BattleLost;
    }

    private void ActivateContent()
    {
        content.SetActive(true);
        tower.Init(5);
    }

    private void DeactivateContent() => content.SetActive(false);

    public void AnimateHideContent() => contentAnim.Play("Hide");
}