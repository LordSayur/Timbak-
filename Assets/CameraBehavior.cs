using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public float rotationSpeed = 5f;

	// Update is called once per frame
	void Update () {
        //transform.RotateAround(transform.position, rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
	}
}
