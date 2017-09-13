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
    
    public DabState State { get; set; }

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
            Dash();
            return;
        }
        if (Input.GetButtonDown("Jump_player" + PlayerId) &&
            Metronome.BeatNumber > lastBeat &&
            Metronome.BeatScore > 0)
        {
            lastBeat = Metronome.BeatNumber;
            Jump();
            return;
        }
        if (this._rigidbody.velocity.y < 0)
        {
            this.State = DabState.GoingDown;
            return;
        }

        if (this._rigidbody.velocity.magnitude < 0.01)
        {
            this.State = DabState.Idle;
            return;
        }
    }

    private void Dash()
    {
        this._rigidbody.AddForce(new Vector2(DashForce, DefaultUpForce));
        this.State = DabState.Dabbing;
    }

    private void Jump()
    {
        this._rigidbody.AddForce(new Vector2(DefaultForwardForce, JumpForce));
        this.State = DabState.JumpingUp;
    }
}
