using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanwave : MonoBehaviour
{
    public float maxScanDistance = 50f;
    public float speed = 100f;
    GameObject Player;


    // Аудио
    [FMODUnity.EventRef]
    public string[] fmodEvent = new string[3];
    private FMOD.Studio.EventInstance eventBeep;
    private FMOD.Studio.EventInstance eventMissileAlarm;
    private FMOD.Studio.EventInstance eventAmmoAlarm;


    public GameObject soundInstancePrefab;
    //private GameObject[] soundInstance = new GameObject[3];
    List<GameObject> soundInstances = new List<GameObject>();
    //


    void Start()
    {
        /// Звук
        eventBeep = FMODUnity.RuntimeManager.CreateInstance(fmodEvent[0]);
        eventBeep.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        eventMissileAlarm = FMODUnity.RuntimeManager.CreateInstance(fmodEvent[1]);
        eventMissileAlarm.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        eventAmmoAlarm = FMODUnity.RuntimeManager.CreateInstance(fmodEvent[2]);
        eventAmmoAlarm.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        ///
        Player = GameObject.FindGameObjectWithTag("Player");
        ///
        ScanWaveSound();
        StartCoroutine(StretchScanwave());
          
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ScanWaveSound()
    {
        soundInstances.Add(new GameObject("Scanwave"));
        ///Создаем и назначаем
        soundInstances[soundInstances.Count - 1] = Instantiate(soundInstancePrefab, transform);
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().audioInstance = eventBeep;
        ///Запуск звука
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().Execute();

        //eventBeep.start();

    }

    IEnumerator StretchScanwave()
    {
        while (transform.localScale.x < maxScanDistance)
        {
            transform.localScale = new Vector3(transform.localScale.x + speed * Time.deltaTime, transform.localScale.y, transform.localScale.z + speed * Time.deltaTime);

            yield return null;
            //yield return new WaitForSeconds(0.0001f);
        }
        Destroy(gameObject);        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision occured with " + other.name);
        CheckCollision(other);
    }

    void CheckCollision(Collider other)
    {
        if (transform.localScale.x < maxScanDistance * 0.9f)
        {
            if (transform.parent.GetComponent<Scanner>().currentScanMode == Scanner.scanMode.Missiles)
            {
                if (other.gameObject.tag == "Missile")
                {
                    MissileAlarm(other);
                }
            }

            if (transform.parent.GetComponent<Scanner>().currentScanMode == Scanner.scanMode.Ammo)
            {
                if (other.gameObject.tag == "Ammo")
                {
                    AmmoAlarm(other);
                }
            }
        }
    }

    void AmmoAlarm(Collider other)
    {
        soundInstances.Add(new GameObject("AmmoAlarm"));
        ///Создаем и назначаем
        soundInstances[soundInstances.Count - 1] = Instantiate(soundInstancePrefab, other.transform);
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().audioInstance = eventAmmoAlarm;
        ///Панорама
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().audioInstance.setParameterByName("AmmoAlarmPanning", CorrectPanning(other));
        ///Запуск звука
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().Execute();
        Debug.Log("ammoscanned");
    }

    void MissileAlarm(Collider other)
    {
        soundInstances.Add(new GameObject("MissileAlarm"));
        ///Создаем и назначаем
        soundInstances[soundInstances.Count - 1] = Instantiate(soundInstancePrefab, other.transform);
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().audioInstance = eventMissileAlarm;
        ///Панорама
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().audioInstance.setParameterByName("AlarmPanning", CorrectPanning(other));
        ///Сближение
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().audioInstance.setParameterByName("Proximity", CorrectProximity(other));
        UnityEngine.Debug.Log(CorrectProximity(other));
        ///Запуск звука
        soundInstances[soundInstances.Count - 1].GetComponent<RunAudioGetMarkers>().Execute();

    }

    float CorrectPanning(Collider other)
    {
        float panning;

        float zdist = other.transform.position.z - Player.transform.position.z;
        float xdist = other.transform.position.x - Player.transform.position.x;

        float angl = Mathf.Rad2Deg * Mathf.Atan(xdist / zdist);

        panning = angl;
        return panning;

        /*
        UnityEngine.Debug.Log("z = " + zdist);
        UnityEngine.Debug.Log("x = " + xdist);
        UnityEngine.Debug.Log("angl = " + angl);
        */
    }

    float CorrectProximity(Collider other)
    {
        float multiplier;
        multiplier = 100f / maxScanDistance;

        float proximity = 0;

        proximity = Mathf.Clamp((Vector3.Distance(other.transform.position, Player.transform.position)), 0, 100f);
        proximity = 100 - (multiplier * proximity);

        return proximity;
    }

}

