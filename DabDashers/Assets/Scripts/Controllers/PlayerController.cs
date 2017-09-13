using Assets.Scripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public int PlayerId = 1;
    public Metronome Metronome;
    public float DashForce = 1000;
    public float JumpForce = 1000;
    public float DefaultUpForce = 100;
    public float DefaultForwardForce = 100;
    
    void Start()
    {
        this._rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        if (Metronome == null)
        {
            Debug.LogError("You don't have set up the metronome!");
        }
    }

    private int lastBeat;

    void Update ()
    {
        if (Input.GetButtonDown("Dash_player" + PlayerId) &&
            Metronome.BeatNumber > lastBeat &&
            Metronome.BeatScore > 0)
        {
            lastBeat = Metronome.BeatNumber;
            this._rigidbody.AddForce(new Vector2(DashForce, DefaultUpForce));
            return;
        }
        if (Input.GetButtonDown("Jump_player" + PlayerId) &&
            Metronome.BeatNumber > lastBeat &&
            Metronome.BeatScore > 0)
        {
            lastBeat = Metronome.BeatNumber;
            this._rigidbody.AddForce(new Vector2(DefaultForwardForce, JumpForce));
            return;
        }
    }
}
