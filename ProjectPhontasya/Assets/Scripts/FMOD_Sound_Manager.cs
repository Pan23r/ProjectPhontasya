using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class FMOD_Sound_Manager : MonoBehaviour
{
    public static float volume = 1;
    private static VCA _vca;
    private static StudioEventEmitter _soundtrackEmitter;

    public static void SetVolume(float newVolume)
    {
        volume = newVolume;
        _vca.setVolume(volume);
    }

    // Start is called before the first frame update
    void Start()
    {
        _vca = RuntimeManager.GetVCA("vca:/MasterVolume");
        _vca.setVolume(volume);

        if(SceneManager.GetActiveScene().name == "GameMenu")
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                _soundtrackEmitter = (transform.GetChild(i).name == "Game_Soundtrack") ? transform.GetChild(i).GetComponent<StudioEventEmitter>() : _soundtrackEmitter;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //TODO ABILITARE QUANDO CI SARA' IL LOAD GAME 
        /*if (SceneManager.GetActiveScene().name == "Girl_BedRoom" && Load_Manager.playMusicOnLoad)
        {
            Play();
            InGame(7);
            Load_Manager.playMusicOnLoad = false;
        }*/

        if (!_soundtrackEmitter.IsPlaying())
            Play();
    }

    static void CreatePlay(StudioEventEmitter emitter)
    {
        if (emitter != null)
            emitter.Play();
        else
            Debug.LogError("EMITTER: NULL");
    }
    public static void GameSoundtrackParameter(int parameter)
    {
        _soundtrackEmitter.SetParameter("GameSoundtrackParameter", parameter);
    }
    
    public static void Stop()
    {
        _soundtrackEmitter.Stop();
    }

    public static void Play()
    {
        CreatePlay(_soundtrackEmitter);
    }

}
