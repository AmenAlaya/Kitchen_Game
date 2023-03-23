using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipesDeliveredText;


    private void Start()
    {
        KitcheGameManager.Instance.OnStateChanged += kitchenGameManager_OnStateChanged;
        Hide();
    }

    private void kitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitcheGameManager.Instance.IsGameOver())
        {
            Show();

            _recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        }
        else
        {
            Hide();
        }
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
