﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Ball ball;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - ball.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
        //kamera zastaje kada dođe do čunjića
        if (ball.transform.position.z <= 1750f)
        {
            transform.position = ball.transform.position + offset;
        }

        
	}
}