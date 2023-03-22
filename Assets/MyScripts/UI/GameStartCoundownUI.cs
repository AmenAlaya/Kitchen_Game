using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Search;
using System;

public class GameStartCoundownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;

    private void Start()
    {
        KitcheGameManager.Instance.OnStateChanged += kitchenGameManager_OnStateChanged;
        Hide();
    }

    private void kitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitcheGameManager.Instance.IsCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        _countdownText.text = Mathf.Ceil( KitcheGameManager.Instance.GetCountdownStartTime()).ToString();
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
