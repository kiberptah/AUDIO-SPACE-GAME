using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public GameObject Scanwave;
    public float scanTime_in_seconds;

    public enum scanMode
    {
        Missiles,
        Ammo
    }
    public scanMode currentScanMode =  scanMode.Missiles;

    //bool approveScan = true;
    void OnEnable()
    {
        StartCoroutine(ScanModeControl());
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
            if (Input.GetButtonDown("ScanMissiles") || Input.GetButtonDown("ScanAmmo"))
            {
                if (GameObject.FindGameObjectWithTag("Scanwave") != null)
                {
                    Destroy(GameObject.FindGameObjectWithTag("Scanwave"));
                }
                //Debug.Log("scan");

                GameObject scanWave;
                scanWave = Instantiate(Scanwave, transform);
                scanWave.SetActive(true);
                //approveScan = true;

                yield return new WaitForSeconds(scanTime_in_seconds);
            }
        }       
    }

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
}
