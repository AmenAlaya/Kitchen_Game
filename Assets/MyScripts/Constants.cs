using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Constants
{
    #region Animation

    public const string IS_WALKING = "IsWalking";
    public const string OPEN_CLOSE = "OpenClose";
    public const string CUT = "Cut";

    #endregion Animation

    #region Varibales

    public const float SPAWN_PLATE_TIMER_MAX = 5f;
    public const int PLATE_SPAWNED_AMOUNT_MAX = 4;

    #endregion Varibales

    #region Player Prefs
    public const string PLAYER_PREFS_SOUND_EFFECT_VOLUME = "SoundEffectVolume";
    public const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public const string PLAYER_PREFS_BINDINGS = "InputBindings";
    #endregion
}