using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter _ContainerCounter;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _ContainerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(Constants.OPEN_CLOSE);
    }
}