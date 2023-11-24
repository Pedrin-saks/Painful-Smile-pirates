using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] private int numberOfShots = 1; // Número de tiros no tiro triplo
    [SerializeField] private float spreadAngle = 30f; // Ângulo entre os tiros no tiro triplo


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShootCustom(int numberOfShots, float spreadAngle, GameObject bulletPrefab, Transform shotSpawnPoint)
    {
        float startAngle = -spreadAngle / 2f;
        float angleIncrement = spreadAngle / (numberOfShots - 1);

        for (int i = 0; i < numberOfShots; i++)
        {
            float angle = startAngle + i * angleIncrement;
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
            Instantiate(bulletPrefab, shotSpawnPoint.position, transform.rotation * bulletRotation);
        }
    }

}
