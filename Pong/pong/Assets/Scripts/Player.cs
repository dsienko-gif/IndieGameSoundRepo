using UnityEngine;
using TMPro;
using System.Collections;
public class Player : MonoBehaviour
{

   [SerializeField] private TMP_Text scoreUI;
    
    // backing variable
    private int _score;
 // access point/public variable :p
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreUI.SetText (Score.ToString());
        }
    }

    private PaddleBehavior _paddle;

    [SerializeField, Range(1.0f, 10.0f)] private float _powerUpDuration = 5.0f;

    private void Start()
    {
        _paddle = GetComponent<PaddleBehavior>();
    }

    public IEnumerator PowerUp()
    {
        if (_paddle.CanShoot)
        {
            yield break;
        }

        _paddle.CanShoot = true;
        
        yield return new WaitForSeconds(_powerUpDuration);

        _paddle.CanShoot = false;

    }
}
