using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameWorld : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private Animation contentAnim;
    [SerializeField] private BattleSimulator battle;
    
    [Inject] private readonly GameController gameController;

    public void Init()
    {
        gameController.StartBattleEvent += ActivateContent;
        gameController.ExitBattleEvent += DeactivateContent;
        gameController.BattleLostEvent += DeactivateContent;

        battle.Init();
    }

    private void ActivateContent()
    {
        content.SetActive(true);

        battle.StartBattle();
    }

    private void DeactivateContent()
    {
        content.SetActive(false);
        battle.StopBattle();
    }

    public void AnimateHideContent()
    {
        contentAnim.Play("Hide");
        battle.StopBattle();
    }
}