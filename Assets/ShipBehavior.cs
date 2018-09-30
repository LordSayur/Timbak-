using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipBehavior : NetworkBehaviour {

    private float move;
    private float rotate;
    public float turnSpeed;
    public float moveSpeed;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer) return;
        Movement();
    }

    private void Movement()
    {
        move = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");

        rb.AddTorque(0, rotate * turnSpeed * Time.deltaTime, 0f);
        rb.AddForce(transform.right * move * moveSpeed * Time.deltaTime);
    }
}
