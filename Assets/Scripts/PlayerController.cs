using System;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _moveSpeed;
    private Rigidbody _playerRb;
    private direction _playerDirection;
    private float _currentAngle;
    private struct direction 
    { 
        public bool Forward; 
        public bool Backward;
        public bool Right;
        public bool Left;

        public direction(bool forward, bool backward, bool right, bool left)
        {
            Forward = forward;
            Backward = backward;
            Right = right;
            Left = left;
        }
    };

    void Start()
    {
        _playerDirection = new direction(false, false, false, false);
        _playerRb = GetComponent<Rigidbody>();
        _currentAngle = gameObject.transform.rotation.y;
    }

    void Update()
    {
        _playerDirection.Forward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        _playerDirection.Backward = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        _playerDirection.Left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        _playerDirection.Right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    void FixedUpdate()
    {
        if (_playerDirection.Forward)
        {
            Vector3 movement = gameObject.transform.forward;
            _playerRb.MovePosition(_playerRb.position + movement * _moveSpeed);
        }
        if (_playerDirection.Backward)
        {
            Vector3 movement = -gameObject.transform.forward;
            _playerRb.MovePosition(_playerRb.position + movement * _moveSpeed);
        }
        if (_playerDirection.Right)
        {
             _currentAngle += _rotationSpeed * Time.deltaTime;
             Quaternion targetRotation = Quaternion.Euler(0, _currentAngle, 0);
             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        }
        if (_playerDirection.Left)
        {
             _currentAngle -= _rotationSpeed * Time.deltaTime;
             Quaternion targetRotation = Quaternion.Euler(0, _currentAngle, 0);
             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        }
    }
}
