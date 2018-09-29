using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float mSize = 1;
    private int x = 1;
    public float rate = 0.02f;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Scale", 0, rate);
	}
	
	void Scale()
    {
        mSize += x;

        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, mSize);
        if (mSize >= 60)
        {
            x*=-1;
        }

        if (mSize <= 0)
        {
            x *= -1;
        }

        
    }
}
