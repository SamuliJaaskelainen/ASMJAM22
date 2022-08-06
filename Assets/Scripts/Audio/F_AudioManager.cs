using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_AudioManager : MonoBehaviour
{
    public static F_AudioManager instance;

    private FMOD.Studio.EventInstance MainTrack;
    private FMOD.Studio.EventInstance MenuTrack;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MainTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MainTrack");
        MenuTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MenuTrack");
    }

    public void PlayMainTrack()
    {
        MainTrack.start();
    }

    public void StopMainTrack()
    {
        MainTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MainTrack.release();
    }

    public void PlayMenuTrack()
    {
        MenuTrack.start();
    }

    public void StopMenuTrack()
    {
        MenuTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MenuTrack.release();
    }
}
