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

    public override void OnStartClient()
    {
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = false;
        GetComponentInChildren<Camera>().enabled = true;
        cameraPivot = GameObject.Find("PlayerCameraPivot");
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        cannonCoolDownTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer) return;
        Movement();
        RotateCamera();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0) && cannonCoolDownTime < Time.time)
        {
            for (int i = 0; i < 5; i++)
            {
                StartCoroutine(InstantiateCannon(i, Random.Range(0.1f, 0.5f)));
            }

            cannonCoolDownTime = cannonReloadingTime + Time.time;
        }

        if (Input.GetMouseButtonDown(1)&& cannonCoolDownTime < Time.time)
        {
            for (int i = 5; i < 10; i++)
            {
                StartCoroutine(InstantiateCannon(i, Random.Range(0.1f, 0.25f)));
            }

            cannonCoolDownTime = cannonReloadingTime + Time.time;
        }
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
        Destroy(cannon, 2f);
        Destroy(smoke, 3f);
    }
}
