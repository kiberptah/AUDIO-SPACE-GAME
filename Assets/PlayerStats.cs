using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float energy;
    public float maxEnergy = 100f;

    public float hull;
    public float maxHull = 100f;

    Vector3 defaultPosition = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        hull = maxHull;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlayerState()
    {
        gameObject.GetComponent<PlayerStats>().energy = gameObject.GetComponent<PlayerStats>().maxEnergy;
        gameObject.GetComponent<PlayerStats>().hull = gameObject.GetComponent<PlayerStats>().maxHull;

        gameObject.transform.position = defaultPosition;
    }
}
