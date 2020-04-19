using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;


public class SoundManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string soundPlayerDeath;
    [FMODUnity.EventRef]
    public string soundAmbience_1;



    public FMOD.Studio.EventInstance eventPlayerDeath;
    public FMOD.Studio.EventInstance eventMusic_1;

    // Start is called before the first frame update
    void Start()
    {
        eventPlayerDeath = FMODUnity.RuntimeManager.CreateInstance(soundPlayerDeath);
        eventPlayerDeath.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        eventMusic_1 = FMODUnity.RuntimeManager.CreateInstance(soundAmbience_1);
        eventMusic_1.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sound_PlayerDeath()
    {
        
        eventPlayerDeath.start();

        //Debug.Log("player death sound");
    }

    public void Sound_StartAmbience()
    {
        eventMusic_1.start();
    }
}
