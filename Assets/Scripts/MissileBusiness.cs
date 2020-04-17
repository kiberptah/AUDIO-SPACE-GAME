using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMOD;
using System;

public class MissileBusiness : MonoBehaviour
{
    GameObject gameController;
    GameObject Player;

    /*
    [FMODUnity.EventRef]
    public string[] fmodEvent = new string[1];
    private FMOD.Studio.EventInstance eventAlarm;

    private FMOD.Studio.PARAMETER_ID panParameterID;
    private float panning;

    public GameObject soundInstancePrefab;
    private GameObject[] soundInstance = new GameObject[3];
    */

    /// Для движения к игроку
    private Transform target;
    public float missileSpeed = 1f;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        ///
        Player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.Find("GameController");


        target = Player.transform;
        
        /*
        /// Звук
        eventAlarm = FMODUnity.RuntimeManager.CreateInstance(fmodEvent[0]);
        eventAlarm.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        FMOD.Studio.EventDescription alarmEventDescription;
        eventAlarm.getDescription(out alarmEventDescription);

        
        FMOD.Studio.PARAMETER_DESCRIPTION panParameterDescription;
        alarmEventDescription.getParameterDescriptionByName("AlarmPanning", out panParameterDescription);
        panParameterID = panParameterDescription.id;
        */
        ///

        //AlarmOn();

        StartCoroutine(Follow());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy()
    {

    }

    /*
    void AlarmOn()
    {
        soundInstance[0] = Instantiate(soundInstancePrefab, transform);
        soundInstance[0].GetComponent<RunAudioGetMarkers>().audioInstance = eventAlarm;
        soundInstance[0].GetComponent<RunAudioGetMarkers>().Execute();

        //soundInstance[0].GetComponent<RunAudioGetMarkers>().Destroy(FMOD.Studio.STOP_MODE.ALLOWFADEOUT, 5);
    }
    */

    /*
     void CorrectPanning()
     {


         float zdist = gameObject.transform.position.z - Player.transform.position.z;
         float xdist = gameObject.transform.position.x - Player.transform.position.x;
         float angl = Mathf.Rad2Deg * Mathf.Atan(xdist / zdist);

         panning = angl;

         eventAlarm.setParameterByID(panParameterID, panning);
         /*
         UnityEngine.Debug.Log("z = " + zdist);
         UnityEngine.Debug.Log("x = " + xdist);
         UnityEngine.Debug.Log("angl = " + angl);

     } */

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other);
    }

    void CheckCollision(Collider other)
    {
        if (other.gameObject.tag == "Scanwave")
        {
            //AlarmOn();
        }
        if (other.gameObject.tag == "Player")
        {
            gameController.GetComponent<GameStatesManager>().Lose();

            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);

            gameController.GetComponent<GameStatesManager>().IncreaseScore(1);
        }
    }

    IEnumerator Follow()
    {
        //UnityEngine.Debug.Log("missile");
        while (Vector3.Distance(transform.position, target.position) > 0.05f)
        {
 
            transform.LookAt(Player.transform);
            transform.Translate(0, 0, missileSpeed * Time.deltaTime);
            /*
            //transform.position = Vector3.MoveTowards(transform.position, target.position, smoothing * Time.deltaTime);
            moveDirection = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, target.position.z - transform.position.z);
            moveDirection = moveDirection.normalized;
            transform.Translate(moveDirection * missileSpeed * Time.deltaTime);
            */

            yield return null;
        }
    }

}
