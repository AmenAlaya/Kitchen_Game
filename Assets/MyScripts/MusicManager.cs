using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; } 



    private AudioSource _audioSource;
    private float _volume=.3f;

    private void Awake()
    {
        Instance = this;


        _audioSource= GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat(Constants.PLAYER_PREFS_MUSIC_VOLUME, 1f);
        _audioSource.volume = _volume;
    }


    public void ChangeVolume()
    {
        _volume += .1f;

        if (_volume > 1f)
        {
            _volume = 0;
        }
        _audioSource.volume = _volume;

        PlayerPrefs.SetFloat(Constants.PLAYER_PREFS_MUSIC_VOLUME, _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return _volume;
    }
}
