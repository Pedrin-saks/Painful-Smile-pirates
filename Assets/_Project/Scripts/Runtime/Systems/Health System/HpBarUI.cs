using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Transform hpBarPosition;

    private Image imgHpBar;
    private GameObject currentHpBar;
    private HealthSystem healthSystem;
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        healthSystem.OnChangeHealth += UpdateHpBarUI;
    }

    private void OnDisable()
    {
        healthSystem.OnChangeHealth -= UpdateHpBarUI;

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHpBar = Instantiate(GameManager.Instance.prefabHpBar, GameManager.Instance.parentContentHpBar);
        imgHpBar = currentHpBar.transform.GetChild(0).GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentHpBar == null) return;
        currentHpBar.transform.position = Camera.main.WorldToScreenPoint(hpBarPosition.position);
    }

    private void UpdateHpBarUI(float currentHelath, float MaxHealth)
    {
        imgHpBar.fillAmount = currentHelath / MaxHealth;
        if(currentHelath <= 0)
        {
            Destroy(currentHpBar);
        }
    }
}
