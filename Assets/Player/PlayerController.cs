using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerSetup))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerController : NetworkBehaviour
{

    PlayerHealth m_playerHealth;
    PlayerMotor m_playerMotor;
    PlayerSetup m_playerSetup;
    PlayerShoot m_playerShoot;

    private GameObject m_cameraPivot;

    public override void OnStartLocalPlayer()
    {
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = false;
        GetComponentInChildren<Camera>().enabled = true;
        m_cameraPivot = GameObject.Find("PlayerCameraPivot");
    }

    void Start ()
    {
        m_playerHealth = GetComponent<PlayerHealth>();
        m_playerMotor = GetComponent<PlayerMotor>();
        m_playerSetup = GetComponent<PlayerSetup>();
        m_playerShoot = GetComponent<PlayerShoot>();
	}
	
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_playerShoot.Fire();
        m_playerMotor.RotateCamera();
	}

    void FixedUpdate ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_playerMotor.MovePlayer(GetInput());
        m_playerMotor.RotatePlayer(GetInput());
    }

    Vector3 GetInput ()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        return new Vector3( v, h, 0);
    }
}
