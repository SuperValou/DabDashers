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
    public float DashForce = 1;
    public float JumpForce = 1;
    public float DefaultUpForce = 0.1f;

    private Rigidbody2D Rigidbody;

    void Start()
    {
        this.Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private int lastBeat;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            Metronome.BeatNumber > lastBeat)
        {
            lastBeat = Metronome.BeatNumber;
            this.Rigidbody.AddForce(new Vector2(Metronome.BeatScore * DashForce, DefaultUpForce));
            //this.Rigidbody.AddForce(new Vector2(ForceScale, 0.1f));
        }
    }
}
