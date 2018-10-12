using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour 
{
    private bool m_isReloading = false;
    public float m_cannonReloadingTime = 3f;
    public float m_cannonPower = 5f;
    public GameObject[] m_cannonSpawnPoints;

    public GameObject m_cannonPref;
    public GameObject m_smokePref;

    public void Fire()
    {
        if (Input.GetMouseButtonDown(1) && !m_isReloading)
        {
            CmdFireRight();
            StartCoroutine("Reloading");
        }

        if (Input.GetMouseButtonDown(0) && !m_isReloading)
        {
            CmdFireLeft();
            StartCoroutine("Reloading");
        }
    }

    [Command]
    public void CmdFireLeft()
    {
        for (int i = 5; i < 10; i++)
        {
            StartCoroutine(InstantiateCannon(i, Random.Range(0.1f, 0.25f)));
        }
    }

    [Command]
    public void CmdFireRight()
    {
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(InstantiateCannon(i, Random.Range(0.1f, 0.5f)));
        }
    }

    IEnumerator InstantiateCannon(int num, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GameObject cannon = Instantiate(m_cannonPref, m_cannonSpawnPoints[num].transform.position, m_cannonSpawnPoints[num].transform.rotation);
        cannon.GetComponent<Rigidbody>().velocity = cannon.transform.forward * m_cannonPower;
        GameObject smoke = Instantiate(m_smokePref, m_cannonSpawnPoints[num].transform.position, m_cannonSpawnPoints[num].transform.rotation);
        NetworkServer.Spawn(cannon);
        NetworkServer.Spawn(smoke);
        Destroy(cannon, 2f);
        Destroy(smoke, 3f);
    }

    IEnumerator Reloading ()
    {
        m_isReloading = true;
        yield return new WaitForSeconds(m_cannonReloadingTime);
        m_isReloading = false;
    }
}
