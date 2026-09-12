using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    private float _speed = 5.0f;

    public KeyCode UpDirection = KeyCode.UpArrow;
    public KeyCode DownDirection = KeyCode.DownArrow;

    void Update()
    {
        float movement = 0.0f;
     
        if (Input.GetKey(UpDirection))
        {
            movement += _speed;
        }
        if (Input.GetKey(DownDirection))
        {
            movement -= _speed;
        }
        movement *= Time.deltaTime;

        transform.Translate(0.0f, movement, 0.0f);
    }
}
