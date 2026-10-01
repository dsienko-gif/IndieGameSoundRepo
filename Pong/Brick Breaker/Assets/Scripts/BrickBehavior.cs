using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip _scoreHit;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }
    }
}
