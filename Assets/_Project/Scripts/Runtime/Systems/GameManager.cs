using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [HideInInspector]
    public Transform PlayerPosition;

    public float gameTime = 180f; // Tempo de jogo em segundos (3 minutos por padrão)
    public float spawnInterval = 10f; // Intervalo de spawn de inimigos em segundos
    public Transform[] spawnPoints; // Pontos de spawn aleatórios
    public GameObject enemyPrefab; // Prefab do inimigo
    //public Text timerText; // Texto para exibir o tempo no canvas

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerRegister(Transform player)
    {
        this.PlayerPosition = player;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
