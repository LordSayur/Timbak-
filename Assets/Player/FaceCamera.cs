using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public GameObject m_camera;

	void Start ()
    {
        if (m_camera != null)
        {
            m_camera = GameObject.Find("PlayerCamera");
        }
	}
	
	void Update ()
    {
        transform.LookAt(transform.position + m_camera.transform.rotation * Vector3.forward, m_camera.transform.rotation * Vector3.up);
	}
}
