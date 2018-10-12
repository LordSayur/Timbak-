using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public List<string> m_collideTags;

    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision);

        if (m_collideTags.Contains(collision.gameObject.tag))
        {
            Explode();
        }
    }

    private void Explode()
    {
        throw new NotImplementedException();
    }

    private void CheckCollision(Collision collision)
    {
        throw new NotImplementedException();
    }
}
