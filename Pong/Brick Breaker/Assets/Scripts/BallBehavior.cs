using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _launchForce = 7.0f;
    [SerializeField] private float _paddleInfluence = 0.4f;
    [SerializeField] private float _speedIncrement = 1.1f;
    Rigidbody2D _rb;
    [SerializeField, Range(0.0f,1.0f)] private float _steepnessThreshold = 0.25f;
    private AudioSource _source;
    [Header("Audio Properties")]
    [SerializeField] private AudioClip _wallHit;
    [SerializeField] private AudioClip _paddleHit;
    [SerializeField] private AudioClip _scoreHit;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();

        ResetBall();
    }

    private void CheckSteepness(ref Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < _steepnessThreshold)
        {
            direction.x += 0.5f * Mathf.Sign(direction.x);
            direction.Normalize();
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            _source.PlayOneShot(_scoreHit);
            GameBehavior.Instance.Score++;
        }
        if (collision.gameObject.CompareTag("Paddle")) 
        {
                if (!Mathf.Approximately(collision.rigidbody.linearVelocityY, 0.0f))
                {
                
                    // We compute direction using a weighted sum, where the weights ->
                    // use a one-minus to be determined
                    Vector2 direction = _rb.linearVelocity * (1.0f - _paddleInfluence)
                                        + collision.rigidbody.linearVelocity * _paddleInfluence;

                    CheckSteepness(ref direction);
                
                    _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized * _speedIncrement;
                }
                _source.PlayOneShot(_paddleHit);
        }
        else
        {
                _source.pitch = Random.Range(0.9f, 1.1f);
                _source.volume = Random.Range(0.8f, 1.0f);
                _source.clip = _wallHit;
                _source.Play();
        }
    }
   
    void Update()
    {
        _rb.simulated = GameBehavior.Instance.State == Utilities.GameState.Play;
    }
    
    private void DestroyBall()
    {
        Destroy(gameObject);
    }
 
    private void ResetBall()
    {
        //Stop the ball
        // _rb.linearVelocity = Vector2.zero;
        
        //TP the ball to middle of screen
        // transform.position = Vector3.zero;
        
        Vector2 direction = Random.onUnitCircle;
        
        CheckSteepness(ref direction);
        
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }
}  
    