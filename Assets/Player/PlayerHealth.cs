using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour 
{
    private float m_currentHealth;
    public float m_maxHealth = 3f;

    public RectTransform m_healthBar;

    public bool m_isDead = false;

	void Start () 
    {
		
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

        foreach (MeshRenderer rend in GetComponentsInChildren<MeshRenderer>())
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
