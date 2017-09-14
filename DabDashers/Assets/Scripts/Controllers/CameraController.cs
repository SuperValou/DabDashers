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

    public Image VictoryImage;

    public float QuenelleSquaredThreshold = 1;
    public float QuenellePower = 10;
    private bool PlayerGotQuenelle = false;

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
    }

	void Update ()
	{
        // center camera between players vertically
        if (Players.Any(p =>
        {
            var screenPos = _camera.WorldToScreenPoint(p.transform.position);
            return screenPos.y < YEdgeLimit || screenPos.y > Screen.height - YEdgeLimit;
        }))
        {
            float y = Players.Max(p => p.transform.position.y);
            float forceY = y - _camera.transform.position.y + YOffset;
            _rigidbody.AddForce(ForceScale * forceY * Vector3.up);
        }

        // keep seeing best player
        if (Players.Any(p => _camera.WorldToScreenPoint(p.transform.position).x > Screen.width - XEdgeLimit))
        {
            float x = Players.Max(p => p.transform.position.x);
            float forceX = x - _camera.transform.position.x + XOffset;
            _rigidbody.AddForce(ForceScale * forceX * Vector3.right);
        }

        if (Players.Any(p =>
        {
            var screenPos = _camera.WorldToScreenPoint(p.transform.position);
            return screenPos.x < -KillPlayer;
        }))
        {
            VictoryImage.gameObject.SetActive(true);
            foreach (var playerController in Players)
            {
                playerController.enabled = false;
            }
        }

        // quenelle
        if (!PlayerGotQuenelle)
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
                        }
                        else
                        {
                            Players[i].GetComponent<Rigidbody2D>().AddForce(Vector3.up * QuenellePower, ForceMode2D.Impulse);
                        }

                        Debug.Log("QUENELLE§");
                        PlayerGotQuenelle = true;
                        Invoke("UnQuenelle", 0.8f);
                    }
                }
            }
        }
    }

    private void UnQuenelle()
    {
        PlayerGotQuenelle = false;
    }
}
