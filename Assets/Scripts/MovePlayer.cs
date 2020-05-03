using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 10f;
    public float cameraOffset = 1f;
    public float cameraReturnSpeed;

    Vector3 cameraOrigPos;
    GameObject mainCamera;

    Coroutine shiftCam;
    Coroutine resetCam;

    // Звуки
    [FMODUnity.EventRef]
    public string eventMovementPath;

    private FMOD.Studio.EventInstance eventMovement;

    //bool isEngineHums = false;
    // Start is called before the first frame update
    void Start()
    {
         mainCamera = GameObject.Find("CameraHolder");
        cameraOrigPos = mainCamera.transform.position;

        // Звуки
        eventMovement = FMODUnity.RuntimeManager.CreateInstance(eventMovementPath);
        FMOD.Studio.EventDescription moveEventDescription;
        eventMovement.getDescription(out moveEventDescription);
        moveEventDescription.loadSampleData();


    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    void Movement()
    {
        float movement;
        
        if (Input.GetButtonDown("MovementX"))
        {

            eventMovement.start();
 
            //Debug.Log(Input.GetAxis("MovementX"));

            if (resetCam != null)
            {
                StopCoroutine(resetCam);
            }
            
            shiftCam = StartCoroutine(shiftCamera());
        }
        if (Input.GetButton("MovementX"))
        {
            movement = speed * Time.deltaTime * Input.GetAxis("MovementX");
            transform.Translate(movement, 0, 0);
            
        }
        if (Input.GetButtonUp("MovementX"))
        {
            if (Input.GetAxis("MovementX") == 0)
            {
                eventMovement.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            }
            if (shiftCam != null)
            {
                StopCoroutine(shiftCam);
            }
            
            resetCam = StartCoroutine(returnCamera());
        }
    }

    IEnumerator shiftCamera()
    {

        Vector3 finalPos = mainCamera.transform.position;
        finalPos.x += -Mathf.Sign(Input.GetAxis("MovementX")) * cameraOffset;

        while (Vector3.Distance(mainCamera.transform.position, finalPos) > 0.01f)
        {

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, finalPos, 1f * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    IEnumerator returnCamera()
    {
        while (Vector3.Distance(mainCamera.transform.position, cameraOrigPos) > 0.001f)
        {


            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraOrigPos, 1f * Time.deltaTime);
            mainCamera.transform.Translate((cameraOrigPos - mainCamera.transform.position) * Time.deltaTime * cameraReturnSpeed);
            //Debug.Log(mainCamera.transform.position);
            yield return null;
        }
        if (Vector3.Distance(mainCamera.transform.position, cameraOrigPos) <= 0.05f)
        {
            mainCamera.transform.position = cameraOrigPos;
        }
        yield return null;
    }
}
