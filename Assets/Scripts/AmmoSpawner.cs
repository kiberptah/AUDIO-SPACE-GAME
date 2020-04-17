using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoPrefab;

    private float minXCoord = -20f;
    private float maxXCoord = 20f;

    float ZCoord = 40f;

    public float spawnDelay = 5f;

    // Start is called before the first frame update
    void OnEnable()
    {
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
            float XCoord = Random.Range(minXCoord, maxXCoord);

            Instantiate(ammoPrefab, new Vector3(XCoord, 0, ZCoord), Quaternion.identity, null);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
