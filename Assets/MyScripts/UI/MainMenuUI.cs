using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playeButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playeButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainGame_Scene);
        });

        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit(); 
        });

        Time.timeScale= 1.0f;
    }
}
