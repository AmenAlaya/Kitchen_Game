using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static StoveCounter;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _particlesGameObject;

    private void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounter_OnstateChanged;
    }

    private void StoveCounter_OnstateChanged(object sender, StoveCounter.OnstateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.frying || e.state == StoveCounter.State.fried;
        _stoveOnGameObject.SetActive(showVisual);
        _particlesGameObject.SetActive(showVisual);
    }
}