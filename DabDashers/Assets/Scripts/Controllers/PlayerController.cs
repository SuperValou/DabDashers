using Assets.Scripts;
using Boo.Lang.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _lastBeat;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    public int PlayerId = 1;
    public Metronome Metronome;
    public float DashForce = 30;
    public float JumpForce = 30;
    public float DefaultUpForce = 1;
    public float DefaultForwardForce = 15;
    
    void Start()
    {
        this._rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        if (Metronome == null)
        {
            Debug.LogError("You don't have set up the metronome!");
        }
        
        this._animator = this.gameObject.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("You don't have set up the animator!");
        }
    }

    void Update()
    {
        this._animator.SetFloat("VelocityX", this._rigidbody.velocity.x);
        this._animator.SetFloat("VelocityY", this._rigidbody.velocity.y);

        if (_lastBeat == Metronome.BeatNumber)
        {
            return;
        }

        if (Input.GetButtonDown("Dash_player" + PlayerId))
        {
            if (_lastBeat < Metronome.BeatNumber && Metronome.BeatScore > 0)
            {
                Dash();
                this._animator.SetBool("MisplacedInput", false);
            }
            else
            {
                Debug.Log("Misplaced input!");
                this._animator.SetBool("Dashed", false);
                this._animator.SetBool("MisplacedInput", true);
                this._rigidbody.AddForce(new Vector2(DashForce / 3, DefaultUpForce), ForceMode2D.Impulse);
            }

            _lastBeat = Metronome.BeatNumber;
            this._animator.SetBool("Jumped", false);
            return;
        }
        
        if (Input.GetButtonDown("Jump_player" + PlayerId))
        {
            if (_lastBeat < Metronome.BeatNumber && Metronome.BeatScore > 0)
            {
                Jump();
                this._animator.SetBool("MisplacedInput", false);
            }
            else
            {
                Debug.Log("Misplaced input!");
                this._animator.SetBool("Jumped", false);
                this._animator.SetBool("MisplacedInput", true);
            }

            _lastBeat = Metronome.BeatNumber;
            this._animator.SetBool("Dashed", false);
            return;
        }
        
        this._animator.SetBool("Dashed", false);
        this._animator.SetBool("Jumped", false);
        this._animator.SetBool("MisplacedInput", false);
    }

    private void Dash()
    {
        Debug.Log("Dash!");
        this._animator.SetBool("Dashed", true);
        this._rigidbody.AddForce(new Vector2(DashForce, DefaultUpForce), ForceMode2D.Impulse);
    }

    private void Jump()
    {
        Debug.Log("Jump!");
        this._animator.SetBool("Jumped", true);
        _rigidbody.AddForce(new Vector2(DefaultForwardForce, JumpForce), ForceMode2D.Impulse);
        Invoke("TinyForwardInput", 0.2f);
    }

    private void TinyForwardInput()
    {
        _rigidbody.AddForce(Vector2.right * DefaultForwardForce, ForceMode2D.Impulse);
    }
}