using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image _barImage;

    private void Start()
    {
        _cuttingCounter.OnProgressChanged += _cuttingCounter_OnProgressChanged;
        Hide();
    }

    private void _cuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        _barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized != 0 && e.progressNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
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