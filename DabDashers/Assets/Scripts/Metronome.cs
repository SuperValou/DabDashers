﻿using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Metronome : MonoBehaviour
    {
        private float _frequency;
        
        public int BeatsPerMinute = 120;
        
        public float PiPhase = 1;

        public AudioSource Source;
        
        public float BeatScore { get; private set; }
        
        public int BeatNumber { get; private set; }

        void Start()
        {
            _frequency = BeatsPerMinute / 60f;
        }
        
        void Update()
        {
            // set the current beat number
            BeatNumber = (int) Math.Floor(Source.time*_frequency);

            // set the score value of an input that would happen now
            BeatScore = Mathf.Clamp(
                (float)Math.Abs(Math.Cos(Math.PI*_frequency*Source.time + PiPhase*Math.PI)) - 0.8f, // |cos(pi * 2 * x + 0.5 * pi)| - 0.8
                0f, 1f);
            
            this.gameObject.transform.localScale = Vector3.one + Vector3.one * BeatScore;
            
        }
    }
}