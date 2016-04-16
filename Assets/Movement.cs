﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour {
    //Parameters controlled by air input 
    public float moveAccel=20f;
    public Vector3 windDirection = new Vector3(0,0,0);
    public float windSpeed = 0f;
    private float forwardSpeed = 3f;
    public float levelWidth, levelHeight;

    public Text scoreText;
    public float score=0f;

    private Vector3 airInput;
    private Vector3 direction;
    private float xRotateMax=45f;
    private float xRotateMin=315f;
    private float yRotateMax=45f;
    private float yRotateMin=315f;
 

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 5, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        scoreText.text = "Score: " + score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        airInput = new Vector3( Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0);
        //transform.Translate(airInput * moveAccel/100);
        if (!((transform.eulerAngles.x >= xRotateMax && airInput.x > 0 && transform.eulerAngles.x<180) || (transform.eulerAngles.x <= xRotateMin && airInput.x < 0 && transform.eulerAngles.x > 180)))
        {
            transform.Rotate((airInput.x * moveAccel / 10), 0, 0);
        }

        if (!((transform.eulerAngles.y >= yRotateMax && airInput.y > 0 && transform.eulerAngles.y < 180) || (transform.eulerAngles.y <= yRotateMin && airInput.y < 0 && transform.eulerAngles.y > 180)))
        {
            transform.Rotate(0,(airInput.y * moveAccel / 10), 0);
        }
        //transform.Rotate((airInput * moveAccel / 10));
        //transform.eulerAngles=new Vector3(Mathf.Min(transform.eulerAngles.x, xRotateMax), Mathf.Min(transform.eulerAngles.y, yRotateMax),0);
        // transform.eulerAngles = new Vector3(Mathf.Max(transform.eulerAngles.x, xRotateMin), Mathf.Max(transform.eulerAngles.y, yRotateMin),0);

        direction = new Vector3(Mathf.Cos( transform.eulerAngles.x), Mathf.Cos(transform.eulerAngles.y), Mathf.Cos(transform.eulerAngles.z));
        transform.position = transform.position + Camera.main.transform.forward * forwardSpeed * Time.deltaTime;
        //transform.Translate(forwardSpeed / 10)
        //transform.Translate(direction.normalized * forwardSpeed/10);


    }
    /*
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Item_Ring")
        {
            Debug.Log("Collide");
        }
    }
    */
}
/*

//
*/