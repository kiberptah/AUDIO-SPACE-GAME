using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;


public class SoundManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string soundPlayerDeath;
    [FMODUnity.EventRef]
    public string AISpeech_1_Path;
    [FMODUnity.EventRef]
    public string AISpeech_2_Path;
    [FMODUnity.EventRef]
    public string AISpeech_3_Path;
    [FMODUnity.EventRef]
    public string AISpeech_4_Path;


    public FMOD.Studio.EventInstance eventPlayerDeath;
    public FMOD.Studio.EventInstance AISpeech_1;
    public FMOD.Studio.EventInstance AISpeech_2;
    public FMOD.Studio.EventInstance AISpeech_3;
    public FMOD.Studio.EventInstance AISpeech_4;


    // Start is called before the first frame update
    void Start()
    {
        eventPlayerDeath = FMODUnity.RuntimeManager.CreateInstance(soundPlayerDeath);
        eventPlayerDeath.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        AISpeech_1 = FMODUnity.RuntimeManager.CreateInstance(AISpeech_1_Path);
        AISpeech_1.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        AISpeech_2 = FMODUnity.RuntimeManager.CreateInstance(AISpeech_2_Path);
        AISpeech_2.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        AISpeech_3 = FMODUnity.RuntimeManager.CreateInstance(AISpeech_3_Path);
        AISpeech_3.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        AISpeech_4 = FMODUnity.RuntimeManager.CreateInstance(AISpeech_4_Path);
        AISpeech_4.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Sound_PlayerDeath()
    {      
        eventPlayerDeath.start();
    }
    public void AI_Speech_1()
    {
        AISpeech_1.start();
        AISpeech_1.release();
    }

    public void AI_ChargeSpeech()
    {
        AISpeech_2.start();
        AISpeech_2.release();
    }

    public void AI_HyperDriveChargeHalf()
    {
        AISpeech_3.start();
        AISpeech_3.release();
    }



    public void AI_FinalSpeech()
    {
        AISpeech_4.start();
        AISpeech_4.release();
    }

}
