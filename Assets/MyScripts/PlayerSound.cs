using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player _player;

    private float _footStepsTimer;
    private float _footStpsTimerMax = .1f;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _footStepsTimer -= Time.deltaTime;

        if (_footStepsTimer < 0)
        {
            _footStepsTimer = _footStpsTimerMax;
            if (_player.IsWalking())
            {
                SoundManager.Instance.PlayerFootSetpsSounds(transform.position, 1f);
            }
        }
    }
}
