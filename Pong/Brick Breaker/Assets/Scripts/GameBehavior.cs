using UnityEngine;
using TMPro;
public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    public Player[] Players = new Player[2];
    
    
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
        // initializer; condition; iterator
        foreach (Player p in Players)
        {
            p.Score = 0;
        }
    }

    public void ScorePoint(int playerNumber)
    {
        Players[playerNumber].Score++;
    }
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

