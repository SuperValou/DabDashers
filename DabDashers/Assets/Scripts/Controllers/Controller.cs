using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Controller : MonoBehaviour
{
    public Metronome Metronome;
    public float forceScale = 10;

    private Rigidbody2D Rigidbody;

    void Start()
    {
        this.Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.transform.Translate(forceScale * Metronome.BeatScore, 0.1f, 0);
        }
    }
}
