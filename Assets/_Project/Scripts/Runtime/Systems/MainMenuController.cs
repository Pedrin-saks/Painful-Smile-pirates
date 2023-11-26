using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] TMP_Text txtGameSessionTime;
    [SerializeField] TMP_Text txtEnemySpawnTime;

    [SerializeField] Slider sliderGameSession;
    [SerializeField] Slider sliderEnemySpawn;

    private void Start()
    {
        sliderGameSession.value = PlayerPrefs.GetFloat("GameSessionTime", 60);
        sliderEnemySpawn.value = PlayerPrefs.GetFloat("EnemySpawnTime", 1);

        ValueChangeGameSessionTime(sliderGameSession.value);
        ValueChangeEnemySpawnTime(sliderEnemySpawn.value);
    }

    public void ValueChangeGameSessionTime(float value)
    {
        txtGameSessionTime.text = $"Game Session Time: {TimeFormatter.FormatTime(value)}";
        PlayerPrefs.SetFloat("GameSessionTime", value);
    }

    public void ValueChangeEnemySpawnTime(float value)
    {
        txtEnemySpawnTime.text = $"Enemy Spawn Time: {TimeFormatter.FormatTime(value)}";
        PlayerPrefs.SetFloat("EnemySpawnTime", value);
    }
}
