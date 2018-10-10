using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : NetworkBehaviour
{

    Rigidbody m_rigidbody;

    public float turnSpeed = 2f;
    public float moveSpeed = 35f;

    public float m_cameraTurnSpeed = 1f;

    private GameObject m_cameraPivot;

    void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_cameraPivot = GameObject.Find("PlayerCameraPivot");
    }

    public void MovePlayer(Vector3 input)
    {
        m_rigidbody.AddTorque ( 0, input.y * turnSpeed * Time.fixedDeltaTime, 0f);
    }

    public void RotatePlayer(Vector3 input)
    {
        m_rigidbody.AddForce(transform.right * input.x * moveSpeed * Time.fixedDeltaTime);
    }

    public void RotateCamera()
    {
        if (Input.GetKey(KeyCode.E))
        {
            m_cameraPivot.transform.Rotate(Vector3.up * m_cameraTurnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            m_cameraPivot.transform.Rotate(Vector3.up * -m_cameraTurnSpeed * Time.deltaTime);
        }
    }
}
