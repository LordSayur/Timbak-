using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Cannon : NetworkBehaviour {

    public float m_damage = 1f;

    public PlayerController m_owner;

    public List<string> m_collideTags;

    private void OnCollisionEnter(Collision collision)
    {
        //CheckCollision(collision);

        if (m_collideTags.Contains(collision.gameObject.tag))
        {
            //Explode();
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Damage(m_damage);
            }
        }
    }

    private void Explode()
    {
        //throw new NotImplementedException();
    }

    private void CheckCollision(Collision collision)
    {
        //throw new NotImplementedException();
    }
}
