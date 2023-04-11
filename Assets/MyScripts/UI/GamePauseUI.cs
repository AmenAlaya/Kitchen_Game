using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionButton;
    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitcheGameManager.Instance.TagglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu_Scene);
        });

        optionButton.onClick.AddListener(() =>
        {
            Hide();
            OptionUI.Instance.Show(Show);
        });

    }


    private void Start()
    {
        KitcheGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitcheGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;
        Hide();
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
        resumeButton.Select();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
