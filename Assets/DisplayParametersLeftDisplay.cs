using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DisplayParametersLeftDisplay : MonoBehaviour
{
    public GameObject hullObject;
    public GameObject energyObject;
    public GameObject hyperdriveChargeObject;

    private TextMeshPro hull;
    private TextMeshPro energy;
    private TextMeshPro hyperdriveCharge;

    private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        hull = hullObject.GetComponent<TextMeshPro>();
        energy = energyObject.GetComponent<TextMeshPro>();
        hyperdriveCharge = hyperdriveChargeObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        displayEnergy();
        displayHull();
        displayHyperdriveCharge();
    }

    void displayHull()
    {
        string hullStatus = "UNKNOWN";
        if(player.GetComponent<PlayerStats>().hull == 100)
        {
            hullStatus = "STABLE";
        }
        if (player.GetComponent<PlayerStats>().hull < 100)
        {
            hullStatus = "STRAINED";
        }
        if (player.GetComponent<PlayerStats>().hull <= 75)
        {
            hullStatus = "DAMAGED";
        }
        if (player.GetComponent<PlayerStats>().hull <= 50)
        {
            hullStatus = "BREACHED";
        }
        if (player.GetComponent<PlayerStats>().hull <= 25)
        {
            hullStatus = "CRITICAL";
        }


        hull.text = "HULL: " + hullStatus;
    }

    void displayEnergy()
    {
        int energyPercent;
        energyPercent = Mathf.RoundToInt(player.GetComponent<PlayerStats>().energy / player.GetComponent<PlayerStats>().maxEnergy * 100f);

        energy.text = "ENERGY: " + energyPercent.ToString() + "%";
    }


    void displayHyperdriveCharge()
    {
        hyperdriveCharge.text = "HYPERDRIVE CHARGE: " + player.GetComponent<PlayerStats>().hyperdriveCharge.ToString() + "%";
    }
}
