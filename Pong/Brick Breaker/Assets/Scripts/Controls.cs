using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    private float _speed = 5.0f;

    public KeyCode RightDirection = KeyCode.RightArrow;
    public KeyCode LeftDirection = KeyCode.LeftArrow;

    void Update()
    {
        float movement = 0.0f;
     
        if (Input.GetKey(RightDirection))
        {
            movement += _speed;
        }
        if (Input.GetKey(LeftDirection))
        {
            movement -= _speed;
        }
        movement *= Time.deltaTime;

        transform.Translate(movement, 0.0f, 0.0f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), -4.43f, 0);
    }
}

