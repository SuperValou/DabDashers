using Assets.Scripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Metronome Metronome;
    public float DashForce = 1000;
    public float JumpForce = 1000;
    public float DefaultUpForce = 100;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        this._rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private int lastBeat;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            Metronome.BeatNumber > lastBeat &&
            Metronome.BeatScore > 0)
        {
            lastBeat = Metronome.BeatNumber;
            this._rigidbody.AddForce(new Vector2(DashForce, DefaultUpForce));
        }
    }
}
