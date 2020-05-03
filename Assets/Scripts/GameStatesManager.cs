using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using TMPro;

public class GameStatesManager : MonoBehaviour
{
    public string OutroScene = "Outro";

    public GameObject gameSystems;
    public GameObject startButton;
    public GameObject ScorePanel;
    public GameObject missileSpawner;
    public GameObject hyperdriveCharger;
    public GameObject textHint;

    public GameObject Player;

    public int gameScore;

    FMOD.Studio.Bus MasterBus;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;


        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");

        StartCoroutine(checkDriveCharge());
        StartCoroutine(checkEnergy());

        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator checkEnergy()
    {
        while (Player.GetComponent<PlayerStats>().energy < 100f)
        {
            yield return null;
        }
        gameObject.GetComponent<SoundManager>().AI_ChargeSpeech();
        StartCoroutine(enableWithDelay(missileSpawner, 5f));
        hyperdriveCharger.SetActive(true);

        StartCoroutine(TypeHint());
    }

    IEnumerator TypeHint()
    {
        string sentence;
        sentence = "HOLD RMB TO AIM. RESONANCE GETS LOUDER IF YOU AIM CORRECTLY";
        textHint.GetComponent<TextMeshPro>().text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            textHint.GetComponent<TextMeshPro>().text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    IEnumerator checkDriveCharge()
    {
        bool check50per = false;
        while(Player.GetComponent<PlayerStats>().hyperdriveCharge < 100f)
        {
            if (Player.GetComponent<PlayerStats>().hyperdriveCharge > 50f && check50per == false)
            {
                //gameObject.GetComponent<SoundManager>().AI_HyperDriveChargeHalf();
                check50per = true;
            }

            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene(OutroScene);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameObject.GetComponent<SoundManager>().AI_FinalSpeech();
    }

    public void BeginGame()
    {
        //MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        //gameObject.GetComponent<SoundManager>().eventMusic_1.start();
        Player.transform.position = new Vector3(0,0,0);


        gameSystems.SetActive(true);
        startButton.SetActive(false);

        ScorePanel.SetActive(false);
        gameScore = 0;

        //Player.GetComponent<ShootLaser>().ammo = Player.GetComponent<ShootLaser>().defaultAmmo;
        Player.GetComponent<PlayerStats>().ResetPlayerState();

        gameObject.GetComponent<SoundManager>().AI_Speech_1();
    }

    public void Lose()
    {
        gameObject.GetComponent<SoundManager>().eventPlayerDeath.start();
        //gameObject.GetComponent<SoundManager>().eventMusic_1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Cursor.visible = true;
        SceneManager.LoadScene("Fail");
        //DestroyObjects();

        /*
        gameSystems.SetActive(false);
        startButton.SetActive(true);

        ScorePanel.SetActive(true);
        ScorePanel.GetComponent<Text>().text = "Last Score: " + gameScore;
        */

    }

    void DestroyObjects()
    {
        List<GameObject> missileInstances = new List<GameObject>();
        missileInstances.AddRange(GameObject.FindGameObjectsWithTag("Missile"));
        foreach (GameObject missile in missileInstances)
        {
            Destroy(missile);
        }

        GameObject scanWave;
        scanWave = GameObject.FindGameObjectWithTag("Scanwave");
        Destroy(scanWave);

    }

    public void IncreaseScore(int points)
    {
        gameScore += points;
    }

    IEnumerator enableWithDelay(GameObject someObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        someObject.SetActive(true);
    }
}
