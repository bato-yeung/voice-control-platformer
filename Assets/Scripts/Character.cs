using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Collider2D _collisionBox;

    [SerializeField]
    private Renderer _renderer;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
>>>>>>> Stashed changes
    private float _moveSpeed = 2f; // move distance per second

    [SerializeField]
    private float _jumpMagnitude = 8f;

    [SerializeField]
    private AudioSource _micAudioSource;

    [SerializeField]
    private AudioPitchEstimator _audioPitchEstimator;

    private bool _isAlive = true;
    private bool _isGrounded = false;

    public event Action<object> Died;
    public event Action<object> Resurrected;

    private const float JumpPitchThreshold = 200f;

    private void Start()
    {
        int length = Microphone.devices.Length;
        for (int i = 0; i < length; i++)
        {
            Debug.Log($"Microphone.devices[{i}]: {Microphone.devices[i]}");
        }

        string deviceName = Microphone.devices[1];
        _micAudioSource.clip = Microphone.Start(deviceName, true, 1, 44100);

        while (!(Microphone.GetPosition(deviceName) > 0)) { }

        _micAudioSource.Play();
    }

    private void Update()
    {
        _animator.SetFloat("Abs(Velocity.x)", Mathf.Abs(_rigidbody2D.velocity.x));
        _animator.SetFloat("Velocity.y", _rigidbody2D.velocity.y);
        _animator.SetBool("IsGrounded", _isGrounded);
    }

    private void FixedUpdate()
    {
        _isGrounded = CheckGrounded();
=======
    private void Update()
    {
        if (_isAlive == false)
        {
            return;
        }

        _animator.SetFloat("Abs(Velocity.x)", Mathf.Abs(_rigidbody2D.velocity.x));
        _animator.SetFloat("Velocity.y", _rigidbody2D.velocity.y);
        _animator.SetBool("IsGrounded", _isGrounded);
    }

    private void FixedUpdate()
    {
        if (_isAlive == false)
        {
            return;
        }

        _isGrounded = CheckGrounded();

        float horizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
>>>>>>> Stashed changes

        float horizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");

        if (Mathf.Abs(horizontal) > 0f)
        {
            Vector2 velocity = _rigidbody2D.velocity;
            velocity.x = Mathf.Sign(horizontal) * _moveSpeed;
            _rigidbody2D.velocity = velocity;
        }

        if (jump > 0f)
        {
            if (_isGrounded == true)
            {
                Jump(1f);
            }
        }

        float pitch = _audioPitchEstimator.Estimate(_micAudioSource);
        //Debug.Log($"pitch: {pitch}");
        if (float.IsNaN(pitch) == false)
        {
            MoveForward();

            if (pitch > JumpPitchThreshold)
            {
                if (_isGrounded == true)
                {
                    float ratio = (pitch - JumpPitchThreshold) / (_audioPitchEstimator.frequencyMax - JumpPitchThreshold);
                    Jump(ratio);
                }
            }
        }

        if (transform.position.y < -2f)
        {
            Die();
        }
    }

    private void MoveForward()
    {
        Vector2 velocity = _rigidbody2D.velocity;
        velocity.x = _moveSpeed;
        _rigidbody2D.velocity = velocity;
    }

    private void Jump(float ratio)
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpMagnitude * ratio, ForceMode2D.Impulse);
    }

    private bool CheckGrounded()
    {
        Vector2 origin = new Vector2(_collisionBox.bounds.center.x, _collisionBox.bounds.min.y);
        int layerMask = LayerMask.GetMask("Terrain");
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.02f, layerMask);
        return (hit.collider != null);
    }

    public void Freeze()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.isKinematic = true;
    }

    public void Unfreeze()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.isKinematic = false;
    }

    public void Die()
    {
        Freeze();

        _renderer.gameObject.SetActive(false);

        Died?.Invoke(this);
    }

    public void Resurrect()
    {
        Unfreeze();

        _renderer.gameObject.SetActive(true);

        Resurrected?.Invoke(this);
    }
}