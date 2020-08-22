using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages playing audio sound effect and music.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Enums
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Current possible sound effects to be played.
    /// </summary>
    public enum AudioType
    {
        None,
        Point,
        Hit,
        Appear,
    }

    /// <summary>
    /// Current possible music to be played.
    /// </summary>
    public enum MusicType
    {
        None,
        MusicMenu,
        MusicLevel1,
        MusicClear,
        MusicScore,
    }

    /*--------------------------------------------------------------------------------
        References
    --------------------------------------------------------------------------------*/
    [Header("Sound FX")]
    /// <summary>
    /// All sound effect AudioSources.
    /// </summary>
    public AudioSource audioPoint;
    public AudioSource audioHit;
    public AudioSource audioAppear;

    [Header("Music")]
    /// <summary>
    /// All music AudioSources.
    /// </summary>
    public AudioSource audioMusicMenu;
    public AudioSource audioMusicLevel1;
    public AudioSource audioMusicLevelClear;
    public AudioSource audioMusicScore;

    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Holds coroutine that keeps track when current playing music is done playing.
    /// </summary>
    private Coroutine coroutinePlayMusicDone;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        PlayFx
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Play a sound effect by AudioType.
    /// </summary>
    /// <param name="audioType">Sound effect to play.</param>
    /// <param name="delay">Delay before sound effect is actually played.</param>
    public void PlayFx(AudioType audioType, float delay=0f)
    {
        StartCoroutine(PlayAudioSourceFX(audioType, delay));
    }
    /*--------------------------------------------------------------------------------
        PlayMusic
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Play music by MusicType.
    /// </summary>
    /// <param name="musicType">Music to play.</param>
    /// <param name="callbackMusicFinished">Callback invokes whenever current music has stopped playing.</param>
    public void PlayMusic(MusicType musicType, Action callbackMusicFinished = null)
    {
        // stop current coroutine of music playing, if it exists.
        if(coroutinePlayMusicDone != null)
        {
            StopCoroutine(coroutinePlayMusicDone);
        }

        // stop all music current playing.
        StopAllMusic();

        float musicDuration = 0f;

        switch (musicType)
        {
            case MusicType.MusicMenu:
                audioMusicMenu.Play();
                musicDuration = audioMusicMenu.clip.length;
                break;
            case MusicType.MusicLevel1:
                audioMusicLevel1.Play();
                musicDuration = audioMusicLevel1.clip.length;
                break;
            case MusicType.MusicClear:
                audioMusicLevelClear.Play();
                musicDuration = audioMusicLevelClear.clip.length;
                break;
            case MusicType.MusicScore:
                audioMusicScore.Play();
                musicDuration = audioMusicScore.clip.length;
                break;
            default:
                break;
        }

        // coroutine firing callback whenever music has stopped playing. Duration is based on music audio clip length.
        coroutinePlayMusicDone = StartCoroutine(CoroutinePlayMusicDone(musicDuration, callbackMusicFinished));
    }

    /*################################################################################
        Functions: private
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        PlayAudio
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Play audio clip from AudioSource.
    /// </summary>
    /// <param name="audioType">Audio effect to play.</param>
    /// <param name="delay">Delay in seconds before audio is actually played.</param>
    /// <returns></returns>
    private IEnumerator PlayAudioSourceFX(AudioType audioType, float delay)
    {
        yield return new WaitForSeconds(delay);

        switch (audioType)
        {
            case AudioType.Point:
                audioPoint.Play();
                break;
            case AudioType.Hit:
                audioHit.Play();
                break;
            case AudioType.Appear:
                audioAppear.Play();
                break;
            default:
                break;
        }
    }
    /*--------------------------------------------------------------------------------
        CoroutinePlayMusicDone
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Coroutine which invokes callback after a certain duration.
    /// </summary>
    /// <param name="delay">Delay before callback is invoked.</param>
    /// <param name="callbackMusicFinished">Callback to invoke when delay has finished.</param>
    /// <returns></returns>
    private IEnumerator CoroutinePlayMusicDone(float delay, Action callbackMusicFinished)
    {
        yield return new WaitForSeconds(delay);
        callbackMusicFinished?.Invoke();
    }
    /*--------------------------------------------------------------------------------
        StopAllMusic
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Stop all music currently playing.
    /// </summary>
    public void StopAllMusic()
    {
        audioMusicMenu.Stop();
        audioMusicLevel1.Stop();
        audioMusicLevelClear.Stop();
        audioMusicScore.Stop();
    }
}
