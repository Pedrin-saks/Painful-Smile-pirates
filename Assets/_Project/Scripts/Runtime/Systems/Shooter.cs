using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{


    public void Shoot(int numberOfShots, float spreadAngle, GameObject bulletPrefab, Transform shotSpawnPoint)
    {
        float startAngle = -spreadAngle / 2f;
        float angleIncrement = numberOfShots > 1 ? spreadAngle / (numberOfShots - 1) : 0;

        for (int i = 0; i < numberOfShots; i++)
        {
            float angle = startAngle + i * angleIncrement;
            Vector3 eulerAngles = new Vector3(0, 0, angle);
            Quaternion bulletRotation = Quaternion.Euler(eulerAngles);
            Instantiate(bulletPrefab, shotSpawnPoint.position, shotSpawnPoint.rotation * bulletRotation);
        }
    }

}
