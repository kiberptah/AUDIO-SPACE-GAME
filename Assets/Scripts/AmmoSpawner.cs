using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    GameObject Player;

    public GameObject ammoPrefab;

    public float spawnOffset = 20f;

    public float spawnDistance = 40f;

    public float spawnDelay = 5f;

    // Start is called before the first frame update
    void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnAmmo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnAmmo()
    {
        while (true)
        {
            float XCoord = Random.Range(Player.transform.position.x + spawnOffset * 0.5f, Player.transform.position.x + spawnOffset);
            if(System.Convert.ToBoolean(Random.value > 0.5f))
            {
                XCoord = -XCoord; 
            }
            Instantiate(ammoPrefab, new Vector3(XCoord, 0, spawnDistance), Quaternion.identity, null);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
