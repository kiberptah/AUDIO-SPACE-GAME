using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using System;

using System.Runtime.InteropServices;

public class ShootLaser : MonoBehaviour
{
    /// Звуки
    public GameObject soundInstancePrefab;
    private GameObject[] soundInstance = new GameObject[3];

    [FMODUnity.EventRef]
    public string[] fmodEvent = new string[4];

    private FMOD.Studio.EventInstance eventCharge;
    private FMOD.Studio.EventInstance eventShoot;
    private FMOD.Studio.EventInstance eventCancel;

    private FMOD.Studio.PARAMETER_ID resParameterID;
    private FMOD.Studio.PARAMETER_ID panParameterID;

    public float resonance;

    /// Логика
    bool diditfired = true;
    enum shootingStatus
    {
        Ready,
        StartingCharge,
        Charging,
        Fire,
        Waiting
    }
    shootingStatus status = shootingStatus.Waiting;

    
    float lookDirection;
    private List<GameObject> missiles = new List<GameObject>();

    /// Стрельба 
    public GameObject laserPrefab;
    GameObject theLaser;
    public float laserSpeed = 10f;

    public int ammo;
    public int defaultAmmo = 10;

    public float cooldown;
    
    

    void Start()
    {
        
        ///---Подцепляем ивенты и параметры---///
        ///
        eventCharge = FMODUnity.RuntimeManager.CreateInstance(fmodEvent[0]);
        FMOD.Studio.EventDescription chargeEventDescription;
        eventCharge.getDescription(out chargeEventDescription);

        FMOD.Studio.PARAMETER_DESCRIPTION resParameterDescription;
        chargeEventDescription.getParameterDescriptionByName("Resonance", out resParameterDescription);
        resParameterID = resParameterDescription.id;

        FMOD.Studio.PARAMETER_DESCRIPTION panParameterDescription;
        chargeEventDescription.getParameterDescriptionByName("Panning", out panParameterDescription);
        panParameterID = panParameterDescription.id;
        ///
        eventShoot = FMODUnity.RuntimeManager.CreateInstance(fmodEvent[1]);
        eventCancel = FMODUnity.RuntimeManager.CreateInstance(fmodEvent[2]);

        /// Без этой хрени порой выскакивает ошибка
        eventCharge.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        eventShoot.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        eventCancel.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        /// 
        /// Сбрасываем боезапас в начале игры
        ammo = defaultAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        DetectMissiles();
        Control();
        mouseTracking();
        
    }

    public void Control()
    {
        if (Input.GetButton("Charge") && status != shootingStatus.Fire)
        {
            if (status == shootingStatus.Ready)
            {
                //UnityEngine.Debug.Log(status);
                /// Начинаем гудеть

                soundInstance[0] = Instantiate(soundInstancePrefab, transform);
                soundInstance[0].GetComponent<RunAudioGetMarkers>().audioInstance = eventCharge;
                soundInstance[0].GetComponent<RunAudioGetMarkers>().Execute();
                
                status = shootingStatus.StartingCharge;
            }
            if (status == shootingStatus.StartingCharge 
                && soundInstance[0].GetComponent<RunAudioGetMarkers>().timelineInfo.lastMarker == "Hum")
            {
                status = shootingStatus.Charging;
            }

            if (Input.GetButtonDown("Fire") 
                && status == shootingStatus.Charging)
            {
                // Спавним пулю и т.д.
                Fire();
                // Прекращаем гул зарядки
                soundInstance[0].GetComponent<RunAudioGetMarkers>().Destroy(FMOD.Studio.STOP_MODE.IMMEDIATE);
                // Начинаем звук стрельбы
                soundInstance[1] = Instantiate(soundInstancePrefab, transform);
                soundInstance[1].GetComponent<RunAudioGetMarkers>().audioInstance = eventShoot;
                soundInstance[1].GetComponent<RunAudioGetMarkers>().Execute();

                status = shootingStatus.Fire;
                StartCoroutine(AfterFire(cooldown));                             
            }
        }
        if (Input.GetButtonUp("Charge"))
        {            
            if(status == shootingStatus.StartingCharge)
            {
                soundInstance[0].GetComponent<RunAudioGetMarkers>().Destroy(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                StartCoroutine(AfterFire(cooldown));
            }
            if (status == shootingStatus.Charging)
            {
                // Прекращаем звук зарядки
                soundInstance[0].GetComponent<RunAudioGetMarkers>().Destroy(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                // Начинаем звук выключения пушки
                soundInstance[2] = Instantiate(soundInstancePrefab, transform);
                soundInstance[2].GetComponent<RunAudioGetMarkers>().audioInstance = eventCancel;
                soundInstance[2].GetComponent<RunAudioGetMarkers>().Execute();

                StartCoroutine(AfterFire(cooldown));
            }
        }

        if(Input.GetButton("Charge") != true && status == shootingStatus.Waiting)
        {

            status = shootingStatus.Ready;
        }
    }
    IEnumerator AfterFire(float seconds)
    {
        yield return new WaitForSeconds (seconds);
        Destroy(soundInstance[1]);
        status = shootingStatus.Waiting;
    }

    void mouseTracking()
    {
        float lookSpeed = 30f;
        
        float limit = 90f;
        if (Input.GetButtonDown("Charge"))
        {
            // Сбрасываем прицеливание
            lookDirection = 0;       
        }    
        if (Input.GetButton("Charge"))
        {
            Vector3 aimVector = Vector3.forward;
            Vector3 missileVector;
            float angl = 999f;
            float zdist = 0f;
            float xdist = 0f;

            float rotationspeed = 5f;
            float input = 0;
            if (Input.GetAxis("AxisX") > 0)
            {
                input = rotationspeed;

            }
            if (Input.GetAxis("AxisX") < 0)
            {
                input = -rotationspeed;

            }

            lookDirection += Time.deltaTime * lookSpeed * input;
            lookDirection = Mathf.Clamp(lookDirection, -limit, limit);
            //theLaser.transform.Rotate(0, lookDirection - theLaser.transform.rotation.z, 0, Space.World);
            eventCharge.setParameterByID(panParameterID, lookDirection);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //aimVector = new Vector3(999f, 0, 999f * Mathf.Rad2Deg * Mathf.Tan(lookDirection));
            aimVector = Quaternion.Euler(0, lookDirection, 0) * aimVector;

            foreach (GameObject missile in missiles)
            {
                zdist = missile.transform.position.z - gameObject.transform.position.z; // игрок по идее всегда в 0.0.0..... но пусть будет
                xdist = missile.transform.position.x - gameObject.transform.position.x;

                missileVector = new Vector3(xdist, 0, zdist);

                UnityEngine.Debug.DrawRay(gameObject.transform.position, aimVector * 1000f);

                //if (angl >= Mathf.Rad2Deg * Mathf.Atan(xdist / zdist))
                if(angl >= Vector3.Angle(aimVector, missileVector))
                {
                    //angl = Mathf.Rad2Deg * Mathf.Atan(xdist / zdist);
                    angl = Vector3.Angle(aimVector, missileVector);
                }
            }
            resonance = Mathf.Clamp(100 - angl, 0f, 100f);
        }

        eventCharge.setParameterByID(resParameterID, resonance);
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    void DetectMissiles()
    {
        missiles.Clear();
        missiles.AddRange(GameObject.FindGameObjectsWithTag("Missile"));
        //missile = GameObject.FindGameObjectWithTag("Missile");

    }

    void Fire()
    {
        if (ammo > 0)
        {
            /// Отослать пулю
            theLaser = Instantiate(laserPrefab, transform);
            theLaser.transform.rotation = Quaternion.Euler(0, lookDirection, 0);
            theLaser.SetActive(true);

            /// Потратить боеприпасы
            --ammo;

            UnityEngine.Debug.Log("Ammo: " + ammo);
        }       
    }


}
