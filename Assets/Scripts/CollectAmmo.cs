using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAmmo : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            gameObject.GetComponent<PlayerStats>().energy = 
                Mathf.Clamp(gameObject.GetComponent<PlayerStats>().energy + other.GetComponent<AmmoBubbleMovement>().energy, 
                0, 
                gameObject.GetComponent<PlayerStats>().maxEnergy);


            //Destroy(other);
            Debug.Log("+Energy: " + gameObject.GetComponent<PlayerStats>().energy);

        }
    }
}
