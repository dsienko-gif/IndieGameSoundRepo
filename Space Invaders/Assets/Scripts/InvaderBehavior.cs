using System;
using UnityEngine;

public class InvaderBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip _hit;
    private AudioSource _source;
    [SerializeField] private float _invaderSpeed = 1.0f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(_invaderSpeed * Time.deltaTime * Vector3.down);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeleteEnemy"))
        {
            Destroy(gameObject);
            GameBehavior.Instance.Health -= 1;
        }
    }
}
