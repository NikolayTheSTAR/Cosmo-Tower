using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameWorld : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private Animation contentAnim;

    [Inject] private readonly GameController gameController;

    public void Init()
    {
        gameController.StartGameEvent += ActivateContent;
        gameController.AnimateExitGameEvent += AnimateHideContent;
        gameController.ExitGameEvent += DeactivateContent;
    }

    private void ActivateContent() => content.SetActive(true);

    private void DeactivateContent() => content.SetActive(false);

    private void AnimateHideContent() => contentAnim.Play("Hide");
}