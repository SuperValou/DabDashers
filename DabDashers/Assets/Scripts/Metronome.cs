using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Metronome : MonoBehaviour
    {
        private float _frequency;
        
        public int BeatsPerMinute = 120;
        public float Delay = -0.1f;

        public AudioSource Source;
        
        public float BeatScore { get; private set; }
        
        void Start()
        {
            _frequency = BeatsPerMinute / 60f;
        }
        
        void Update()
        {
            BeatScore = (float) (1 - Math.Abs(Math.Cos(_frequency * Source.time + Delay)));
            //Debug.Log(BeatScore);

            if (BeatScore > 0.8)
            {
                this.gameObject.transform.localScale = 3 * BeatScore * Vector3.one;
            }
            else
            {
                this.gameObject.transform.localScale = Vector3.one;
            }
        }
    }
}