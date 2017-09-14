using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float maxSize = 1;
    public float speed = 1.5f;

    // Update is called once per frame
    void Update ()
	{
	    if (this.transform.localScale.x < maxSize)
	    {
            this.transform.localScale *= speed;
        }
    }
}
