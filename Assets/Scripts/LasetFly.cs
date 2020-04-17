using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasetFly : MonoBehaviour
{
    public float laserSpeed = 1f;
    void Start()
    {
        StartCoroutine(Fly());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fly()
    {
        while (Mathf.Abs(transform.position.z) < 100f && Mathf.Abs(transform.position.x) < 100f && Mathf.Abs(transform.position.y) < 100f)
        {
            transform.Translate(new Vector3(0, 0, laserSpeed));
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
