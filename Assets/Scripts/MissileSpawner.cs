using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missilePrefab;
    GameObject Player;

    public float spawnRadius = 40f;
    public float spawnDelay = 2f;



    private int spawnZone;

    private float minZCoord = 20f;
    private float maxZCoord = 40f;

    private float minXCoord = -20f;
    private float maxXCoord = 20f;

    private float ZCoord;
    private float XCoord;

    // Start is called before the first frame update
    void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnMissile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnMissile()
    {
        while (true)
        {
            Vector2 coordinates;
            coordinates = Random.insideUnitCircle.normalized * spawnRadius;
            coordinates.y = Mathf.Abs(coordinates.y); // чтобы спавнить только в передней полусфере

            Instantiate(missilePrefab, Player.transform.position + new Vector3(coordinates.x, 0, coordinates.y), Quaternion.identity, null);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SpawnMissileOld()
    {
        while (true)
        {
            spawnZone = Random.Range(1, 3);
            switch (spawnZone)
            {
                case 1:
                    {
                        minXCoord = -40f;
                        maxXCoord = -40f;

                        minZCoord = 0;
                        maxZCoord = 40f;
                    }
                    break;
                case 2:
                    {
                        minXCoord = 40f;
                        maxXCoord = 40f;

                        minZCoord = 0;
                        maxZCoord = 40f;
                    }
                    break;
                case 3:
                    {
                        minXCoord = -20f;
                        maxXCoord = 20f;

                        minZCoord = 20;
                        maxZCoord = 40f;
                    }
                    break;
                default:
                    break;
            }


            float ZCoord = Random.Range(minZCoord, maxZCoord);
            float XCoord = Random.Range(minXCoord, maxXCoord);

            Instantiate(missilePrefab, new Vector3(XCoord, 0, ZCoord), Quaternion.identity, null); 

            yield return new WaitForSeconds(spawnDelay);

        }

    }
}
