using UnityEngine;
using TMPro;
public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    private Utilities.GameState _state;

    public Utilities.GameState State
    {
        get => _state;
        set
        {
            _state = value;
            _pauseUI.enabled = State == Utilities.GameState.Pause;
        }
    }
    
    public Player[] Players = new Player[2];

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballParent;
    GameObject _ball;

    [SerializeField] private int _winningScore = 5;
    
    [SerializeField] private TMP_Text _pauseUI;
    
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
        ResetScore();
        ResetPoint();
        
        State = Utilities.GameState.Play;
    }

    private void Update()
    {
        // State machine transition
        if (Input.GetKeyDown(KeyCode.P))
        {
            State = State == Utilities.GameState.Play ?
                Utilities.GameState.Pause : 
                Utilities.GameState.Play;
        }
        
        if (!_ball)
        {
            ResetPoint();
        }
    }

    void ResetScore()
    {
        // initializer; condition; iterator
        foreach (Player p in Players)
        {
            p.Score = 0;
        }

    }

    public void ResetPoint()
    {
        //Add a prefab to the game
        // This overload
      
        _ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity, _ballParent);
    }

    public void ScorePoint(int playerNumber)
    {
        Players[playerNumber].Score++;
        
        if (Players[playerNumber].Score >= _winningScore)
        {
            ResetScore();
        }
    }
}
