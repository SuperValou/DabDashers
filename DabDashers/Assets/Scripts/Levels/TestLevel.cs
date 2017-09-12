using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TestLevel : MonoBehaviour
{
    public AudioSource audioSource;
    public Metronome metronome;
    
    void Start ()
    {
        audioSource.Play();
    }
}
