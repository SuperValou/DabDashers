using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody _rigidbody;

    public PlayerController[] Players = new PlayerController[2];

    public float XOffset = 2;
    public float YOffset = 2;
    public float ForceScale = 20;
    public float ClosestCameraZPosition = -8;
    public float CloseToEdgeLimit = 10;

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
        // center camera between players
        if (Players.Any(p =>
        {
            var screenPos = _camera.WorldToScreenPoint(p.transform.position);
            return screenPos.x < CloseToEdgeLimit || screenPos.x > Screen.width - CloseToEdgeLimit
                || screenPos.y < CloseToEdgeLimit || screenPos.y > Screen.height - CloseToEdgeLimit;
        }))
        {
            float x = Players.Aggregate(0f, (sum, player) => sum + player.transform.position.x) / Players.Length;
            float y = Players.Aggregate(0f, (sum, player) => sum + player.transform.position.y) / Players.Length;
            
            float forceX = x - _camera.transform.position.x + XOffset;
            float forceY = y - _camera.transform.position.y + YOffset;

            _rigidbody.AddForce(ForceScale * new Vector3(forceX, forceY, 0));
        }

        // move camera forward or backward to adjust screen space
        
    }
}
