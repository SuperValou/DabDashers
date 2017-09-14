using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody _rigidbody;

    public PlayerController[] Players = new PlayerController[2];

    public float XOffset = 2;
    public float YOffset = 2;
    public float ForceScale = 20;
    public float ClosestCameraZPosition = -8;
    public float YEdgeLimit = 10;
    public float XEdgeLimit = 10;
	
	public float KillPlayer = 1;

    public Image VictoryImage1;
    public Image VictoryImage2;

    public float QuenelleSquaredThreshold = 1;
    public float QuenellePower = 10;
    private bool _playerGotQuenelle = false;

    public AudioSource FxAudioSource;
    public AudioSource MusicAudioSource;
    public AudioClip Player1Leader;
    public AudioClip Player2Leader;

    public AudioClip Victory1;
    public AudioClip Victory2;

    void Start()
    {
        _camera = this.gameObject.GetComponent<Camera>();
        if (_camera == null)
        {
            Debug.LogError("No camera found");
            this.enabled = false;
        }

        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("No rigidbody found");
            this.enabled = false;
        }

        if (Players.Any(p => p == null) || Players.Length == 0)
        {
            Debug.LogError("Players not set up!");
            this.enabled = false;
        }

        MusicAudioSource.volume = 1;
    }

	void Update ()
	{
        float bestX = Players.Max(p => p.transform.position.x);

        // center camera between players vertically
        if (Players.Any(p =>
        {
            var screenPos = _camera.WorldToScreenPoint(p.transform.position);
            return screenPos.y < YEdgeLimit || screenPos.y > Screen.height - YEdgeLimit;
        }))
        {
            float y = Players.First(p => p.transform.position.x == bestX).transform.position.y;
            float forceY = y - _camera.transform.position.y + YOffset;
            _rigidbody.AddForce(ForceScale * forceY * Vector3.up);
        }

        // keep seeing best player
        if (Players.Any(p => _camera.WorldToScreenPoint(p.transform.position).x > Screen.width - XEdgeLimit))
        {
            float forceX = bestX - _camera.transform.position.x + XOffset;
            _rigidbody.AddForce(ForceScale * forceX * Vector3.right);
        }

        // end of game
        if (Players.Any(p =>
        {
            var screenPos = _camera.WorldToScreenPoint(p.transform.position);
            return screenPos.x < -KillPlayer;
        }))
        {
            if (Players[0].transform.position.x < Players[1].transform.position.x)
            {
                VictoryImage2.gameObject.SetActive(true);
                FxAudioSource.PlayOneShot(Victory2);
            }
            else
            {
                VictoryImage1.gameObject.SetActive(true);
                FxAudioSource.PlayOneShot(Victory1);
            }

            MusicAudioSource.volume /= 10;

            foreach (var playerController in Players)
            {
                playerController.enabled = false;
            }

            this.enabled = false;
        }

        // quenelle
        if (!_playerGotQuenelle)
	    {
            for (int i = 0; i < Players.Length; i++)
            {
                for (int j = i + 1; j < Players.Length; j++)
                {
                    if (MiniMath.GetSquaredDistance(Players[i].transform.position, Players[j].transform.position) < QuenelleSquaredThreshold)
                    {
                        if (Players[i].transform.position.x < Players[j].transform.position.x)
                        {
                            Players[j].GetComponent<Rigidbody2D>().AddForce(Vector3.up * QuenellePower, ForceMode2D.Impulse);
                            FxAudioSource.PlayOneShot(Player2Leader);
                        }
                        else
                        {
                            Players[i].GetComponent<Rigidbody2D>().AddForce(Vector3.up * QuenellePower, ForceMode2D.Impulse);
                            FxAudioSource.PlayOneShot(Player1Leader);
                        }

                        Debug.Log("QUENELLE§");
                        _playerGotQuenelle = true;
                        Invoke("UnQuenelle", 0.8f);
                    }
                }
            }
        }
    }

    private void UnQuenelle()
    {
        _playerGotQuenelle = false;
    }
}
