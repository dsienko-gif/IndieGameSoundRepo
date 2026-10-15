using UnityEngine;

public class SpaceshipBehavior : MonoBehaviour
{
    private float _direction = 0.0f;
    [SerializeField] private float _speed = 5.0f;
    [HideInInspector] public bool CanShoot = true;
    [SerializeField] private KeyCode rightDirection = KeyCode.RightArrow;
    [SerializeField] private KeyCode leftDirection = KeyCode.LeftArrow;
    [SerializeField] private KeyCode _shootKey = KeyCode.Space;
    [SerializeField] private AudioClip _shoot;
    private Rigidbody2D _rb;
    private AudioSource _source;
    [Header("Projectile")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private Transform _projectileParent;
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
        if (GameBehavior.Instance.State == Utilities.GameState.Play)
        {
            if (Input.GetKey(rightDirection))
            {
                _direction += 1.0f;
            }

            if (Input.GetKey(leftDirection))
            {
                _direction -= 1.0f;
            }
            
            if (CanShoot && Input.GetKeyDown(_shootKey))
            {
                Instantiate(_projectilePrefab, 
                    _projectileSpawnPoint.position, 
                    _projectileSpawnPoint.rotation,
                    _projectileParent);
            }
        }
        
    }
}