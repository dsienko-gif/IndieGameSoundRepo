using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    private float _direction = 0.0f;
    [SerializeField] private float _speed = 5.0f;

    [SerializeField] private KeyCode RightDirection = KeyCode.RightArrow;
    [SerializeField] private KeyCode LeftDirection = KeyCode.DownArrow;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = _direction * _speed;
    }

    void Update()
    {
        _direction = 0.0f;
        if (Input.GetKey(RightDirection))
        {
            _direction += 1.0f;
        }

        if (Input.GetKey(LeftDirection))
        {
            _direction -= 1.0f;
        }
    }
}

