using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    private float _direction = 0.0f;
    [SerializeField] private float _speed = 5.0f;

    [SerializeField] private KeyCode UpDirection = KeyCode.UpArrow;
    [SerializeField] private KeyCode DownDirection = KeyCode.DownArrow;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityY = _direction * _speed;
    }

    void Update()
    {
        _direction = 0.0f;
        if (Input.GetKey(UpDirection))
        {
            _direction += 1.0f;
        }

        if (Input.GetKey(DownDirection))
        {
            _direction -= 1.0f;
        }
    }
}
