using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAmmo : MonoBehaviour
{
    public int maxAmmo = 10;
    public int ammoPickUp = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo" && gameObject.GetComponent<ShootLaser>().ammo < maxAmmo)
        {
            

            gameObject.GetComponent<ShootLaser>().ammo += ammoPickUp;
            Destroy(other);
            Debug.Log("+Ammo: " + gameObject.GetComponent<ShootLaser>().ammo);
        }
    }
}
