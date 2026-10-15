using UnityEngine;
using TMPro;
public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
    
    [Header("State")]
    private Utilities.GameState _state;
    [SerializeField] private TMP_Text _pauseUI;
    public Utilities.GameState State
    {
        get => _state;
        set
        {
            _state = value;
            _pauseUI.enabled = State == Utilities.GameState.Pause;
        }
    }
    [Header("General")]
    public Player[] Players = new Player[2];

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballParent;
    GameObject _ball;

    [SerializeField] private int _winningScore = 5;
    
    [Header("PowerUp")]
    [SerializeField] private GameObject _powerUpPrefab;
    [SerializeField] private Transform _powerUpParent;
    private GameObject _powerUp;

    [SerializeField] private Vector2 _powerUpSpawnBounds = new Vector2(7.0f, 4.5f);

    [SerializeField, Range(0.0000001f, 0.01f)] private float _powerUpSpawnProbablity = 0.0001f;
    private Vector2 _prefabSize;
    void Awake()
    {
        // Singleton Pattern
        // Enforce that there is only ever a single instance of this class
        // Throughout execution of the program
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        BoxCollider2D powerUpCollider = _powerUpPrefab.GetComponent<BoxCollider2D>();

        if (powerUpCollider)
        {
            _prefabSize = powerUpCollider.size;
        }
        
        
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

        if (State == Utilities.GameState.Play)
        {
            if (!_ball)
            {
                ResetPoint();
            }

            if (!_powerUp && Random.value < _powerUpSpawnProbablity)
            {
                Vector2 position;
                do
                {
                    position = new Vector2(
                        Random.Range(-_powerUpSpawnBounds.x, _powerUpSpawnBounds.x),
                        Random.Range(-_powerUpSpawnBounds.y, _powerUpSpawnBounds.y)
                    );
                } while (
                    Physics2D.OverlapBox(position, _prefabSize, 0.0f) &&
                    Physics2D.OverlapBox(position, _prefabSize, 45.0f)
                    );
                _powerUp = Instantiate (_powerUpPrefab, position, Quaternion.identity, _powerUpParent);
            }
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

    public void PowerUp(int playerNumber)
    {
        StartCoroutine(Players[playerNumber].PowerUp());
    }
}
