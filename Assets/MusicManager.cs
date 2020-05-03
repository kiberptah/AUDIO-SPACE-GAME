using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    // Звуки
    [FMODUnity.EventRef]
    public string eventAmbPath;
    [FMODUnity.EventRef]
    public string eventKickLoopPath;

    private FMOD.Studio.EventInstance eventAmb;
    private FMOD.Studio.EventInstance eventKickLoop;
    // Start is called before the first frame update
    void Start()
    {
        eventAmb = FMODUnity.RuntimeManager.CreateInstance(eventAmbPath);
        eventAmb.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        eventAmb.start();
        eventAmb.release();

        eventKickLoop = FMODUnity.RuntimeManager.CreateInstance(eventKickLoopPath);
        eventKickLoop.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(GameObject.Find("FMOD_StudioSystem"));


    }

    // Update is called once per frame
    void Update()
    {
        /*
        int timel;
        eventAmb.getTimelinePosition(out timel);
        Debug.Log(timel);
        */
    }

    public void startKickLoop()
    {
        eventKickLoop.start();
    }
}
