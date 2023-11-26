using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Bullet", menuName = "Scriptable/Bullet", order = 2)]
public class BulletData : ScriptableObject
{
    public float speed;
    public int damage;
    public float maxDistance;
    public List<string> tagDamage;
    [HideInInspector] public float maxHealth = 1;
}
