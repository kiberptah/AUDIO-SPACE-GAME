using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAudioInstance : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField]
    public FMOD.Studio.EventInstance audioInstance;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("WTF");
        //audioInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        audioInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        audioInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAudioInstance(FMOD.Studio.EventInstance instance)
    {
        audioInstance = instance;
    }
}
