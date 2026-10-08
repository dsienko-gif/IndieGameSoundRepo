using UnityEngine;
using TMPro;
public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
    [SerializeField] private TMP_Text scoreUI;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballParent;
    [SerializeField] private TMP_Text _pauseUI;
    GameObject _ball;
    // backing variable
    private int _score;
    // access point/public variable :p
    
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
    
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreUI.SetText(Score.ToString());
        }
    }
  
    void ResetScore()
    {
        Score = 0;

    }
    
    public void ResetPoint()
    {
        //Add a prefab to the game
        // This overload
      
        _ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity, _ballParent);
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
        ResetScore();
        ResetPoint();
        
        State = Utilities.GameState.Play;
    }

    void ResetGame()
    {
        Score = 0;
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
}

