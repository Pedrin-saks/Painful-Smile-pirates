using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject prefabHpBar;
    public Transform parentContentHpBar;
    public static GameManager Instance;

    [HideInInspector]
    public Transform PlayerPosition;

    [Header("Settings")]
    [Range(60,180)]
    [SerializeField] private float gameTime;
    [Range(1, 10)]
    [SerializeField] private float spawnInterval;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemyPrefab;

    [Header("UI")]
    [SerializeField] private GameObject painelFinishGame;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    private float timer;
    private bool gameIsRunning = true;
    private int scorePlayer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = PlayerPrefs.GetFloat("GameSessionTime", gameTime);
        InvokeRepeating("SpawnEnemy", 0f, PlayerPrefs.GetFloat("EnemySpawnTime", spawnInterval));
    }

    private void OnEnable()
    {
        EventsManager.OnFinishGame += FinishGame;
        EventsManager.OnEnemyDie += PlayerScoreUpdate;
    }

    private void OnDisable()
    {
        EventsManager.OnFinishGame -= FinishGame;
        EventsManager.OnEnemyDie -= PlayerScoreUpdate;
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                gameIsRunning = false;
                timer = 0f;
                EventsManager.OnFinishGameTrigger();
            }

            UpdateTimerUI();
        }
    }

    public void PlayerRegister(Transform player)
    {
        this.PlayerPosition = player;
    }

    public void FinishGame()
    {
        Time.timeScale = 0;
        scoreText.text = "Score Earned: " + scorePlayer.ToString();
        painelFinishGame.SetActive(true);

    }

    public void PlayerScoreUpdate()
    {
        scorePlayer++;
    }

    private void SpawnEnemy()
    {
        if (gameIsRunning)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject randomEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Count)];
            Instantiate(randomEnemy, randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = $"Tempo: {TimeFormatter.FormatTime(timer)}";
    }



}
