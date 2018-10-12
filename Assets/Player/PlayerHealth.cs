using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour 
{
    [SyncVar(hook = "UpdateHealthBar")]
    private float m_currentHealth;
    public float m_maxHealth = 15f;

    public RectTransform m_healthBar;
    [SyncVar]
    public bool m_isDead = false;

	void Start () 
    {
        Reset();
        //Debug.Log(m_currentHealth);
        //StartCoroutine("CountDown");

    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(m_currentHealth);
        Damage(1f);
        UpdateHealthBar(m_currentHealth);

        yield return new WaitForSeconds(1f);
        Debug.Log(m_currentHealth);
        Damage(1f);
        UpdateHealthBar(m_currentHealth);

        yield return new WaitForSeconds(1f);
        Debug.Log(m_currentHealth);
        Damage(1f);
        UpdateHealthBar(m_currentHealth);
    }

    public void UpdateHealthBar (float value)
    {
        m_healthBar.sizeDelta = new Vector2(value / m_maxHealth * 500f, m_healthBar.sizeDelta.y);
    }

    public void Damage(float damage, PlayerController pc = null)
    {
        if (!isServer)
        {
            return;
        }

        m_currentHealth -= damage;

        if (m_currentHealth <= 0 && !m_isDead)
        {
            m_isDead = true;
            RpcDie();
        }
    }

    [ClientRpc]
    private void RpcDie()
    {
        SetActiveState(false);
    }

    private void SetActiveState(bool state)
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = state;
        }

        foreach (Canvas canvas in GetComponentsInChildren<Canvas>())
        {
            canvas.enabled = state;
        }

        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            rend.enabled = state;
        }
    }

    public void Reset()
    {
        m_currentHealth = m_maxHealth;
        SetActiveState(true);
        m_isDead = false;
    }
}
