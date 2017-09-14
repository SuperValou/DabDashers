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

    public Image VictoryImage;

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
            float y = Players.Aggregate(0f, (sum, player) => sum + player.transform.position.y) / Players.Length;
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
	        return screenPos.x < 0;
	    }))
	    {
	        VictoryImage.gameObject.SetActive(true);
	        foreach (var playerController in Players)
	        {
	            playerController.enabled = false;
	        }
	    }
    }
}
