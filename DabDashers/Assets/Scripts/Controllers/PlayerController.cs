using Assets.Scripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _lastBeat;
    
    private Rigidbody2D _rigidbody;
    private ConstantForce2D _forwardForce;
    
    public int PlayerId = 1;
    public Metronome Metronome;
    public float DashForce = 1000;
    public float JumpForce = 1000;
    public float DefaultUpForce = 100;
    public float DefaultForwardForce = 100;

    private DabState _state;

    public DabState State
    {
        private get { return _state; }
        set
        {
            if (value != DabState.JumpingUp)
            {
                _forwardForce.force = Vector3.zero;
            }

            _state = value;
        }
    }

    void Start()
    {
        this._rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        if (Metronome == null)
        {
            Debug.LogError("You don't have set up the metronome!");
        }

        this._forwardForce = this.gameObject.GetComponent<ConstantForce2D>();
        if (_forwardForce == null)
        {
            Debug.LogError("You don't have set up the constant force!");
        }
        else
        {
            _forwardForce.force = Vector3.zero;
        }
    }


    void Update ()
    {
        if (Input.GetButtonDown("Dash_player" + PlayerId) &&
            Metronome.BeatNumber > _lastBeat &&
            Metronome.BeatScore > 0)
        {
            _lastBeat = Metronome.BeatNumber;
            Dash();
            return;
        }
        if (Input.GetButtonDown("Jump_player" + PlayerId) &&
            Metronome.BeatNumber > _lastBeat &&
            Metronome.BeatScore > 0)
        {
            _lastBeat = Metronome.BeatNumber;
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
        this._rigidbody.AddForce(new Vector2(DashForce, DefaultUpForce), ForceMode2D.Impulse);
        this.State = DabState.Dabbing;
    }

    private void Jump()
    {
        this.State = DabState.JumpingUp;
        _forwardForce.force = Vector3.right * DashForce;

        this._rigidbody.AddForce(Vector2.up * JumpForce);
    }
}
