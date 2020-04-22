using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperDriveCharge : MonoBehaviour
{
    public float minutesToCharge;
    private float tickDelay;
    public float charge;

    private void OnEnable()
    {
        tickDelay = minutesToCharge * 60f / 100f;

        charge = 0;
        StopAllCoroutines();
        StartCoroutine(chargeHyperdrive());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator chargeHyperdrive()
    {
        while(charge < 100)
        {
            charge++;
            yield return new WaitForSeconds(tickDelay);
        }

        yield return null;
    }
}
