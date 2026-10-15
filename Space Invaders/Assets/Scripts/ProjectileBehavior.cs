using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    
    [SerializeField, Range(5.0f, 50.0f)] private float _projectileSpeed = 10.0f;
    void Update()
    {
        transform.Translate(_projectileSpeed * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeleteProjectile"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            GameBehavior.Instance.Score++;
        }
    }
}

