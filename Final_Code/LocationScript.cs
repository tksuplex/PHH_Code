using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
    public int thisLocation;
    public bool LightOn;
    public GameObject lights;
    public LocationControllerScript loc;

    private void Start()
    {
        loc = GameObject.Find("LocationController").GetComponent<LocationControllerScript>();
    }

    private void FixedUpdate()
    {
        if (loc.DarkRoom[thisLocation])
        {
            lights.SetActive(false);
            LightOn = false;
        }
        else
        {
            lights.SetActive(true);
            LightOn = true;
        }
    }
}
