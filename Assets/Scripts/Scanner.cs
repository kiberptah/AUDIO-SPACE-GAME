using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private GameObject Player;
    public GameObject Scanwave;
    public float scanTime_in_seconds;

    

    public float energyForScan = 5f;

    //bool approveScan = true;
    void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(ScanModeControl());
        StartCoroutine(ScanTimer());
        
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {

        /*
        if (approveScan == true) // НАЗУЙ ЭТО НАДО??
        {
            approveScan = false;
            
        }
        */
    }
    IEnumerator ScanTimer()
    {
        while(true)
        {
            if (Input.GetButtonDown("ScanMissiles"))
            {
                Scanwave.GetComponent<Scanwave>().currentScanMode = global::Scanwave.scanMode.Missiles;
            }
            if (Input.GetButtonDown("ScanAmmo"))
            {
                Scanwave.GetComponent<Scanwave>().currentScanMode = global::Scanwave.scanMode.Ammo;
            }
            if ((Input.GetButtonDown("ScanMissiles") || Input.GetButtonDown("ScanAmmo"))
                && Player.GetComponent<PlayerStats>().energy >= energyForScan)
            {
                //Тратим энергию
                Player.GetComponent<PlayerStats>().energy -= energyForScan;
                // убираем предыдущий скан
                if (GameObject.FindGameObjectWithTag("Scanwave") != null)
                {
                    Destroy(GameObject.FindGameObjectWithTag("Scanwave"));
                }
                //делаем новый
                GameObject scanWave;
                scanWave = Instantiate(Scanwave, transform);
                scanWave.SetActive(true);

                yield return new WaitForSeconds(scanTime_in_seconds);
            }
            yield return null;
        }       
    }
    /*
    IEnumerator ScanModeControl()
    {
        while(true)
        {
            if (Input.GetButtonDown("ScanMissiles"))
            {
                currentScanMode = scanMode.Missiles;
                UnityEngine.Debug.Log(currentScanMode);

                yield return new WaitForSeconds(1f);
            }
            if (Input.GetButtonDown("ScanAmmo"))
            {
                currentScanMode = scanMode.Ammo;
                UnityEngine.Debug.Log(currentScanMode);

                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }     
    }
    */
}
