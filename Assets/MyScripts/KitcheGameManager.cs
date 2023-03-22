using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitcheGameManager : MonoBehaviour
{
    public static KitcheGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;
    private float _watingStartTimer = 1f;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingtTimer = 10f;

    private void Awake()
    {
        Instance= this; 
    }


    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _watingStartTimer-= Time.deltaTime;
                if (_watingStartTimer < 0f)
                {
                    _state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this,EventArgs.Empty);
                }
                break;
                case State.CountdownToStart:
                _countdownToStartTimer-= Time.deltaTime;
                if(_countdownToStartTimer < 0f)
                {
                    _state= State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
                case State.GamePlaying:
                _gamePlayingtTimer -= Time.deltaTime;
                if (_gamePlayingtTimer < 0f)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
                case State.GameOver:
                break;
        }

        Debug.Log(_state);

    }


    public bool isGamePlaying()
    {
        return _state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive()
    {
        return _state!= State.CountdownToStart;
    }

    public float GetCountdownStartTime()
    {
        return _countdownToStartTimer;
    }
}
