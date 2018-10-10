using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerSetup : NetworkBehaviour
{
    public Color m_playerColor;
    public string m_basename = "Lord";

    public int m_playerNum = 1;
    public Text m_playerNameText;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (m_playerNameText != null)
        {
            m_playerNameText.enabled = false;
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        //CmdSetupPlayer();
        UpdateName(m_playerNum);
        UpdateColor(m_playerColor);
    }

    void Start ()
    {
        if (!isLocalPlayer)
        {
            //UpdateName(m_playerNum);
            //UpdateColor(m_playerColor);
        }
	}

    private void UpdateColor(Color m_playerColor)
    {
        Renderer renderer = GameObject.Find("Sails").GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = m_playerColor;
        }
    }

    private void UpdateName(int m_playerNum)
    {
        if (m_playerNameText != null)
        {
            m_playerNameText.enabled = true;
            m_playerNameText.text = m_basename + " " + m_playerNum.ToString();
            //m_playerNameText.color = m_playerColor;
        }
    }
}
