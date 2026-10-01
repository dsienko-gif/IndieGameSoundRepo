using UnityEngine;
using TMPro;
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
}