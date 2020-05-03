using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBubbleMovement : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPathCollectAmmo;
    [FMODUnity.EventRef]
    public string eventPathEnergyNoise;

    private FMOD.Studio.EventInstance collectAmmo;
    private FMOD.Studio.EventInstance energyNoise;


    public float energy = 30f;
    public float speed = 10f;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        collectAmmo = FMODUnity.RuntimeManager.CreateInstance(eventPathCollectAmmo);
        energyNoise = FMODUnity.RuntimeManager.CreateInstance(eventPathEnergyNoise);

        energyNoise.start();
        StartCoroutine(setParameters());
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

    private void OnDestroy()
    {
        StopAllCoroutines();
        energyNoise.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    float CorrectPanning(GameObject other)
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

    float Distance()
    {
        float distance;
        distance = (transform.position - Player.transform.position).magnitude;
        distance = 50 - Mathf.Clamp(distance, 0, 50f);
        //Debug.Log(distance);
        return distance;
    }

    IEnumerator setParameters()
    {
        while (true)
        {
            energyNoise.setParameterByName("Panning", CorrectPanning(gameObject));
            energyNoise.setParameterByName("Proximity", Distance());
            yield return null;
        }
    }
}
