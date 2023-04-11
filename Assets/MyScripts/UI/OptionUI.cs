using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _soundEffectsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _interactButton;
    [SerializeField] private Button _interactAlternetButton;
    [SerializeField] private Button _pauseButton;

    [SerializeField] private Button _gamepadInteractButton;
    [SerializeField] private Button _gamepadInteractAlternetButton;
    [SerializeField] private Button _gamepadPauseButton;

    [SerializeField] private TextMeshProUGUI _soundEffectText;
    [SerializeField] private TextMeshProUGUI _musicText;
    [SerializeField] private TextMeshProUGUI _moveUPText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _interactAlternetText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractAlternetText;
    [SerializeField] private TextMeshProUGUI _gamepadPauseText;

    [SerializeField] private Transform _pressToRebundKeyTransform;

    private Action _onCloseButtionAction;

    private void Awake()
    {
        Instance = this;

        _soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });


        _musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });


        _closeButton.onClick.AddListener(() =>
        {
            Hide();
            _onCloseButtionAction();
        });

        _moveUpButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.Move_Up);
        });

        _moveDownButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.Move_Down);
        });

        _moveLeftButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.Move_Left);
        });

        _moveRightButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.Move_Right);
        }
        );

        _interactButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.Interact);
        });

        _interactAlternetButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.InteractAlternate);
        });

        _gamepadInteractButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.Gamepad_Interact);
        });

        _gamepadInteractAlternetButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });

        _gamepadPauseButton.onClick.AddListener(() =>
        {
            RebinBinding(GameInput.Binding.Gamepad_Pause);
        });

    }
    private void Start()
    {
        KitcheGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;

        Hide();
        HidePressToRebindKey();
        UpdateVisual();
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        _soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        _musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        _moveUPText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Move_Up);
        _moveDownText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Move_Down);
        _moveLeftText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Move_Left);
        _moveRightText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Move_Right);
        _interactText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Interact);
        _interactAlternetText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.InteractAlternate);
        _pauseText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Pause);
        _gamepadInteractText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Gamepad_Interact);
        _gamepadInteractAlternetText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        _gamepadPauseText.text = GameInput.Instantce.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this._onCloseButtionAction= onCloseButtonAction;
        gameObject.SetActive(true);
        _soundEffectsButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        _pressToRebundKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        _pressToRebundKeyTransform.gameObject.SetActive(false);
    }

    private void RebinBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instantce.RebindBinding(binding,()=> {
            HidePressToRebindKey();
            UpdateVisual();
            });
    }

}
