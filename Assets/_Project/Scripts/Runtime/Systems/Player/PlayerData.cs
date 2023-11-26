using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "new Player Settings", menuName = "Scriptable/Player", order = 1)]
public class PlayerData : ScriptableObject
{
    public float maxHealth;
    public float moveSpeed;
    public float rotateSpeed;
    public GameObject bulletPrefab;

    public float acceleration;
    public float deacceleration;
    public List<HealthSettings> healthSettings;
}
