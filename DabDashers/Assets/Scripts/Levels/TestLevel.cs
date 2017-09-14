using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class TestLevel : MonoBehaviour
{
    public AudioSource audioSource;
    public Metronome metronome;

    public Image Asset3;
    public Image Asset2;
    public Image Asset1;
    public Image AssetGo;

    public PlayerController player1;
    public PlayerController player2;

    void Start ()
    {
        audioSource.Play();
        player1.enabled = false;
        player2.enabled = false;
    }

    void Update()
    {
        switch (metronome.BeatNumber)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                Asset3.gameObject.SetActive(true);
                break;

            case 3:
                Asset3.gameObject.SetActive(false);
                Asset2.gameObject.SetActive(true);
                break;
            case 4:
                Asset2.gameObject.SetActive(false);
                Asset1.gameObject.SetActive(true);
                break;

            case 5:
                Asset1.gameObject.SetActive(false);
                AssetGo.gameObject.SetActive(true);

                player1.enabled = true;
                player2.enabled = true;
                break;

            default:
                AssetGo.gameObject.SetActive(false);
                this.enabled = false;
                break;
        }
    }
}
