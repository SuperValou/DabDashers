using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Controller : MonoBehaviour
{
    public Metronome Metronome;
    public float ForceScale = 1;

    private Rigidbody2D Rigidbody;

    void Start()
    {
        this.Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Rigidbody.AddForce(new Vector2((float)Math.Pow(Metronome.BeatScore, 2) * ForceScale, 0.1f));
        }
    }
}
