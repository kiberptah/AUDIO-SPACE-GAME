using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingIn : MonoBehaviour
{   

    void Start()
    {
        StartCoroutine(FadeIn());   
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator FadeIn()
    {
        Color iColor = gameObject.GetComponent<Image>().color;
        while (iColor.a > 0)
        {
            iColor.a -= 0.01f;

            gameObject.GetComponent<Image>().color = iColor;

            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

}
