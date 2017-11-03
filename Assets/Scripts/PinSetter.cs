﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    private Ball ball;
    public Text standingDisplay;
    private bool ballEnteredBox = false;


    public int lastStandingCount = -1;
    //kada se je zadnje Text tj broj stojecih pinova promijenio
    private float lastChangeTime;

    // Use this for initialization
    void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString(); //update na UI text koji sadrzi broj stojecih pinova


        //if ball entered box
        if (ballEnteredBox)
        {
            CheckStanding();
        }
	}

    void CheckStanding()
    {
        //update lastStandingCount
        //call PinsHaveSettled() when they have
        int currentStanding = CountStanding();

        if(currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f; // how to long to wait to consider pins settled
        if ((Time.time - lastChangeTime)>settleTime) // if last change > 3s ago
        {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled()
    {
        lastStandingCount = -1;
        ballEnteredBox = false;
        standingDisplay.color = Color.green;

        //dok se pinovi smire resetiraj loptu
        ball.ResetirajLoptu();


    }


    int CountStanding()
    {
        int standing = 0;
        //Prolazimo kroz sve pinove i provjeravamo dali stoje 
        foreach(Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standing++;
            }
        }

        return standing;
    }

   

    void OnTriggerEnter(Collider collider)
    {
        GameObject thing = collider.gameObject;
        //lopta ulazi u play box
        if (thing.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        GameObject thingLeft = collider.gameObject;

        if (thingLeft.GetComponent<Pin>())
        {
            Destroy(thingLeft);
        }
    }
}