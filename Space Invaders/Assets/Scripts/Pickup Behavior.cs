using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    [SerializeField, Range(0.0f, 100.0f)] private float _rotateSpeed = 20.0f;
    private void Update()
    {
        if (GameBehavior.Instance.State == Utilities.GameState.Play)
        {
            transform.Rotate(0.0f, 0.0f, _rotateSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D projRB = other.GetComponent<Rigidbody2D>();

        if (projRB)
        {
            GameBehavior.Instance.PowerUp(
                projRB.linearVelocityX >= 0.0f ? 0 : 1
            );
        }
        
        Destroy(gameObject);
    }
}
