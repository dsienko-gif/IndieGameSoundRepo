using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    private int _hp;
    
    
 
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white;
       
        _hp = 3;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _hp -= 1;
            
        }
    }

    void Update()
    {
        if (_hp <= 2)
        {
            _spriteRenderer.color = Color.red;
        }

        if (_hp <= 1)
        {
            _spriteRenderer.color = Color.green;
        }
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
   
}
