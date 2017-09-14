using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public Metronome metronome;
    public float pulsePower = 1;

    private Vector3 startingScale;

    void Start()
    {
        startingScale = this.transform.localScale;
    }

	void Update ()
    {
        this.gameObject.transform.localScale = startingScale + Vector3.one * metronome.BeatScore * pulsePower;
    }
}
