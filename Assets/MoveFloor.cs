using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    float movement;

    GameObject FloorSpawner;
    Vector3 returnposition;
    void Start()
    {
        FloorSpawner = GameObject.FindGameObjectWithTag("FloorSpawner");

        
    }

    // Update is called once per frame
    void Update()
    {
        movement = speed * Time.deltaTime;
        transform.Translate(0, 0, -movement);

        if(transform.position.z < - 5f)
        {
            returnposition = new Vector3(transform.position.x, transform.position.y, FloorSpawner.GetComponent<Floor>().maxZ - 5f);

            transform.position = returnposition;
        }
    }
}
