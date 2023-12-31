using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy", menuName ="Scriptable/Enemy", order = 0)]
public class EnemyData : ScriptableObject
{
    public float maxHealth;
    public float enemySpeed;
    public float rotationSpeed;
    public float damage;
    [Range(0.2F, 15)]
    public float radius;
    public LayerMask layerMask;
    public List<HealthSettings> healthSettings;

    [Space(20)]
    [Range(0,3)]
    public int numberOfShots;
    [Range(0,360)]
    public float spreadAngle;
    [Range(0.1F, 5)]
    public float delayBetweenShot;
    public GameObject bulletPrefab;
}


[System.Serializable]
public class HealthSettings
{
    public Sprite healthSprite;
    public float lifePercentage;
}
