using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float energy;
    public float maxEnergy = 100f;

    public float hull;
    public float maxHull = 100f;

    public GameObject hyperdriveCharger;
    public float hyperdriveCharge;

    GameObject gameController;

    Vector3 defaultPosition = new Vector3(0, 0, 0);

    // Звуки
    [FMODUnity.EventRef]
    public string eventHitPath;
    private FMOD.Studio.EventInstance eventHit;


    void Start()
    {
        eventHit = FMODUnity.RuntimeManager.CreateInstance(eventHitPath);

        gameController = GameObject.FindGameObjectWithTag("GameController");

        hull = maxHull;
        energy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkHull();
        updateHyperdriveCharge();
    }

    public void ResetPlayerState()
    {
        gameObject.GetComponent<PlayerStats>().energy = 0;
        gameObject.GetComponent<PlayerStats>().hull = gameObject.GetComponent<PlayerStats>().maxHull;

        gameObject.transform.position = defaultPosition;
    }

    void checkHull()
    {
        if (hull <= 0)
        {
            gameController.GetComponent<GameStatesManager>().Lose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("COLLISION");
        if (other.gameObject.tag == "Missile")
        {
            eventHit.start();
        }
    }

    void updateHyperdriveCharge()
    {
        hyperdriveCharge = hyperdriveCharger.GetComponent<HyperDriveCharge>().charge;
    }
}
