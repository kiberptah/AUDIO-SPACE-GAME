using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public int minX;
    public int maxX;
    public int maxZ;

    public GameObject floorTilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        spawnFloor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnFloor()
    {
        for (int i = 0; i < maxZ; ++i)
        {          
            for(int j = minX; j <= maxX; ++j)
            {
                Instantiate(floorTilePrefab, new Vector3(j + 2.5f, -1f, i), Quaternion.identity, transform);
                j += 4;
            }
            i += 4;
        }
    }
}
