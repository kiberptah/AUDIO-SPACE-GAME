using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBubbleMovement : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPathCollectAmmo;
    private FMOD.Studio.EventInstance collectAmmo;



    public float energy = 30f;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        collectAmmo = FMODUnity.RuntimeManager.CreateInstance(eventPathCollectAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);

        if(transform.position.z < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collectAmmo.start();
            Destroy(gameObject);
        }
    }

}
