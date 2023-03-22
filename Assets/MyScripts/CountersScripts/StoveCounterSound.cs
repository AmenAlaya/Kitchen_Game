using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter _deliveryCounter;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource= GetComponent<AudioSource>();
    }

    private void Start()
    {
        _deliveryCounter.OnStateChanged += DeliveryCounter_OnStateChanged;
    }

    private void DeliveryCounter_OnStateChanged(object sender, StoveCounter.OnstateChangedEventArgs e)
    {
        bool isCoocking = e.state == StoveCounter.State.frying || e.state==StoveCounter.State.fried;
        
        if(isCoocking)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
