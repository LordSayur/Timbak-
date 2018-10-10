using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipBehavior : NetworkBehaviour {

    private float move;
    private float rotate;
    public float turnSpeed = 2f;
    public float moveSpeed = 35f;
    private Rigidbody rb;
    private GameObject cameraPivot;
    public float cameraTurnSpeed = 5f;
    public GameObject smokePref;
    public GameObject cannonPref;
    public GameObject[] cannonSpawnPoints;
    public float cannonPower = 5f;
    private float cannonReloadingTime = 3f;
    private float cannonCoolDownTime;
    private Renderer rend;
    private Vector3 colors;

    public override void OnStartLocalPlayer()
    {
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = false;
        GetComponentInChildren<Camera>().enabled = true;
        cameraPivot = GameObject.Find("PlayerCameraPivot");
        rend = GameObject.Find("Sails").GetComponent<Renderer>();
        colors = new Vector3(Random.value, Random.value, Random.value);
        rend.material.color = new Color(colors.x, colors.y, colors.z);
    }

    void Start () {
        rb = GetComponent<Rigidbody>();
        cannonCoolDownTime = Time.time;
        rend = GameObject.Find("Sails").GetComponent<Renderer>();
        rend.material.color = new Color(colors.x, colors.y, colors.z);
        if (!isLocalPlayer) return;
    }
	
	void Update ()
    {
        if (!isLocalPlayer) return;
        Movement();
        RotateCamera();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(1) && cannonCoolDownTime < Time.time)
        {
            CmdFireRight();
        }

        if (Input.GetMouseButtonDown(0)&& cannonCoolDownTime < Time.time)
        {
            CmdFireLeft();
        }
    }

    [Command]
    private void CmdFireLeft()
    {
        for (int i = 5; i < 10; i++)
        {
            StartCoroutine(InstantiateCannon(i, Random.Range(0.1f, 0.25f)));
        }

        cannonCoolDownTime = cannonReloadingTime + Time.time;
    }

    [Command]
    private void CmdFireRight()
    {
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(InstantiateCannon(i, Random.Range(0.1f, 0.5f)));
        }

        cannonCoolDownTime = cannonReloadingTime + Time.time;
    }

    private void RotateCamera()
    {
        if (Input.GetKey(KeyCode.E))
        {
            cameraPivot.transform.Rotate(Vector3.up * cameraTurnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            cameraPivot.transform.Rotate(Vector3.up * -cameraTurnSpeed * Time.deltaTime);
        }
    }

    private void Movement()
    {
        move = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");

        rb.AddTorque(0, rotate * turnSpeed * Time.deltaTime, 0f);
        rb.AddForce(transform.right * move * moveSpeed * Time.deltaTime);
    }

    IEnumerator InstantiateCannon(int num, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GameObject cannon = Instantiate(cannonPref, cannonSpawnPoints[num].transform.position, cannonSpawnPoints[num].transform.rotation);
        cannon.GetComponent<Rigidbody>().velocity = cannon.transform.forward * cannonPower;
        GameObject smoke = Instantiate(smokePref, cannonSpawnPoints[num].transform.position, cannonSpawnPoints[num].transform.rotation);
        NetworkServer.Spawn(cannon);
        NetworkServer.Spawn(smoke);
        Destroy(cannon, 2f);
        Destroy(smoke, 3f);
    }

}
