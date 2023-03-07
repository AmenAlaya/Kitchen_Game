using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter _ContainerCounter;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _ContainerCounter.Oncut += ContainerCounter_Oncut;
    }

    private void ContainerCounter_Oncut(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(Constants.CUT);
    }
}