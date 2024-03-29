using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _BaseCounter;

    [SerializeField] private GameObject[] _visualGameObjectArray;

    private void Start()
    {
        if (Player.LocalInstance == null)
        {
            Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
        }
        else
        {
            Player.LocalInstance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

    }
    
    private void Player_OnAnyPlayerSpawned(object sender, System.EventArgs e)   
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChanged -= Player_OnSelectedCounterChanged;
            Player.LocalInstance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == _BaseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var visualGameObject in _visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visualGameObject in _visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}