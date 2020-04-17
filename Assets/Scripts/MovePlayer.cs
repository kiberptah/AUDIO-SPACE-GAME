using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float movement;
        

        if (Input.GetButton("MovementX"))
        {
            movement = speed * Time.deltaTime * Input.GetAxis("MovementX");
            transform.Translate(movement, 0, 0); 

        }
    }
}
