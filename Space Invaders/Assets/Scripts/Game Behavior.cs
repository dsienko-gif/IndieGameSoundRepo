using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameBehavior : MonoBehaviour
{
    private SpaceshipBehavior _spaceship;
    [SerializeField] private float _powerUpDuration = 10f;
    [SerializeField] private TMP_Text scoreUI;
    
    private int _score;

    public static GameBehavior Instance;
    
   
    [Header("State")]
    private Utilities.GameState _state;

    [SerializeField] private TMP_Text _pauseUI;
    [SerializeField] private TMP_Text hpUI;
    private int _php;
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

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreUI.SetText(Score.ToString());
        }
    }
    public int Health
    {
        get => _php;
        set
        {
            _php = value;
            hpUI.SetText(Health.ToString());
        }
    }

    void Start()
    {  
        {
            _spaceship = GetComponent<SpaceshipBehavior>();
        }
        BoxCollider2D powerUpCollider = _powerUpPrefab.GetComponent<BoxCollider2D>();

        if (powerUpCollider)
        {
            _prefabSize = powerUpCollider.size;
        }
        
        
        ResetScore();
        ResetHealth();
        
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
        if (Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void ResetScore()
    {
        Score = 0;
    }

    void ResetHealth()
    {
        Health = 3;
    }
  
    public IEnumerator PowerUp()
    {
        if (_spaceship.CanShoot)
        {
            yield break;
        }

        _spaceship.CanShoot = true;
        
        yield return new WaitForSeconds(_powerUpDuration);

        _spaceship.CanShoot = false;

    }

}
