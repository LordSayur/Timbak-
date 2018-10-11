using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Transform m_camera;

	void Start ()
    {
        m_camera = gameObject.transform.parent.Find("PlayerCameraPivot").gameObject.transform.Find("PlayerCamera");
	}
	
	void Update ()
    {
        transform.LookAt(transform.position + m_camera.transform.rotation * Vector3.forward, m_camera.transform.rotation * Vector3.up);
	}
}
