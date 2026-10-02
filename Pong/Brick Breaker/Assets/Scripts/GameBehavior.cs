using UnityEngine;
using TMPro;
public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
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
            scoreUI.SetText(Score.ToString());
        }
    }
  
    
    
    void Awake()
    {
        // Singleton Pattern
        // Enforce that there is only ever a single instance of this class
        // Throughout execution of the program
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("New instance initialized...");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate instance found and deleted...");
        }
    }


    void Start()
    {
        ResetGame();
    }

    void ResetGame()
    {
        Score = 0;
    }

}

